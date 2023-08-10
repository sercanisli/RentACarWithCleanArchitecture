using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.EntityConfigurations
{
    public class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.ToTable("Cars").HasKey(b => b.Id);

            builder.Property(c => c.Id).HasColumnName("Id").IsRequired();
            builder.Property(c => c.ModelId).HasColumnName("ModelId").IsRequired();
            builder.Property(c => c.Kilometer).HasColumnName("Kilometer").IsRequired();
            builder.Property(c => c.CarState).HasColumnName("State");
            builder.Property(c => c.ModelYear).HasColumnName("ModelYear");

            builder.HasOne(c => c.Model);

            //Global Query Filter
            builder.HasQueryFilter(t => !t.DeletedDate.HasValue);
        }
    }
}
