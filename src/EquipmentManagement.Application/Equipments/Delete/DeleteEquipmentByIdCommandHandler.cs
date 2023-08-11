using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagement.Application.Equipments.Delete;
internal class DeleteEquipmentByIdCommandHandler : ICommandHandler<DeleteEquipmentByIdCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteEquipmentByIdCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteEquipmentByIdCommand request, CancellationToken cancellationToken)
    {
        var recordsToDelete = _context
            .Set<EquipmentRecord>()
            .Where(x => x.EquipmentId == request.Id);

        _context
            .Set<EquipmentRecord>()
            .RemoveRange(recordsToDelete);

        var equipmentToDelete = await _context
            .Set<Equipment>()
            .SingleAsync(x => x.Id == request.Id);

        _context
            .Set<Equipment>()
            .Remove(equipmentToDelete);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
