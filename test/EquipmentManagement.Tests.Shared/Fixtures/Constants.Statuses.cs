using EquipmentManagement.Domain.Models;

namespace EquipmentManagement.Tests.Shared.Fixtures;
public static partial class Constants
{
    public static class Statuses
    {
        public static readonly Guid StatusAId = Guid.NewGuid();
        public static readonly Guid StatusBId = Guid.NewGuid();

        public const string StatusAName = "Status A";
        public const string StatusBName = "Status B";

        public static Status CreateA()
            => new Status
            {
                Id = StatusAId,
                Title = StatusAName,
            };

        public static Status CreateB()
            => new Status
            {
                Id = StatusBId,
                Title = StatusAName,
            };
    }
}
