using Windowsparty.Model;
using Microsoft.EntityFrameworkCore;

namespace WindowsParty.Data.DbContexts
{
    public class UserContexts : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(LocalDb)\MSSQLLocalDB;Database=WindowsParty;Trusted_Connection=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    UserName = "Martynas",
                    Password = "Martynas"
                },
                new User
                {
                    Id = 2,
                    UserName = "Jonas",
                    Password = "Jonas"
                }
            );
            base.OnModelCreating(modelBuilder);
        }
    }
}
