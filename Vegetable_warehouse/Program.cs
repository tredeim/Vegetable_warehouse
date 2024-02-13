using System;
using System.Collections.Generic;
using System.IO;

namespace Vegetable_warehouse
{
    class Program
    {
        static void Main(string[] args)
        {
            // Greeting and review.

            Console.WriteLine("                                               Vegetable Warehouse");
            Console.WriteLine(" Hello, we need to set data for your vegetable warehouse of your company.");
            Console.WriteLine(" You have workers who have a name and salary so for this we need to set a salary budget of your company.");
            Console.WriteLine(" The warehouse has cantainers and canteiners store crates. You need to set the data for them too.");

            // To repeat the program.

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine(" Please select a data input method.");
                Console.WriteLine(" If you want to enter data from a file then write 'file' and if from the console then just press enter.");
                Console.Write(" >>> ");
                var mode = Console.ReadLine();
                if (mode == "file")
                {
                    Console.WriteLine(" Data will be read from file.");
                    Console.WriteLine();
                    DoFromFile();
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine(" Data will be read from console.");
                    DoFromConsole();
                }
                Console.WriteLine(" If you want to exit the program write 'end' or press enter if you want to repeat.");
                Console.Write(" >>> ");
                var input = Console.ReadLine();
                if (input == "end")
                {
                    break;
                }
            }
        }
        // Method for working with files.

