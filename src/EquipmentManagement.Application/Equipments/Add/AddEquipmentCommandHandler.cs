using AutoMapper;
using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Domain.Models;

namespace EquipmentManagement.Application.Equipments.Add;

public class AddEquipmentCommandHandler : ICommandHandler<AddEquipmentCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public AddEquipmentCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
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
        await _context.Set<Equipment>().AddAsync(equipment,cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return equipment.Id;
    }
}
