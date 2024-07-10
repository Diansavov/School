using Data;
using Model;

namespace Business
{
    //Smeni Towns navsqkude
    public class TownsBusiness
    {

        private TownsContext townsContext;
        public List<Town> GetAll()
        {
            using (townsContext = new TownsContext())
            {
                return townsContext.Towns.ToList();
            }
        }
        public Town Get(int id)
        {
            using (townsContext = new TownsContext())
            {
                Town town =  townsContext.Towns.Find(id);
                Country country = townsContext.Countries.Find(town.CountryId);
                town.Country = country;
                return town;
            }
        }
        public void Add(Town town, string countryName)
        {
            using (townsContext = new TownsContext())
            {
                Country countryExists = townsContext.Countries.Where(e => e.Name == countryName).FirstOrDefault();
                if (countryExists == null)
                {
                    Country country = new Country();
                    country.Name = countryName;
                    town.Country = country;
                }
                else
                {
                    town.CountryId = countryExists.Id;
                }
                townsContext.Towns.Add(town);
                townsContext.SaveChanges();
            }
        }
        public void Update(Town town, string countryName)
        {
            using (townsContext = new TownsContext())
            {
                var item = townsContext.Towns.Find(town.Id);
                if (item != null)
                {
                    Country countryExists = townsContext.Countries.Where(e => e.Name == countryName).FirstOrDefault();
                    if (countryExists != null)
                    {
                        town.CountryId = countryExists.Id;
                    }
                    else
                    {
                        countryExists = new Country(){
                            Name = countryName
                        };
                        town.Country = countryExists;
                    }
                    townsContext.Entry(item).CurrentValues.SetValues(town);

                    townsContext.SaveChanges();
                }
            }
        }
        public void Delete(int id)
        {
            using (townsContext = new TownsContext())
            {
                var town = townsContext.Towns.Find(id);
                if (town != null)
                {
                    townsContext.Towns.Remove(town);

                    townsContext.SaveChanges();
                }
            }

        }
    }
    public class CountryBusiness
    {
        private TownsContext townsContext;
        public List<Country> GetAll()
        {
            using (townsContext = new TownsContext())
            {
                return townsContext.Countries.ToList();
            }
        }
        public void Add(Country country)
        {
            using (townsContext = new TownsContext())
            {
                townsContext.Countries.Add(country);
                townsContext.SaveChanges();
            }
        }
        public void Update(Country country)
        {
            using (townsContext = new TownsContext())
            {
                var item = townsContext.Countries.Find(country.Id);
                if (item != null)
                {
                    townsContext.Entry(item).CurrentValues.SetValues(country);

                    townsContext.SaveChanges();

                }
            }
        }
        public Country Get(int id)
        {
            using (townsContext = new TownsContext())
            {
                return townsContext.Countries.Find(id);
            }
        }
        public void Delete(int id)
        {
            using (townsContext = new TownsContext())
            {
                var country = townsContext.Countries.Find(id);
                if (country != null)
                {
                    List<Town> towns = townsContext.Towns.Where(e => e.CountryId == id).ToList();
                    if (towns != null)
                    {
                        foreach (var town in towns)
                        {
                            townsContext.Towns.Remove(town);
                        }
                    }
                    townsContext.Countries.Remove(country);
                    townsContext.SaveChanges();
                }
            }

        }
    }
}