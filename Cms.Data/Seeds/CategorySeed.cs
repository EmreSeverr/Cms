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
                    Id = 1
                },
                new()
                {
                    Id = 2
                },
                new()
                {
                    Id = 3
                },
                new()
                {
                    Id = 4
                }
            };

            builder.HasData(categories);
        }
    }
}
