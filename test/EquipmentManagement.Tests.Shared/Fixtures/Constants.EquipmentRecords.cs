using EquipmentManagement.Domain.Models;

namespace EquipmentManagement.Tests.Shared.Fixtures;
public static partial class Constants
{
    public static class EquipmentRecords
    {
        public static readonly Guid EquipmentRecordAId = Guid.NewGuid();
        public static readonly Guid EquipmentRecordBId = Guid.NewGuid();

        public static EquipmentRecord CreateA(DateTimeOffset createdAt)
            => new EquipmentRecord
            {
                Id = EquipmentRecordAId,
                Employee = Employees.CreateA(),
                EmployeeId = Employees.EmployeeAId,
                Equipment = Equipments.CreateA(),
                EquipmentId = Equipments.EquipmentAId,
                Modified = createdAt,
                ModifyAuthor = "Test user",
                Status = Statuses.CreateA(),
                StatusId = Statuses.StatusAId,
            };

        public static EquipmentRecord CreateB(DateTimeOffset createdAt)
            => new EquipmentRecord
            {
                Id = EquipmentRecordBId,
                Employee = Employees.CreateB(),
                EmployeeId = Employees.EmployeeBId,
                Equipment = Equipments.CreateB(),
                EquipmentId = Equipments.EquipmentBId,
                Modified = createdAt,
                ModifyAuthor = "Test user",
                Status = Statuses.CreateB(),
                StatusId = Statuses.StatusBId,
            };
    }
}
