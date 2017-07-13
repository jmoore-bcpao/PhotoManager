using BCPAO.PhotoManager.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BCPAO.PhotoManager.Data
{
    public class DatabaseContext : IdentityDbContext<User, Role, int>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        //public DbSet<BuildingPhoto> BuildingPhotos { get; set; }
        //public DbSet<FileDescription> FileDescriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRoleClaim<int>>(entity =>
            {
                entity.ToTable("RoleClaims");
            });

            builder.Entity<Role>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName).HasName("RoleNameIndex");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);

                entity.HasMany(d => d.RolePermissions).WithOne(p => p.Role).HasForeignKey(d => d.RoleId);

                entity.ToTable("Roles");
            });

            builder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail).HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName).HasName("UserNameIndex");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
                
                //entity.HasMany(d => d.DeliveredPackages).WithOne(p => p.DeliveredBy).HasForeignKey(d => d.DeliveredByUserId);
                //entity.HasMany(d => d.AssignedPackages).WithOne(p => p.AssignedTo).HasForeignKey(d => d.AssignedToUserId);
                //entity.HasMany(d => d.Payments).WithOne(p => p.User).HasForeignKey(d => d.UserId);
                entity.HasMany(d => d.UserPermissions).WithOne(p => p.User).HasForeignKey(d => d.UserId);
                //entity.HasMany(d => d.CreatedBookings).WithOne(p => p.CreatedBy).HasForeignKey(d => d.CreatedByUserId);

                entity.ToTable("Users");
            });

            builder.Entity<IdentityUserClaim<int>>(entity =>
            {
                entity.ToTable("UserClaims");
            });

            builder.Entity<IdentityUserLogin<int>>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.Property(e => e.LoginProvider).HasMaxLength(450);

                entity.Property(e => e.ProviderKey).HasMaxLength(450);

                entity.ToTable("UserLogins");
            });

            builder.Entity<IdentityUserRole<int>>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });
                entity.ToTable("UserRoles");
            });
            
            builder.Entity<Permission>(entity =>
            {
                entity.HasIndex(e => e.Name).HasName("UK_Permission").IsUnique();

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.Group)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsRequired();
                entity.ToTable("Permissions");
            });

            builder.Entity<Setting>(entity =>
            {
                entity.HasKey(e => e.Name);

                entity.Property(e => e.DisplayName)
                    .HasColumnType("text");

                entity.Property(e => e.Value)
                    .HasColumnType("text");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsRequired();
                entity.ToTable("Settings");
            });

            builder.Entity<RolePermission>(entity =>
            {
                entity.HasKey(e => new { e.RoleId, e.PermissionId });

                entity.HasOne(d => d.Permission).WithMany(p => p.RolePermissions).HasForeignKey(d => d.PermissionId);

                entity.HasOne(d => d.Role).WithMany(p => p.RolePermissions).HasForeignKey(d => d.RoleId);
                entity.ToTable("RolePermissions");
            });

            builder.Entity<UserPermission>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.PermissionId });

                entity.HasOne(d => d.Permission).WithMany(p => p.UserPermissions).HasForeignKey(d => d.PermissionId);

                entity.HasOne(d => d.User).WithMany(p => p.UserPermissions).HasForeignKey(d => d.UserId);
                entity.ToTable("UserPermissions");
            });

            builder.Entity<Photo>(entity =>
            {
                entity.HasKey(e => e.PhotoId);
                entity.Property(e => e.PropertyId);
                entity.Property(e => e.BuildingId);
                entity.Property(e => e.BuildingSeq);
                entity.Property(e => e.ImageData);
                entity.Property(e => e.ImageName).HasMaxLength(20);
                entity.Property(e => e.ImageSize);
                entity.Property(e => e.DateTaken);
                entity.Property(e => e.UploadedDate).IsRequired();
                entity.Property(e => e.UploadedBy).HasMaxLength(20);
                entity.Property(e => e.MasterPhoto);
                entity.Property(e => e.FrontPhoto);
                entity.Property(e => e.PublicPhoto);
                entity.Property(e => e.Status).HasMaxLength(10);
                entity.Property(e => e.Active);

                entity.ToTable("Photos");
            });
        }
        
        public virtual new DbSet<Role> Roles { get; set; }
        public virtual new DbSet<User> Users { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<Setting> Settings { get; set; }
        public virtual DbSet<UserPermission> UserPermissions { get; set; }
        public virtual DbSet<RolePermission> RolePermissions { get; set; }
        public virtual DbSet<Photo> Photos { get; set; }
    }
}
