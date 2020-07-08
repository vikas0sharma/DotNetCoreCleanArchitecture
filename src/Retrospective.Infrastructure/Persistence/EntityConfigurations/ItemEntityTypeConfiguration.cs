using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Retrospective.Domain.AggregatesModel.SprintAggregate;

namespace Retrospective.Infrastructure.Persistence.EntityConfigurations
{
    public class ItemEntityTypeConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder
               .ToTable("Items", SprintContext.DEFAULT_SCHEMA);

            builder.HasNoKey();
            builder.Property(i => i.Title);
            builder.Property(i => i.Description);
            builder.Property(i => i.AddedBy);
        }
    }
}
