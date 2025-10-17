using Microsoft.EntityFrameworkCore;
using UserApi.Models;

namespace UserApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<User> Users { get; set; }
        public DbSet<UserDetails> UsersDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Users table
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");        
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name).HasColumnName("name");
                entity.Property(e => e.Email).HasColumnName("email");
            });

            // UsersDetails table
            modelBuilder.Entity<UserDetails>(entity =>
            {
                entity.ToTable("users_details", schema: "magnis");
                entity.HasKey(e => e.UserId);
                entity.HasIndex(e => e.UserEmail).IsUnique();

                entity.Property(e => e.UserId).HasColumnName("user_id");
                entity.Property(e => e.UserName).HasColumnName("user_name");
                entity.Property(e => e.UserEmail).HasColumnName("user_email");
                entity.Property(e => e.ExpirationDate).HasColumnName("expiration_date");
                entity.Property(e => e.IsFirstTimeLogging).HasColumnName("is_first_time_logging");
                entity.Property(e => e.IsExternal).HasColumnName("is_external");
                entity.Property(e => e.Active).HasColumnName("active");
                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
                entity.Property(e => e.ModifiedOn).HasColumnName("modified_on");
            });
        }
    }
}
