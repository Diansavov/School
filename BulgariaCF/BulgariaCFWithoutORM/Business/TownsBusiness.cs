using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrudWithoutORM.Data;
using CrudWithoutORM.Common;

namespace CrudWithoutORM.Business
{
    public class TownsBusiness
    {
        private TownsData manager = new TownsData();
        public List<Town> GetAll()
        {
            return manager.GetAll();
        }

        public Town Get(int id)
        {
            return manager.Get(id);
        }

        public void Add(Town town)
        {
            manager.Add(town);
        }

        public void Update(Town town)
        {
            manager.Update(town);
        }

        public void Delete(int id)
        {
            manager.Delete(id);
        }
    }
}
