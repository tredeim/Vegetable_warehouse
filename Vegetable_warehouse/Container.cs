using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vegetable_warehouse
{
    class Container
    {
        private List<VegetableBox> _boxes;

        public int Limit { get; }

        public double Damage { get; }

        public Guid Guid { get; }
        
        public Container(Guid guid)
        {
            Limit = GetRandomLimit();
            Damage = GetRandomDamage();
            _boxes = new List<VegetableBox>();
            Guid = guid;
        }

        // Method for adding the box to the container.

        public bool TryAdd(VegetableBox box)
        {
            double weight = box.GetPrice();
            double sum = GetTotalWeight();
            if (sum + weight <= Limit)
            {
                _boxes.Add(box);
                return true;
            }
            else
            {
                return false;
            }
        }

        // Get total weight of the container.

        public double GetTotalWeight()
        {
            return _boxes.Sum(b => b.Capacity);
        }
        
        // Get total price of the container.

        public double GetTotalPrice()
        {
            var volume = _boxes.Sum(b => b.GetPrice());
            return volume - volume * Damage;
        }

        // Method for getting container info.

        public override string ToString()
        {
            string output = $"\n\t | GUID: {Guid}\n\t | Limit: {Limit}(Kg)\n\t | Damage: {Damage}\n\t | Count of boxes: {_boxes.Count}\n\t | Total Weight: {GetTotalWeight()}Kg\n\t | Total Price: {GetTotalPrice()}\n";
            for (int i = 0; i < _boxes.Count; i++)
            {
                output += $" Box{i + 1}  \t| Price for Kg: {_boxes[i].PriceForKg}\t| Weight: {_boxes[i].Capacity}Kg\t| Price: {_boxes[i].GetPrice()}\n";
            }
            return output;
        }
        private static int GetRandomLimit()
        {
            Random rnd = new Random();
            int value = rnd.Next(50, 1001);
            return value;
        }

        private static double GetRandomDamage()
        {
            Random rnd = new Random();
            double value = rnd.NextDouble();

            return value / 2;
        }
    }
}
