using MySql.Data.MySqlClient;
using Microsoft.EntityFrameworkCore;

namespace HelloMaui
{
    public class Authorization : DbContext
    {
        public DbSet<User> Users { get; set; }

        public Authorization()
          : base()
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().Property(x => x.UserId).IsRequired();
            modelBuilder.Entity<User>().Property(x => x.FirstName).IsRequired();
            modelBuilder.Entity<User>().Property(x => x.LastName).IsRequired();
            modelBuilder.Entity<User>().Property(x => x.Password).IsRequired();
        }
    }
}
