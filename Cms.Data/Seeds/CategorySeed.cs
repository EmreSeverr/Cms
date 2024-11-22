using Cms.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cms.Data.Seeds
{
    public class CategorySeed : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            var categories = new List<Category>()
            {
                new()
                {
                    Languages =
                    [
                        new()
                        {
                            Title = "Teknoloji",
                            LanguageId = 1
                        },
                        new()
                        {
                            Title = "Technology",
                            LanguageId = 2
                        }
                    ]
                },
                new()
                {
                    Languages =
                    [
                        new()
                        {
                            Title = "Sağlık",
                            LanguageId = 1
                        },
                        new()
                        {
                            Title = "Health",
                            LanguageId = 2
                        }
                    ]
                },
                new()
                {
                    Languages =
                    [
                        new()
                        {
                            Title = "Eğitim",
                            LanguageId = 1
                        },
                        new()
                        {
                            Title = "Education",
                            LanguageId = 2
                        }
                    ]
                },
                new()
                {
                    Languages =
                    [
                        new()
                        {
                            Title = "Ulaşım",
                            LanguageId = 1
                        },
                        new()
                        {
                            Title = "Transportation",
                            LanguageId = 2
                        }
                    ]
                }
            };

            builder.HasData(categories);
        }
    }
}
