using Cms.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cms.Data.Seeds
{
    public class SystemLanguageSeed : IEntityTypeConfiguration<SystemLanguage>
    {
        public void Configure(EntityTypeBuilder<SystemLanguage> builder)
        {
            var systemLanguages = new List<SystemLanguage>()
            {
                new()
                {
                    Id = 1,
                    LanguageCode = "tr-TR"
                },
                new()
                {
                    Id = 2,
                    LanguageCode = "en-US"
                }
            };

            builder.HasData(systemLanguages);
        }
    }
}
