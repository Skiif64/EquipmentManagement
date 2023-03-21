using AutoFixture.NUnit3;
using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Application.EquipmentRecords.GetActualsByEmployeeID;
using EquipmentManagement.Domain.Models;
using EquipmentManagement.Tests.Fixtures;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Diagnostics;

namespace EquipmentManagement.Tests;

internal class EquipmentRecordsCommandHandlersTests
{
    private readonly Mock<IApplicationDbContext> _contextMock;
    private readonly Mock<DbSet<EquipmentRecord>> _equipmentSetMock;
    
    public EquipmentRecordsCommandHandlersTests()
    {
        _contextMock = new Mock<IApplicationDbContext>();
        _equipmentSetMock = new Mock<DbSet<EquipmentRecord>>();        
    }

    [SetUp]
    public void ClearMocks()
    {
        _contextMock.Reset();
        _equipmentSetMock.Reset();
    }

    [Test, AutoData]
    public async Task WhenGetActualByEmployeeId__ShouldReturn3Records(Guid employeeId1, Guid employeeId2, Guid employeeId3)
    {
        var sut = new GetActualsByEmployeeIdQueryHandler(_contextMock.Object);
        var records =
            EquipmentRecordsFixtures.CreateMany(3, employeeId1)
            .Concat(EquipmentRecordsFixtures.CreateMany(3, employeeId2))
            .Concat(EquipmentRecordsFixtures.CreateMany(3, employeeId3))
            .AsQueryable();

        SetupSet(_equipmentSetMock.As<IQueryable<EquipmentRecord>>(), records);

        _contextMock.Setup(x => x.Set<EquipmentRecord>())
            .Returns(_equipmentSetMock.Object);
        var request = new GetActualsByEmployeeIdQuery(employeeId1);


        var actual = await sut.Handle(request, default);

        Assert.That(actual, Is.Not.Null);
        CollectionAssert.AllItemsAreNotNull(actual);
        Assert.That(actual.Count(), Is.EqualTo(3));

    }

    [Test, AutoData]
    public async Task WhenGetActualByEmployeeId_ShouldReturnActualRecords(Guid employeeId, Guid equipmentId)
    {
        var sut = new GetActualsByEmployeeIdQueryHandler(_contextMock.Object);
        var records =
            EquipmentRecordsFixtures.CreateMany(3, employeeId, equipmentId)            
            .AsQueryable();
        
        SetupSet(_equipmentSetMock.As<IQueryable<EquipmentRecord>>(), records);

        _contextMock.Setup(x => x.Set<EquipmentRecord>())
            .Returns(_equipmentSetMock.Object);
        var request = new GetActualsByEmployeeIdQuery(employeeId);

        var newest = records.Max(x => x.Modified);

        var actual = await sut.Handle(request, default);
        
        Assert.That(actual, Is.Not.Null);
        Assert.That(actual.Count, Is.EqualTo(1));
        CollectionAssert.AllItemsAreNotNull(actual);
        Assert.That(actual.First().Modified, Is.EqualTo(newest));
        Assert.That(actual.First().Employee.Id, Is.EqualTo(employeeId));
        Assert.That(actual.First().Equipment.Id, Is.EqualTo(equipmentId));
    }

    private void SetupSet<T>(Mock<IQueryable<T>> set, IQueryable<T> data)
    {
        set
            .Setup(x => x.Provider)
            .Returns(data.Provider);
        set
            .Setup(x => x.Expression)
            .Returns(data.Expression);
        set
            .Setup(x => x.ElementType)
            .Returns(data.ElementType);
        set
            .Setup(x => x.GetEnumerator())
            .Returns(data.GetEnumerator());
    }
}
