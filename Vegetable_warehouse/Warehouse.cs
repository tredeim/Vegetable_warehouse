using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Vegetable_warehouse
{
    class Warehouse
    {
        private List<Worker> _workers;

        private List<Container> _containers;

        public int SalaryBudget { get; }

        public int Limit { get; }

        public double Price { get; }

        public int ContainersCount => _containers.Count;

        public Warehouse(int limit, double price, int salaryBudget)
        {
            if (limit < 1)
            {
                throw new ArgumentException(nameof(limit));
            }

            if (price <= 0)
            {
                throw new ArgumentException(nameof(price));
            }

            if (salaryBudget < 0)
            {
                throw new ArgumentException(nameof(salaryBudget));
            }

            Limit = limit;
            Price = price;
            SalaryBudget = salaryBudget;
            _containers = new List<Container>();
            _workers = new List<Worker>();
        }

        // Method for adding container.

        public bool AddContainer(Container container)
        {
            if (_containers.Count == Limit)
            {
                if (container.GetTotalPrice() > Price)
                {
                    if (_containers.Count > 0)
                    {
                        _containers.RemoveAt(0);
                    }

                    _containers.Add(container);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (container.GetTotalPrice() > Price)
                {
                    _containers.Add(container);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        // Method for removeing container.

        public bool RemoveContainer(Guid guid)
        {
            if (_containers.RemoveAll(c => c.Guid == guid) > 0)
            {
                return true;
            }

            return false;
        }

        // Method for adding worker.

        public bool AddWorker(Worker worker)
        {
            if (worker.Salary + _workers.Sum(b => b.Salary) <= SalaryBudget)
            {
                _workers.Add(worker);
                return true;
            }

            return false;
        }

        // Method for removeing worker.

        public bool RemoveWorker(Guid guid)
        {
            if (_workers.RemoveAll(c => c.Guid == guid) > 0)
            {
                return true;
            }

            return false;
        }

        // Method for adding container.

        public IReadOnlyList<Container> GetContainers()
        {
            return _containers.AsReadOnly();
        }

        public IReadOnlyList<Worker> GetWorkers()
        {
            return _workers.AsReadOnly();
        }

        // Method for getting warehouse info.

        public override string ToString()
        {
            string info = $@"
 Vegetable Warehouse info:
            Warehouse limit: { Limit}
            Fee for container storage: { Price}
            Salary budget: { SalaryBudget}
";
            info += " Workers info:";
            for (int i = 0; i < _workers.Count; i++)
            {
                info += $"\t{i + 1}. Worker info\n\t | GUID: {_workers[i].Guid}\n\t | Name: {_workers[i].Name}\n\t | Salary: {_workers[i].Salary}\n";
            }
            info += " Containers info:";
            for (int i = 0; i < _containers.Count; i++)
            {
                info += $"{i + 1}. Container" + _containers[i].ToString();
            }
            return info ;
        }
    }
}
