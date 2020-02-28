using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Services
{
    public class VendingMachineService
    {
        public async Task<InsertedMoney> AcceptPayment(double amount)
        {
            if (MoneyList.Bills.Contains(amount))
                return new InsertedMoney { Type = MoneyType.Bills, Amount = amount };

            if (MoneyList.Coins.Contains(amount))
                return new InsertedMoney { Type = MoneyType.Coins, Amount = amount };

            return MoneyList.Cents.Contains(amount) ? new InsertedMoney { Type = MoneyType.Cents, Amount = amount } : null;
        }

        public async Task<Product> SelectProduct(int productId)
        {
            return Products.GetProducts().SingleOrDefault(p => p.ProductId == productId);
        }

        public async Task<Tuple<string, double>> CancelRequest(CustomerMoney money)
        {
            return Tuple.Create("Refund Amount", money.TotalMoney);
        }

        public async Task<Tuple<CustomerSelectedProducts, double>> Calculate(CustomerSelectedProducts selectedProducts, CustomerMoney money)
        {
            return Tuple.Create(selectedProducts, money.TotalMoney - selectedProducts.TotalPrice);
        }

        public async Task<Tuple<bool, string>> EvaluatePurchase(CustomerSelectedProducts selectedProducts, CustomerMoney money)
        {
            return (money.TotalMoney - selectedProducts.TotalPrice) < 0 ? Tuple.Create(false, "Insufficient fund!") : Tuple.Create(true, string.Empty);
        }
    }
}
