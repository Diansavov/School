using Data;
using Model;

namespace Business
{
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
                return townsContext.Towns.Find(id);
            }
        }
        public void Add(Town town)
        {
            using (townsContext = new TownsContext())
            {
                townsContext.Towns.Add(town);
                townsContext.SaveChanges();
            }
        }
        public void Update(Town town)
        {
            using (townsContext = new TownsContext())
            {
                var item = townsContext.Towns.Find(town.Id);
                if (item != null)
                {
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
}