        private static void DoFromFile()
        {
            try
            {
                Console.WriteLine(" Write the name of the file from which you want to read the warehouse data. ");
                try
                {
                    try
                    {
                        // Create the warehouse

                        Console.Write(" >>> ");
                        var filename = Console.ReadLine();
                        var input = File.ReadAllLines(filename);
                        var warehousedata = input[1].Split("\t");
                        int limit = int.Parse(warehousedata[0]);
                        double price = double.Parse(warehousedata[1]);
                        int salarybudget = int.Parse(warehousedata[2]);
                        Warehouse warehouse = new Warehouse(limit, price, salarybudget);
                        Console.WriteLine(" Write the name of the file from which you want to read the commands.");
                        try
                        {
                            Console.Write(" >>> ");
                            var filename1 = Console.ReadLine();
                            var lines = File.ReadAllLines(filename1);
                            for (int k = 0; k < lines.Length; k++)
                            {
                                var commands = lines[k].Split(' ');
                                if (commands.Length > 0)
                                {
                                    var command = commands[0];
                                    if (command == "add")
                                    {
                                        try
                                        {
                                            var guid = Guid.NewGuid();
                                            var container = new Container(guid);

                                            // Print Container info.

                                            Console.WriteLine($@"
 Container info:
     Container guid: {guid}
     Container limit: {container.Limit}Kg
     Container damage: {container.Damage}");

                                            var boxes = File.ReadAllLines(commands[1]);
                                            var prices = boxes[0].Split("\t");
                                            var weigher = boxes[1].Split("\t");
                                            for (int i = 0; i < prices.Length; i++)
                                            {
                                                VegetableBox box = new VegetableBox(double.Parse(prices[i]), double.Parse(weigher[i]));
                                                if (container.TryAdd(box))
                                                {
                                                    Console.WriteLine(" Vegetable box added.");
                                                }
                                                else
                                                {
                                                    Console.WriteLine(" There is no room for such a box.");

                                                }
                                            }

                                            if (warehouse.AddContainer(container))
                                            {
                                                Console.WriteLine(" Container added.");
                                            }
                                            else
                                            {
                                                Console.WriteLine(" ERROR! Cannot add container because it's not profitable.");
                                            }

                                        }
                                        catch (Exception e)
                                        {
                                            Console.WriteLine(" " + e.Message);
                                        }

                                    }

                                    else if (command == "remove")
                                    {
                                        try
                                        {
                                            var guid = Guid.Parse(commands[1]);
                                            if (warehouse.RemoveContainer(guid))
                                            {
                                                Console.WriteLine(" Container removed.");
                                            }
                                            else
                                            {
                                                Console.WriteLine(" ERROR! There is no such container.");
                                            }
                                        }
                                        catch (Exception e)
                                        {
                                            Console.WriteLine(" " + e.Message);
                                        }
                                    }

                                    else if (command == "addworker")
                                    {
                                        try
                                        {
                                            var workman = File.ReadAllLines(commands[1]);
                                            var name = workman[0];
                                            var salary = int.Parse(workman[1]);
                                            var guid = Guid.NewGuid();
                                            var worker = new Worker(name, salary, guid);
                                            if (warehouse.AddWorker(worker))
                                            {
                                                Console.WriteLine(" Worker added.");
                                            }
                                            else
                                            {
                                                Console.WriteLine(" ERROR! Cannot add worker because you have insufficient salary budget.");
                                            }

                                            // Print Worker info.

                                            Console.WriteLine($@"
 Worker info:
     Worker guid: {guid}
     Worker name: {worker.Name}
     Worker salary: {worker.Salary}");

                                        }
                                        catch (Exception e)
                                        {
                                            Console.WriteLine(" " + e.Message);
                                        }
                                    }

                                    else if (command == "removeworker")
                                    {
                                        try
                                        {
                                            var guid = Guid.Parse(commands[1]);
                                            if (warehouse.RemoveWorker(guid))
                                            {
                                                Console.WriteLine(" Worker removed.");
                                            }
                                            else
                                            {
                                                Console.WriteLine(" ERROR! There is no such worker.");
                                            }
                                        }
                                        catch (Exception e)
                                        {
                                            Console.WriteLine(" " + e.Message);
                                        }
                                    }

                                    else
                                    {
                                        Console.WriteLine(" Wrong command!");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine(" Incorrect input");
                                }

                            }

                            var info = warehouse.ToString();
                            Console.WriteLine(" Write the name of the file where there will be warehouse info");
                            Console.Write(" >>> ");
                            var fileinfo = Console.ReadLine();
                            File.WriteAllText(fileinfo, info);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(" " + e.Message);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(" " + e.Message);
                    }
                }

                catch (Exception e)
                {
                    Console.WriteLine(" " + e.Message);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(" " + e.Message);
            }

        }

        // Method for working with the console.

        private static void DoFromConsole()
        {
            try
            {
                // Create the warehouse.

                Console.WriteLine(" Set the limit on the count of containers in your warehouse (int).");
                Console.Write(" >>> ");
                int limit = int.Parse(Console.ReadLine());
                Console.WriteLine(" Set the fee for container storage (double).");
                Console.Write(" >>> ");
                double price = double.Parse(Console.ReadLine());
                Console.WriteLine(" Set the salary budget (int).");
                Console.Write(" >>> ");
                int salarybudget = int.Parse(Console.ReadLine());
                Warehouse warehouse = new Warehouse(limit, price, salarybudget);
                try
                {
                    // Commands info.

                    Console.WriteLine($@"
 Write 'add' to add cantainer
 Write 'remove' to remove cantainer
 Write 'addworker' to add worker
 Write 'removeworker' to remove worker
 Write 'end' if you finished your actions");

                    // Command check.

                    while (true)
                    {
                        Console.Write(" >>> ");
                        var command = Console.ReadLine();
                        if (command == "add")
                        {
                            AddCont(warehouse);
                        }

                        else if (command == "remove")
                        {
                            RemoveCont(warehouse);
                        }
                        else if (command == "addworker")
                        {
                            AddWorkman(warehouse);
                        }
                        else if (command == "removeworker")
                        {
                            RemoveWorkman(warehouse);
                        }
                        else
                        {
                            if (command == "end")
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine(" Wrong command!");
                            }
                        }
                    }

                    WarehouseInfo(warehouse);

                }
                catch (Exception e)
                {
                    Console.WriteLine(" " + e.Message);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(" " + e.Message);
            }
        }

        // Method for getting information about the warehouse.

        public static void WarehouseInfo(Warehouse warehouse)
        {
            Console.WriteLine();
            Console.WriteLine($@"
 Vegetable Warehouse info:
     Warehouse limit: {warehouse.Limit}
     Fee for container storage: {warehouse.Price}
     Salary budget: {warehouse.SalaryBudget}");
            Console.WriteLine();
            WorkersInfo(warehouse);
            ContainersInfo(warehouse);
        }

        // Method for getting information about the workers.

        public static void WorkersInfo(Warehouse warehouse)
        {
            Console.WriteLine(" Workers info:");
            var workers = warehouse.GetWorkers();
            for (int i = 0; i < workers.Count; i++)
            {
                Console.WriteLine($"\t{i + 1}. Worker info\n\t | GUID: {workers[i].Guid}\n\t | Name: {workers[i].Name}\n\t | Salary: {workers[i].Salary}\n");
            }
        }

        // Method for getting information about the containers.

        public static void ContainersInfo(Warehouse warehouse)
        {
            Console.WriteLine(" Containers info:");
            var containers = warehouse.GetContainers();
            for (int i = 0; i < containers.Count; i++)
            {
                Console.WriteLine($"{i + 1}. Container" + containers[i].ToString());
            }
        }

        // Method for adding Container.

        public static void AddCont(Warehouse warehouse)
        {
            // Аdd container.

            try
            {
                var guid = Guid.NewGuid();
                var container = new Container(guid);
                Console.WriteLine($@"
 Container info:
     Container guid: {guid}
     Container limit: {container.Limit}Kg
     Container damage: {container.Damage}");
                Console.WriteLine(" Write 'add' to add box to container and write 'end' to finish container management.");
                while (true)
                {
                    // Аdd vegetable box.

                    try
                    {
                        Console.Write(" >>> ");
                        var input = Console.ReadLine();
                        if (input == "add")
                        {
                            Console.WriteLine(" Set the price and weight for the box (double).");
                            Console.Write(" PriceForKg: ");
                            var priceforkg = double.Parse(Console.ReadLine());
                            Console.Write(" Weight: ");
                            var capacity = double.Parse(Console.ReadLine());
                            VegetableBox box = new VegetableBox(priceforkg, capacity);
                            if (container.TryAdd(box))
                            {
                                Console.WriteLine(" Vegetable box added.");
                            }
                            else
                            {
                                Console.WriteLine(" There is no room for such a box.");

                            }
                        }
                        else
                        {
                            if (input == "end")
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine(" Wrong command!");
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(" " + e.Message);
                    }
                }

                if (warehouse.AddContainer(container))
                {
                    Console.WriteLine(" Container added.");
                }
                else
                {
                    Console.WriteLine(" ERROR! Cannot add container because it's not profitable.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(" " + e.Message);
            }
        }

        // Method for removing Container.

        public static void RemoveCont(Warehouse warehouse)
        {
            try
            {
                Console.WriteLine(" Write the guid of the container you want to remove.");
                Console.Write(" >>> ");
                var guid = Guid.Parse(Console.ReadLine());
                if (warehouse.RemoveContainer(guid))
                {
                    Console.WriteLine(" Container removed.");
                }
                else
                {
                    Console.WriteLine(" ERROR! There is no such container.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(" " + e.Message);
            }
        }

        // Method for adding worker.

        public static void AddWorkman(Warehouse warehouse)
        {
            try
            {
                Console.WriteLine(" Enter the name (string) and salary (int) of the employee.");
                Console.Write(" Name: ");
                var name = Console.ReadLine();
                Console.Write(" Salary: ");
                var salary = int.Parse(Console.ReadLine());
                var workerguid = Guid.NewGuid();
                var worker = new Worker(name, salary, workerguid);
                if (warehouse.AddWorker(worker))
                {
                    Console.WriteLine(" Worker added.");
                }
                else
                {
                    Console.WriteLine(" ERROR! Cannot add worker because you have insufficient salary budget.");
                }
                Console.WriteLine($@"
 Worker info:
     Worker guid: {workerguid}
     Worker name: {worker.Name}
     Worker salary: {worker.Salary}");
            }
            catch (Exception e)
            {
                Console.WriteLine(" " + e.Message);
            }
        }

        // Method for removing vorker.

        public static void RemoveWorkman(Warehouse warehouse)
        {
            try
            {
                Console.WriteLine(" Write the guid of the worker you want to remove.");
                Console.Write(" >>> ");
                var workerguid = Guid.Parse(Console.ReadLine());
                if (warehouse.RemoveWorker(workerguid))
                {
                    Console.WriteLine(" Worker removed.");
                }
                else
                {
                    Console.WriteLine(" ERROR! There is no such worker.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(" " + e.Message);
            }
        }
    }
}
