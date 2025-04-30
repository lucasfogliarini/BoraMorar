using BoraMorar.Indices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoraMorar.Infrastructure.EntityConfigurations;

public class IndiceAplicadoConfiguration : IEntityTypeConfiguration<IndiceAplicado>
{
    public void Configure(EntityTypeBuilder<IndiceAplicado> builder)
    {
        builder.HasKey(o => o.Id);
    }
}
