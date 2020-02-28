using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine
{
    public static class Products
    {
        public static List<Product> GetProducts()
        {
            return new List<Product>()
            {
                new Product
                {
                    ProductId = 1,
                    ProductName = "Coke",
                    Price = 25
                },
                new Product
                {
                    ProductId = 2,
                    ProductName = "Pepsi",
                    Price = 35
                },
                new Product
                {
                    ProductId = 3,
                    ProductName = "Soda",
                    Price = 45
                },
                new Product
                {
                    ProductId = 4,
                    ProductName = "Chocolate bar",
                    Price = 20.25
                },
                new Product
                {
                    ProductId = 5,
                    ProductName = "Chewing gum",
                    Price = 10.50
                },
                new Product
                {
                    ProductId = 6,
                    ProductName = "Bottled water",
                    Price = 15
                }
            };
        }
    }
}
