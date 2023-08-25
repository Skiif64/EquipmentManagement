using EquipmentManagement.Domain.Models;

namespace EquipmentManagement.Integration.Tests.Fixtures;
public static partial class Constants
{
    public static class EquipmentTypes
    {
        public static readonly Guid TypeAId = Guid.NewGuid();
        public static readonly Guid TypeBId = Guid.NewGuid();

        public const string TypeAName = "A";
        public const string TypeBName = "B";

        public static EquipmentType CreateA()
            => new EquipmentType
            {
                Id = TypeAId,
                Name = TypeAName,
            };

        public static EquipmentType CreateB()
            => new EquipmentType
            {
                Id = TypeBId,
                Name = TypeBName,
            };

    }
}
