using Cms.Data.Seeds;
using Cms.Entity;
using Microsoft.EntityFrameworkCore;

namespace Cms.Data
{
    public class CmsDbContext : DbContext
    {
        public CmsDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasMany(p => p.Languages)
                                           .WithOne(t => t.Category)
                                           .HasForeignKey(t => t.CategoryId)
                                           .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Category>().HasMany(p => p.Contents)
                                           .WithOne(t => t.Category)
                                           .HasForeignKey(t => t.CategoryId)
                                           .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Content>().HasMany(p => p.Languages)
                                          .WithOne(t => t.Content)
                                          .HasForeignKey(t => t.ContentId)
                                          .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Content>().HasOne(p => p.User)
                                           .WithMany(t => t.Contens)
                                           .HasForeignKey(t => t.UserId)
                                           .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>().HasIndex(p => p.UserName).IsUnique();

            modelBuilder.Entity<CategoryLanguage>().HasIndex(t => new { t.CategoryId, t.LanguageId }).IsUnique();
            modelBuilder.Entity<ContentLanguage>().HasIndex(t => new { t.ContentId, t.LanguageId, t.VariantId }).IsUnique();

            modelBuilder.Entity<SystemLanguage>().HasIndex(t => new { t.LanguageCode }).IsUnique();


            modelBuilder.ApplyConfiguration(new SystemLanguageSeed());
            modelBuilder.ApplyConfiguration(new CategorySeed());
            modelBuilder.ApplyConfiguration(new CategoryLanguageSeed());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryLanguage> CategoryLanguages { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<ContentLanguage> ContentLanguages { get; set; }
        public DbSet<SystemLanguage> SystemLanguages { get; set; }
    }
}
