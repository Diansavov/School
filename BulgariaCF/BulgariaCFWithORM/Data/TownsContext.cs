using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Model;


namespace Data
{

    public class TownsContext : DbContext
    {
        public TownsContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-VI3G3BQ\\SQLEXPRESS;Database = BulgariaCF; Integrated Security = true; ");            
        }
        public DbSet<Town> Towns { get; set; }
    }
}
