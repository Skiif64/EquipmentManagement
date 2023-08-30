using EquipmentManagement.Domain.Models;

namespace EquipmentManagement.Tests.Shared.Fixtures;
public static partial class Constants
{
    public static class Equipments
    {
        public static readonly Guid EquipmentAId = Guid.NewGuid();
        public static readonly Guid EquipmentBId = Guid.NewGuid();

        public static Equipment CreateA()
            => new Equipment
            {
                Id = EquipmentAId,
                Article = "Test equip A",
                Author = "Test user",
                CreatedAt = DateTimeOffset.MinValue,
                SerialNumber = "serail",
                Type = EquipmentTypes.CreateA(),
                TypeId = EquipmentTypes.TypeAId,
            };

        public static Equipment CreateB()
            => new Equipment
            {
                Id = EquipmentAId,
                Article = "Test equip B",
                Author = "Test user",
                CreatedAt = DateTimeOffset.MinValue,
                SerialNumber = "serail",
                Type = EquipmentTypes.CreateB(),
                TypeId = EquipmentTypes.TypeBId,
            };
    }
}
