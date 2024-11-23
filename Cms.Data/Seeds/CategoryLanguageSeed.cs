using Cms.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cms.Data.Seeds
{
    public class CategoryLanguageSeed : IEntityTypeConfiguration<CategoryLanguage>
    {
        public void Configure(EntityTypeBuilder<CategoryLanguage> builder)
        {
            var categoryLangugaes = new List<CategoryLanguage>()
            {
                new()
                {
                    Id = 1,
                    Title = "Teknoloji",
                    LanguageId = 1,
                    CategoryId = 1
                },
                new()
                {
                    Id = 2,
                    Title = "Technology",
                    LanguageId = 2,
                    CategoryId = 1
                },
                new()
                {
                    Id = 3,
                    Title = "Sağlık",
                    LanguageId = 1,
                    CategoryId = 2
                },
                new()
                {
                    Id = 4,
                    Title = "Health",
                    LanguageId = 2,
                    CategoryId = 2
                },
                new()
                {
                    Id = 5,
                    Title = "Eğitim",
                    LanguageId = 1,
                    CategoryId = 3,
                },
                new()
                {
                    Id = 6,
                    Title = "Education",
                    LanguageId = 2,
                    CategoryId = 3
                },
                new()
                {
                    Id = 7,
                    Title = "Ulaşım",
                    LanguageId = 1,
                    CategoryId = 4
                },
                new()
                {
                    Id = 8,
                    Title = "Transportation",
                    LanguageId = 2,
                    CategoryId = 4
                }
            };

            builder.HasData(categoryLangugaes);
        }
    }
}
