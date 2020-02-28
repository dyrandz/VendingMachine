using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace VendingMachine
{
    public class CustomerSelectedProducts
    {
        public List<Product> Products { get; set; }

        public double TotalPrice
        {
            get
            {
                if (Products != null)
                {
                    return Products.Sum(s => s.Price);
                }

                return 0;
            }
        } 
    }
}
