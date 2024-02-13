using System;
using System.Collections.Generic;
using System.Text;

namespace Vegetable_warehouse
{
    public class VegetableBox
    {
        public double PriceForKg { get; set; }
        
        public double Capacity { get; }
        
        public VegetableBox(double priceForKg, double capacity)
        {
            if (priceForKg <= 0)
            {
                throw new ArgumentException(nameof(priceForKg));
            }

            if (capacity <= 0)
            {
                throw new ArgumentException(nameof(capacity));
            }
            PriceForKg = priceForKg;
            Capacity = capacity;
        }

        // Get the price for a box.

        public double GetPrice()
        {
            return PriceForKg * Capacity;
        }
    }
}
