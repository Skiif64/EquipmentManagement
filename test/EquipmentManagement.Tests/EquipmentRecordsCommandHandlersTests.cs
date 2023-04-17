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
        

    }

    [Test, AutoData]
    public async Task WhenGetActualByEmployeeId_ShouldReturnActualRecords(Guid employeeId, Guid equipmentId)
    {
        
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
