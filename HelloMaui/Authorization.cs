using MySql.Data.MySqlClient;
using Microsoft.EntityFrameworkCore;

namespace HelloMaui
{
    class Authorization : DbContext
    {
        public DbSet<User> Users { get; set; }

        public Authorization()
          : base()
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId);
                entity.Property(e => e.FirstName).IsRequired();
                entity.Property(e => e.LastName).IsRequired();
                entity.Property(e => e.Password).IsRequired();
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseMySQL("server=localhost;port=3306;database=HelloMaui;user=root;password=00000000");
    }
}
