using System;
using System.Collections.Generic;
using System.Text;

namespace Vegetable_warehouse
{
    class Worker
    {
        public string Name { get; }

        public int Salary { get; }

        public Guid Guid { get; }

        public Worker(string name, int salary, Guid guid)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException(nameof(name));
            }

            if (salary < 0)
            {
                throw new ArgumentException(nameof(salary));
            }

            Name = name;
            Salary = salary;
            Guid = guid;
        }
    }
}
