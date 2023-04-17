using AutoMapper;
using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Domain.Models;

namespace EquipmentManagement.Application.EquipmentTypes.Add;
internal class AddEquipmentTypeCommandHandler : ICommandHandler<AddEquipmentTypeCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public AddEquipmentTypeCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<Guid> Handle(AddEquipmentTypeCommand request, CancellationToken cancellationToken)
    {
        var type = _mapper.Map<EquipmentType>(request);
        await _context
            .Set<EquipmentType>()
            .AddAsync(type);
        await _context.SaveChangesAsync(cancellationToken);
        return type.Id;
    }
}
