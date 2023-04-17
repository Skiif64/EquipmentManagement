using EquipmentManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EquipmentManagement.DAL.EntityConfiguration;

internal class EquipmentTypeConfiguration : IEntityTypeConfiguration<Equipment>
{
    public void Configure(EntityTypeBuilder<Equipment> builder)
    {
        builder.Ignore(prop => prop.LastRecord);
    }
}
