using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Application.Equipments.Create;
using EquipmentManagement.Application.Exceptions;
using EquipmentManagement.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagement.Application.EquipmentRecords;
internal sealed class EquipmentCreatedEventHandler : INotificationHandler<EquipmentCreatedEvent>
{
    private readonly IApplicationDbContext _context;

    public EquipmentCreatedEventHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(EquipmentCreatedEvent notification, CancellationToken cancellationToken)
    {
        var status = await _context.Set<Status>()
            .SingleOrDefaultAsync(x => x.Title == "Создано", cancellationToken)
            ?? throw new NotFoundException("Status");
        await _context.Set<EquipmentRecord>().AddAsync(new EquipmentRecord
        {
            Equipment = notification.Equipment,
            EquipmentId = notification.Equipment.Id,
            Modified = DateTimeOffset.UtcNow,
            ModifyAuthor = notification.Equipment.Author,
            Status = status,
            StatusId = status.Id,            
        },
        cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
