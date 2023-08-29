using EquipmentManagement.Domain.Models;

namespace EquipmentManagement.Integration.Tests.Fixtures;
public static partial class Constants
{
    public static class Employees
    {
        public static readonly Guid EmployeeAId = Guid.NewGuid();
        public static readonly Guid EmployeeBId = Guid.NewGuid();

        public static Employee CreateA()
            => new Employee
            {
                Id = EmployeeAId,
                Firstname = "Test A",
                Lastname = "Testoviy A",                
            };

        public static Employee CreateB()
            => new Employee
            {
                Id = EmployeeBId,
                Firstname = "Test B",
                Lastname = "Testoviy B",
            };
    }
}
