using Blog.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Database.ModelConfigurations
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Title).HasMaxLength(50);
            builder.Property(a => a.BlogId);
            builder.Property(a => a.Body);
            builder.Property(a => a.RowVersion).IsRowVersion();
        }
    }
}