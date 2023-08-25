using AutoMapper;
using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Application.Exceptions;
using EquipmentManagement.Application.Statuses;
using EquipmentManagement.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagement.Application.Equipments.Create;

public class CreateEquipmentCommandHandler : ICommandHandler<CreateEquipmentCommand, Guid>
{
    private const string CREATED_STATUS_NAME = "Создано";
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IJournal _journal;
    private readonly IPublisher _publisher;

    public CreateEquipmentCommandHandler(IApplicationDbContext context,
        IMapper mapper,
        IJournal journal,
        IPublisher publisher)
    {
        _context = context;
        _mapper = mapper;
        _journal = journal;
        _publisher = publisher;
    }

    public async Task<Guid> Handle(CreateEquipmentCommand request, CancellationToken cancellationToken)
    {
        var equipment = _mapper.Map<Equipment>(request);
        equipment.Images = new List<Image>();
        foreach (var imageName in request.ImageNames)
        {
            var image = new Image
            {
                Equipment = equipment,
                EquipmentId = equipment.Id,
                FullImagePath = imageName
            };
            equipment.Images.Add(image);
            await _context.Set<Image>().AddAsync(image, cancellationToken);
        }

        var status = await _context
            .Set<Status>()
            .SingleOrDefaultAsync(x => x.Title == CREATED_STATUS_NAME);

        if (status is null)
            await _publisher.Publish(new CreateStatusEvent(CREATED_STATUS_NAME));
        equipment.Type = await _context
            .Set<EquipmentType>()
            .FindAsync(new object[] { request.TypeId }, cancellationToken)
            ?? throw new NotFoundException("EquipmentType");

        await _context.Set<Equipment>().AddAsync(equipment, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        await _publisher.Publish(new EquipmentCreatedEvent(equipment));
        await _journal.WriteAsync(AppLogEvents.Create,
            $"Создано оборудование: {equipment.Type.Name} {equipment.Article} {equipment.SerialNumber}",
            cancellationToken);
        return equipment.Id;
    }
}
