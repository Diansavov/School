using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business;
using Model;
using System.Data;
using System.ComponentModel;
using Microsoft.Identity.Client;

namespace Presentation
{

    public class Display
    {
        private int closeOperationId = 6;
        private TownsBusiness townBusiness = new TownsBusiness();
        public Display()
        {
            Input();
        }

        private void ShowMenu()
        {
            Console.WriteLine(new string('-', 40));
            Console.WriteLine(new string(new string(' ', 18) + "Menu" + new string(' ', 18)));
            Console.WriteLine(new string('-', 40));
            Console.WriteLine("1. List all towns");
            Console.WriteLine("2. Add new town");
            Console.WriteLine("3. Update town");
            Console.WriteLine("4. Fetch town by id");
            Console.WriteLine("5. Delete town by id");
            Console.WriteLine("6. Exit");
        }

        private void Input()
        {
            var operation = -1;
            do
            {
                ShowMenu();
                operation = int.Parse(Console.ReadLine());
                switch (operation)
                {
                    case 1:
                        ListAll();
                        break;
                    case 2:
                        Add();
                        break;
                    case 3:
                        Update();
                        break;
                    case 4:
                        Fetch();
                        break;
                    case 5:
                        Delete();
                        break;
                    default:
                        break;
                }
            }
            while (operation != closeOperationId);
        }

        private void Delete()
        {
            Console.WriteLine("Enter Id to delete: ");
            int id = CheckBiggerInt();
            if (id == -1)
            {
                return;
            }
            townBusiness.Delete(id);
            Console.WriteLine("Done");
        }

        private void Fetch()
        {
            Console.WriteLine("Enter Id to fetch: ");
            int id = CheckBiggerInt();
            Town town = townBusiness.Get(id);
            if (id == -1)
            {
                return;
            }
            if (town != null)
            {
                Console.WriteLine(new string('-', 45));
                Console.WriteLine("Id: " + town.Id);
                Console.WriteLine("Name: " + town.Name);
                Console.WriteLine(new string('-', 45));
                Console.WriteLine("Done");
            }
            else
            {
                System.Console.WriteLine("Town not found");
                return;
            }
        }

        private void ListAll()
        {
            Console.WriteLine(new string('-', 45));
            Console.WriteLine(new string(new string(' ', 18) + "Town" + new string(' ', 18)));
            Console.WriteLine(new string('-', 45));
            var towns = townBusiness.GetAll();
            foreach (var item in towns)
            {
                Console.WriteLine($"{item.Id} {item.Name}");
            }
        }

        private void Add()
        {
            Town town = new Town();

            Console.WriteLine("Enter name: ");
            town.Name = Console.ReadLine();

            townBusiness.Add(town);

        }

        private void Update()
        {
            Console.WriteLine("Enter Id to update: ");
            int id = CheckBiggerInt();
            if (id == -1)
            {
                return;
            }

            Town town = townBusiness.Get(id);
            if (town != null)
            {
                Console.WriteLine("Enter name: ");
                town.Name = Console.ReadLine();

                townBusiness.Update(town);
            }
            else
            {
                Console.WriteLine("Town Not Found");
            }
            Console.WriteLine("Done");
        }
        private int CheckBiggerInt()
        {
            int id = 0;
            try
            {
                id = int.Parse(Console.ReadLine());
            }
            catch (OverflowException e)
            {
                Console.WriteLine();
                return -1;
            }

            return id;
        }
    }
}
