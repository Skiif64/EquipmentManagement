using AutoMapper;
using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Application.Exceptions;
using EquipmentManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagement.Application.Equipments.Add;

public class AddEquipmentCommandHandler : ICommandHandler<AddEquipmentCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IJournal _journal;

    public AddEquipmentCommandHandler(IApplicationDbContext context, IMapper mapper, IJournal journal)
    {
        _context = context;
        _mapper = mapper;
        _journal = journal;
    }

    public async Task<Guid> Handle(AddEquipmentCommand request, CancellationToken cancellationToken)
    {
        var equipment = _mapper.Map<Equipment>(request);
        equipment.Images = new List<Image>();
        foreach(var imageName in request.ImageNames!)
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
        equipment.Type = await _context
            .Set<EquipmentType>()
            .FindAsync(new object[] { request.TypeId }, cancellationToken)
            ?? throw new NotFoundException("EquipmentType");
        await _context.Set<Equipment>().AddAsync(equipment,cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        await _journal.WriteAsync(AppLogEvents.Create,
            $"Создано оборудование: {equipment.Type.Name} {equipment.Article} {equipment.SerialNumber}",
            cancellationToken);
        return equipment.Id;
    }
}
