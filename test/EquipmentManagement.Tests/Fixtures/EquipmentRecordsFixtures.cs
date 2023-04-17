using AutoFixture;
using EquipmentManagement.Domain.Models;

namespace EquipmentManagement.Tests.Fixtures;

public static class EquipmentRecordsFixtures
{
    private class TestEquipmentRecord : EquipmentRecord
    {
        public TestEquipmentRecord() : base()
        { }
    }
    private static readonly Fixture _fixture = new();
    public static EquipmentRecord Create()
        => _fixture.Build<EquipmentRecord>()                
        .Create<TestEquipmentRecord>();


    static EquipmentRecordsFixtures()
    {
        _fixture.Register<EquipmentRecord>(() => new TestEquipmentRecord());
    }

    public static EquipmentRecord Create(Guid employeeId)
    {
        var record = Create();        
        return record;
    }
    public static EquipmentRecord Create(Guid employeeId, Guid equipmentId)
    {
        var record = Create();        
        return record;
    }


    public static IEnumerable<EquipmentRecord> CreateMany(int count)
        => Enumerable.Range(0, count).Select(i => Create());

    public static IEnumerable<EquipmentRecord> CreateMany(int count, Guid employeeId)
        => Enumerable.Range(0, count).Select(i => Create(employeeId));
    public static IEnumerable<EquipmentRecord> CreateMany(int count, Guid employeeId, Guid equipmentId)
        => Enumerable.Range(0, count).Select(i => Create(employeeId, equipmentId));
}
