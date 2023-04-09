using Blog.Data.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogBE.Data.Configurations
{
    public class ArticleConfiguration : IEntityTypeConfiguration<Article>
    {
        public ArticleConfiguration()
        {
        }

        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.Property(x => x.Title).IsRequired(true).HasMaxLength(100);
            builder.Property(x => x.Content).IsRequired(true).HasMaxLength(50);

        }
    }
}

