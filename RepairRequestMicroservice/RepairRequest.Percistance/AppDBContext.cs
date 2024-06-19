using Microsoft.EntityFrameworkCore;
using RepairRequest.Domain;

namespace RepairRequest.Percistance
{
    public class AppDBContext : DbContext
    {

        public DbSet<AppUser> Users { get; set; }
        public DbSet<RepairRequestEntity> Requests { get; set; }
        public DbSet<Component> Components { get; set; }
        public DbSet<ServiceEntity> Services { get; set; }
        public DbSet<ServiceCenterEntity> ServicesCenters { get; set; }
        public DbSet<RequestStatus> RequestStatuses { get; set; }
        public DbSet<RepairRequestComponent> RepairRequestsComponent { get; set; }
        public DbSet<RepairRequestService> RepairRequestsService { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

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

            modelBuilder.Entity<RefreshToken>().HasKey(c => c.Id);
            modelBuilder.Entity<RefreshToken>().Property(e => e.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<RefreshToken>().HasOne(c => c.User).WithMany().HasForeignKey(e => e.UserId);

            modelBuilder.Entity<RepairRequestEntity>().HasKey(u => u.Id);
            modelBuilder.Entity<RepairRequestEntity>().HasOne(e => e.Master);
            modelBuilder.Entity<RepairRequestEntity>().HasOne(e => e.User);
            modelBuilder.Entity<RepairRequestEntity>().HasOne(e => e.RequestStatus);

            modelBuilder.Entity<RepairRequestComponent>().HasKey(c => c.Id);
            modelBuilder.Entity<RepairRequestComponent>().HasOne(c => c.Component);
            modelBuilder.Entity<RepairRequestComponent>().HasOne(c => c.RepairRequest);


            modelBuilder.Entity<RepairRequestService>().HasKey(c => c.Id);
            modelBuilder.Entity<RepairRequestService>().HasOne(c => c.Service);
            modelBuilder.Entity<RepairRequestService>().HasOne(c => c.RepairRequest);


            base.OnModelCreating(modelBuilder);
        }
    }
}
