using System;
using Business;
using Model;
using Microsoft.Identity.Client;
using System.Xml;


namespace View
{
    public class Display
    {
        private int closeOperationId = 10;
        private TownsBusiness townBusiness = new TownsBusiness();
        private CountryBusiness countryBusiness = new CountryBusiness();
        public Display()
        {
            Input();
        }

        private void ShowMenu()
        {
            //Smeni towns i podobni
            Console.WriteLine(new string('-', 40));
            Console.WriteLine(new string(new string(' ', 18) + "Menu" + new string(' ', 18)));
            Console.WriteLine(new string('-', 40));
            Console.WriteLine("1. List all towns");
            Console.WriteLine("2. Add new town");
            Console.WriteLine("3. Update town");
            Console.WriteLine("4. Fetch town by id");
            Console.WriteLine("5. Add new country");
            Console.WriteLine("6. Update country");
            Console.WriteLine("7. Fetch a country");
            Console.WriteLine("8. Delete town by id");
            Console.WriteLine("9. Delete country by id");
            Console.WriteLine("10. Exit");
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
                        AddTown();
                        break;
                    case 3:
                        UpdateTown();
                        break;
                    case 4:
                        FetchTown();
                        break;
                    case 5:
                        AddCountry();
                        break;
                    case 6:
                        UpdateCountry();
                        break;
                    case 7:
                        FetchCountry();
                        break;
                    case 8:
                        DeleteTown();
                        break;
                    case 9:
                        DeleteCountry();
                        break;

                    default:
                        break;
                }
            }
            while (operation != closeOperationId);
        }
        private void ListAll()
        {
            Console.WriteLine(new string('-', 45));
            Console.WriteLine(new string(new string(' ', 18) + "Town" + new string(' ', 18)));
            Console.WriteLine(new string('-', 45));
            var towns = townBusiness.GetAll();
            foreach (var item in towns)
            {
                Console.WriteLine($"{item.Id} {item.Name} {item.CountryId}");
            }
            Console.WriteLine(new string('-', 45));
            Console.WriteLine(new string(new string(' ', 18) + "Countires" + new string(' ', 18)));
            Console.WriteLine(new string('-', 45));
            var countries = countryBusiness.GetAll();
            foreach (var item in countries)
            {
                Console.WriteLine($"{item.Id} {item.Name}");
            }
        }
        private void FetchTown()
        {
            Console.WriteLine("Enter Id to fetch: ");
            int id = int.Parse(Console.ReadLine());
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
                Console.WriteLine("Country Id: " + town.Country.Id);
                Console.WriteLine("Country Name: " + town.Country.Name);
                Console.WriteLine(new string('-', 45));
                Console.WriteLine("Done");
            }
            else
            {
                Console.WriteLine("Town not found");
                return;
            }
        }

        private void AddTown()
        {
            Town town = new Town();

            Console.WriteLine("Enter name: ");
            town.Name = Console.ReadLine();
            Console.WriteLine("Enter the town's country name:");
            string countryName = Console.ReadLine();

            townBusiness.Add(town, countryName);

        }

        private void UpdateTown()
        {
            Console.WriteLine("Enter Id to update: ");
            int id = int.Parse(Console.ReadLine());
            if (id == -1)
            {
                return;
            }

            Town town = townBusiness.Get(id);
            if (town != null)
            {
                Console.WriteLine("Enter name: ");
                town.Name = Console.ReadLine();
                Console.WriteLine("Enter Country name:");
                string countryName = Console.ReadLine();

                townBusiness.Update(town, countryName);
            }
            else
            {
                Console.WriteLine("Town Not Found");
            }
            Console.WriteLine("Done");
        }
        private void DeleteTown()
        {
            Console.WriteLine("Enter Id to delete: ");
            int id = int.Parse(Console.ReadLine());
            if (id == -1)
            {
                return;
            }
            townBusiness.Delete(id);
            Console.WriteLine("Done");
        }
        private void DeleteCountry()
        {
            Console.WriteLine("Enter Id to delete: ");
            int id = int.Parse(Console.ReadLine());
            if (id == -1)
            {
                return;
            }
            countryBusiness.Delete(id);
            Console.WriteLine("Done");
        }





        private void AddCountry()
        {
            Country country = new Country();

            Console.WriteLine("Enter country name:");
            country.Name = Console.ReadLine();

            countryBusiness.Add(country);
        }
        private void UpdateCountry()
        {
            Console.WriteLine("Enter Id to update: ");
            int id = int.Parse(Console.ReadLine());
            if (id == -1)
            {
                return;
            }

            Country country = countryBusiness.Get(id);
            if (country != null)
            {
                Console.WriteLine("Enter name: ");
                country.Name = Console.ReadLine();

                countryBusiness.Update(country);
            }
            else
            {
                Console.WriteLine("Country Not Found");
            }
            Console.WriteLine("Done");
        }
        private void FetchCountry()
        {
            Console.WriteLine("Enter Id to fetch: ");
            int id = int.Parse(Console.ReadLine());
            Country country = countryBusiness.Get(id);
            if (id == -1)
            {
                return;
            }
            if (country != null)
            {
                Console.WriteLine(new string('-', 45));
                Console.WriteLine("Id: " + country.Id);
                Console.WriteLine("Name: " + country.Name);
                Console.WriteLine(new string('-', 45));
                Console.WriteLine("Done");
            }
            else
            {
                Console.WriteLine("Country not found");
                return;
            }
        }
    }
}
