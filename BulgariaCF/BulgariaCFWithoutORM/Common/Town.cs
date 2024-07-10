using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudWithoutORM.Common
{

    public class Town
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Town()
        {
            
        }
        public Town(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
    }
}
