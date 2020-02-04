using ASPNETCore_Angular.Entity;
using Microsoft.EntityFrameworkCore;

namespace Angular_ASPNETCore.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Produto> Produtos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=localhost;Initial Catalog=SiteMercado;persist security info=True;Integrated Security=SSPI;");
        }
    }
}
