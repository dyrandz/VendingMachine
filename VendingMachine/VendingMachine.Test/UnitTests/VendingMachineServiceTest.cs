using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Services;
using Xunit;

namespace VendingMachine.Test.UnitTests
{
    public class VendingMachineServiceTest
    {
        public static VendingMachineService _Service = new VendingMachineService();

        [Theory]
        [InlineData(100)]
        [InlineData(50)]
        [InlineData(20)]
        [InlineData(10)]
        [InlineData(5)]
        [InlineData(1)]
        [InlineData(0.25)]
        [InlineData(0.50)]
        public async Task AcceptPayment_ShouldReturnValue(double amount)
        {
            var result = await _Service.AcceptPayment(amount);

            Assert.True(result.Amount > 0);
        }

        [Theory]
        [InlineData(1000)]
        [InlineData(75)]
        [InlineData(63)]
        [InlineData(22)]
        [InlineData(0)]
        public async Task AcceptPayment_ShouldReturnNULL(double amount)
        {
            var result = await _Service.AcceptPayment(amount);

            Assert.True(result == null);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        public async Task SelectProduct_ShouldReturnProduct(int productId)
        {
            var result = await _Service.SelectProduct(productId);

            Assert.True(result != null);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(20)]
        [InlineData(30)]
        [InlineData(40)]
        [InlineData(50)]
        [InlineData(60)]
        public async Task SelectProduct_ShouldReturnNull(int productId)
        {
            var result = await _Service.SelectProduct(productId);

            Assert.True(result == null);
        }

        [Fact]
        public async Task CancelRequest_ShouldreturnValue()
        {
            var customerMoney = new CustomerMoney
            {
                Money = new List<InsertedMoney>()
                {
                    new InsertedMoney()
                    {
                        Type = MoneyType.Bills,
                        Amount = 100
                    },
                    new InsertedMoney()
                    {
                        Type = MoneyType.Coins,
                        Amount = 5
                    },
                    new InsertedMoney()
                    {
                        Type = MoneyType.Cents,
                        Amount = 0.25
                    }
                }
            };

            var expected = Tuple.Create("Refund Amount", customerMoney.TotalMoney);

            var result = await _Service.CancelRequest(customerMoney);

            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task Calculate_ShouldReturnCorrectValue()
        {
            var customerProducts = new CustomerSelectedProducts()
            {
                Products = new List<Product>()
                {
                    new Product()
                    {
                        ProductId = 1,
                        ProductName = "Coke",
                        Price = 25
                    },
                    new Product()
                    {
                        ProductId = 1,
                        ProductName = "Pepsi",
                        Price = 25
                    }
                }
            };

            var customerMoney = new CustomerMoney
            {
                Money = new List<InsertedMoney>()
                {
                    new InsertedMoney()
                    {
                        Type = MoneyType.Bills,
                        Amount = 100
                    },
                    new InsertedMoney()
                    {
                        Type = MoneyType.Coins,
                        Amount = 5
                    }
                }
            };

            var result = await _Service.Calculate(customerProducts, customerMoney);

            Assert.Equal(55, result.Item2);
        }

        [Fact]
        public async Task EvaluatePurchase_ShouldReturn_False()
        {
            var customerProducts = new CustomerSelectedProducts()
            {
                Products = new List<Product>()
                {
                    new Product()
                    {
                        ProductId = 1,
                        ProductName = "Coke",
                        Price = 25
                    },
                    new Product()
                    {
                        ProductId = 1,
                        ProductName = "Pepsi",
                        Price = 25
                    }
                }
            };

            var customerMoney = new CustomerMoney
            {
                Money = new List<InsertedMoney>()
                {
                    new InsertedMoney()
                    {
                        Type = MoneyType.Bills,
                        Amount = 20
                    },
                    new InsertedMoney()
                    {
                        Type = MoneyType.Cents,
                        Amount = 0.50
                    }
                }
            };

            var result = await _Service.EvaluatePurchase(customerProducts, customerMoney);

            Assert.False(result.Item1);
        }

        [Fact]
        public async Task EvaluatePurchase_ShouldReturn_True()
        {
            var customerProducts = new CustomerSelectedProducts()
            {
                Products = new List<Product>()
                {
                    new Product()
                    {
                        ProductId = 1,
                        ProductName = "Coke",
                        Price = 25
                    },
                    new Product()
                    {
                        ProductId = 1,
                        ProductName = "Pepsi",
                        Price = 25
                    }
                }
            };

            var customerMoney = new CustomerMoney
            {
                Money = new List<InsertedMoney>()
                {
                    new InsertedMoney()
                    {
                        Type = MoneyType.Bills,
                        Amount = 50
                    },
                    new InsertedMoney()
                    {
                        Type = MoneyType.Cents,
                        Amount = 0.50
                    }
                }
            };

            var result = await _Service.EvaluatePurchase(customerProducts, customerMoney);

            Assert.True(result.Item1);
        }
    }
}
