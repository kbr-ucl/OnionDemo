using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Database.ModelConfigurations
{
    public class BlogConfiguration : IEntityTypeConfiguration<Domain.Model.Blog>
    {
        public void Configure(EntityTypeBuilder<Domain.Model.Blog> builder)
        {
            builder.HasKey(a => a.Id);
        }
    }
}