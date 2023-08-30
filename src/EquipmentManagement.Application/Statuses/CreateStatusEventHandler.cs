using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Domain.Models;
using MediatR;

namespace EquipmentManagement.Application.Statuses;
internal sealed class CreateStatusEventHandler : INotificationHandler<CreateStatusEvent>
{
    private readonly IApplicationDbContext _context;

    public CreateStatusEventHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(CreateStatusEvent notification, CancellationToken cancellationToken)
    {
        await _context.Set<Status>().AddAsync(new Status
        {
            Title = notification.Title,
        },
        cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
