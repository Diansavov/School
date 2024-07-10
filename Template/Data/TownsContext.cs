using Microsoft.EntityFrameworkCore;
using Model;


namespace Data
{
    //Smeni TownsContext
    public class TownsContext : DbContext
    {
        public TownsContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Smeni server i imeto na Database
            optionsBuilder.UseSqlServer("Server=DELLPC;Database = TownsCF; Integrated Security = true;TrustServerCertificate=True ");   
        }
        //Smeni Towns i Countires
        public DbSet<Town> Towns { get; set; }
        public DbSet<Country> Countries { get; set; }
    }
}
