using ContosoUniversity.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ContosoUniversity.DAL
{
    public class ProjectContext : DbContext

    {

        public ProjectContext() : base("ProjectContext")
        {
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Point> Points { get; set; }
        public DbSet<Restaurant> Restaurants{ get; set; }
        public DbSet<Statistic> Statistics { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
