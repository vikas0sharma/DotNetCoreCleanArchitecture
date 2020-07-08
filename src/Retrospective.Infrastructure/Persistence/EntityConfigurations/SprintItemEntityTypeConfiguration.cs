using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Retrospective.Domain.AggregatesModel.SprintAggregate;

namespace Retrospective.Infrastructure.Persistence.EntityConfigurations
{
    public class SprintItemEntityTypeConfiguration :
        IEntityTypeConfiguration<SprintItem>
    {
        public void Configure(EntityTypeBuilder<SprintItem> builder)
        {
            builder
               .ToTable("SprintItems", SprintContext.DEFAULT_SCHEMA);

            builder.HasKey(b => b.Id);

            builder.Property(b => b.Category);
            builder.Property(b => b.SprintItemType);
        }
    }
}
