using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_static.Models
{

    public class NuovaPizzaContext : DbContext
    {
        public DbSet<NuovaPizza> nuovapizzas { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=pizzaDB;Integrated Security=True;Pooling=False");
        }
    }
}
