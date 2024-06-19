using Microsoft.EntityFrameworkCore;
using Users.Domain;

namespace Users.Percistance
{
    public class AppDBContext : DbContext
    {

        public DbSet<AppUser> Users { get; set; }

        public AppDBContext(DbContextOptions<AppDBContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<AppUser>().HasKey(u => u.Id);
            modelBuilder.Entity<AppUser>().Property(b => b.Login).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<AppUser>().HasIndex(u => u.Login).IsUnique();

            modelBuilder.Entity<AppUser>().HasMany(e => e.AppUserAppRoles);
            modelBuilder.Entity<AppUserRole>().HasMany(e => e.AppUserAppRole);

            modelBuilder.Entity<AppUserAppRole>().HasOne(e => e.Role);
            modelBuilder.Entity<AppUserAppRole>().HasOne(e => e.User);

            //modelBuilder.Entity<RepairRequestEntity>().HasOne(e => e.User);


            base.OnModelCreating(modelBuilder);
        }
    }
}
