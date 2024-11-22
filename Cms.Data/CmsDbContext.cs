using Cms.Data.Seeds;
using Cms.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Cms.Data
{
    public class CmsDbContext : IdentityDbContext<CmsUser, IdentityRole<Guid>, Guid>
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

            modelBuilder.Entity<Content>().HasOne(p => p.CmsUser)
                                           .WithMany(t => t.Contens)
                                           .HasForeignKey(t => t.CmsUserId)
                                           .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CategoryLanguage>().HasIndex(t => new { t.CategoryId, t.LanguageId }).IsUnique();
            modelBuilder.Entity<ContentLanguage>().HasIndex(t => new { t.ContentId, t.LanguageId }).IsUnique();

            modelBuilder.Entity<SystemLanguage>().HasIndex(t => new { t.LanguageCode }).IsUnique();


            modelBuilder.ApplyConfiguration(new SystemLanguageSeed());
            modelBuilder.ApplyConfiguration(new CategorySeed());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryLanguage> CategoryLanguages { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<ContentLanguage> ContentLanguages { get; set; }
        public DbSet<SystemLanguage> SystemLanguages { get; set; }
    }
}
