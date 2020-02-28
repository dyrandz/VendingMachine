using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using VendingMachine.Services;

namespace VendingMachine
{
    class Program
    {
        public static CustomerMoney _customerMoney = new CustomerMoney();
        public static CustomerSelectedProducts _customerProducts = new CustomerSelectedProducts();
        public static VendingMachineService _service = new VendingMachineService();
        static async Task Main(string[] args)
        {
            _customerMoney.Money = new List<InsertedMoney>();
            _customerProducts.Products = new List<Product>();

           var amount = string.Empty;
           var acceptPaymentResult = new InsertedMoney();
           var selectProductResult = new Product();
           Print();
           var command = Console.ReadLine();
           while (!string.Equals(command, "X", StringComparison.OrdinalIgnoreCase))
           {
               if (Compare(command, "A"))
               {
                   command = await CommandA(command);
               }
               else if (Compare(command, "B"))
               {
                   command = await CommandB(command);
               }
               else if (Compare(command, "C"))
               {
                   command = await CommandC();
               }else if ((Compare(command, "D")))
               {
                   command = await CommandD();
               }
               else if ((Compare(command, "E")))
               {
                   Environment.Exit(0);
               }
               else
               {
                   Print();
                   command = Console.ReadLine();
               }
           }
        }

        private static async Task<string> CommandA(string com)
        {
            var command = com;
            while (Compare(command, "A"))
            {
                Console.WriteLine("Allowed amount: [100] [50] [20] [10] [5] [1] [0.50] [0.25]");
                Console.Write("Insert Amount: ");
                command = Console.ReadLine();
                var acceptPaymentResult = await _service.AcceptPayment(double.Parse(command));
                if (acceptPaymentResult != null)
                {
                    _customerMoney.Money.Add(acceptPaymentResult);
                }
                Print();
            }

            return command;
        }

        private static async Task<string> CommandB(string com)
        {
            var command = com;
            while (Compare(command, "B"))
            {
                Console.Write("Select Product By Choosing Product ID: ");
                command = Console.ReadLine();
                var selectProductResult = await _service.SelectProduct(int.Parse(command));
                if (selectProductResult != null)
                {
                    _customerProducts.Products.Add(selectProductResult);
                }
                Print();
            }

            return command;
        }

        private static async Task<string> CommandC()
        {
            var evaluateResult = await _service.EvaluatePurchase(_customerProducts, _customerMoney);
            if (!evaluateResult.Item1)
            {
                Console.Clear();
                Console.WriteLine($"MESSAGE: {evaluateResult.Item2}");
                Console.WriteLine("Commands: [A]-> Add Payment, [B] -> Select Product, [C] -> Calculate, [D] -> Cancel Request, [E] -> exit");
                return Console.ReadLine();
            }


            var result = await _service.Calculate(_customerProducts, _customerMoney);
            Console.Clear();
            Console.WriteLine("Purchased Products: ");
            result.Item1.Products.ForEach(i => Console.WriteLine("Id: {0}\t Name:{1}\t Price:{2}", i.ProductId, i.ProductName, i.Price));

            Console.WriteLine("Change: Php " + result.Item2);

            Console.WriteLine();
            Console.WriteLine("Commands: [A]-> Add Payment, [B] -> Select Product, [C] -> Calculate, [D] -> Cancel Request, [E] -> exit");
            await ResetData();
            return Console.ReadLine();
        }

        private static async Task<string> CommandD()
        {
            var result = await _service.CancelRequest(_customerMoney);
            Console.Clear();
            Console.WriteLine($"{result.Item1}: {result.Item2}");

            Console.WriteLine();
            Console.WriteLine("Commands: [A]-> Add Payment, [B] -> Select Product, [C] -> Calculate, [D] -> Cancel Request, [E] -> exit");
            return Console.ReadLine();
        }

        public static async Task ResetData()
        {
            _customerMoney.Money = new List<InsertedMoney>();
            _customerProducts.Products = new List<Product>();
        }

        private static bool Compare(string x, string y)
        {
            return string.Equals(x, y, StringComparison.OrdinalIgnoreCase);
        }

        private static void Print()
        {
            Console.Clear();

            Console.WriteLine();
            Console.WriteLine("List of Products: ");
            Products.GetProducts().ForEach(i => Console.WriteLine("Id: {0}\t Name:{1}\t Price:{2}", i.ProductId, i.ProductName, i.Price));

            Console.WriteLine();
            Console.WriteLine("Selected Products: ");
            _customerProducts?.Products?.ForEach(i => Console.WriteLine("Id: {0}\t Name:{1}\t Price:{2}", i.ProductId, i.ProductName, i.Price));
            Console.WriteLine("Total Products Amount: " + _customerProducts?.TotalPrice);

            Console.WriteLine();
            Console.WriteLine("Total Money: Php " + _customerMoney?.TotalMoney);

            Console.WriteLine();
            Console.WriteLine("Commands: [A]-> Add Payment, [B] -> Select Product, [C] -> Calculate, [D] -> Cancel Request, [E] -> exit");
        }
    }
}
