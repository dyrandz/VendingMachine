using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VendingMachine
{
    public class CustomerMoney
    {
        public List<InsertedMoney> Money { get; set; }

        public double TotalMoney 
        {
            get
            {
                if (Money == null) return 0;
                var bills = Money.Where(m => m.Type == MoneyType.Bills).Sum(s => s.Amount);
                var coins = Money.Where(m => m.Type == MoneyType.Coins).Sum(s => s.Amount);
                var cents = Money.Where(m => m.Type == MoneyType.Cents).Sum(s => (s.Amount));

                return bills + coins + cents;
            }
        }
    }
}
