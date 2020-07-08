using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Retrospective.Domain.AggregatesModel.SprintAggregate;

namespace Retrospective.Infrastructure.Persistence.EntityConfigurations
{
    public class SprintEntityTypeConfiguration : IEntityTypeConfiguration<Sprint>
    {
        public void Configure(EntityTypeBuilder<Sprint> builder)
        {
            builder
               .ToTable("Sprints", SprintContext.DEFAULT_SCHEMA);

            builder.HasKey(s => s.Id);

            builder.Property(s => s.CreatedBy);
            builder.Property(s => s.CreatedOn);
        }
    }
}
