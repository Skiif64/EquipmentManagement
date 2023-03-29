using AutoMapper;
using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Domain.Models;

namespace EquipmentManagement.Application.Statuses.Add;

public class AddStatusCommandHandler : ICommandHandler<AddStatusCommand, Status>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public AddStatusCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Status> Handle(AddStatusCommand request, CancellationToken cancellationToken)
    {
        var status = _mapper.Map<Status>(request);
        await _context.Set<Status>().AddAsync(status, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return status;
    }
}
