using AutoMapper;
using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Application.Exceptions;
using EquipmentManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagement.Application.Equipments.Update;
internal class UpdateEquipmentCommandHandler : ICommandHandler<UpdateEquipmentCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IJournal _journal;

    public UpdateEquipmentCommandHandler(IApplicationDbContext context, IMapper mapper, IJournal journal)
    {
        _context = context;
        _mapper = mapper;
        _journal = journal;
    }
    public async Task<Guid> Handle(UpdateEquipmentCommand request, CancellationToken cancellationToken)
    {//TODO: use LastModified
        var equipment = await _context
            .Set<Equipment>()
            .Where(x => x.Id == request.EquipmentId)
            .Include(x => x.Images)
            .Include(x => x.Records)            
            .SingleOrDefaultAsync(x => x.Id == request.EquipmentId, cancellationToken)
            ?? throw new NotFoundException("Equipment");

        var type = await _context
            .Set<EquipmentType>()
            .FindAsync(new object[] { request.TypeId }, cancellationToken)
            ?? throw new NotFoundException("EquipmentType");        

        var existingImages = _context
            .Set<Image>()
            .Where(x => x.EquipmentId == request.EquipmentId)
            .AsEnumerable();

        var imagesToDelete = existingImages
            .Where(x => request.ImageToRemove.Contains(x.FullImagePath));
        
        
        if (imagesToDelete.Count() != 0)
        {
            foreach (var image in imagesToDelete)
            {
                equipment.Images.Remove(image);
            }
            _context
                .Set<Image>()
                .RemoveRange(imagesToDelete);
        }
        if (request.NewImages.Count() != 0)
        {
            var newImagesEntities = request.NewImages.Select(img
            => new Image
            {
                Equipment = equipment,
                EquipmentId = request.EquipmentId,
                FullImagePath = img
            });            
            await _context
                .Set<Image>()
                .AddRangeAsync(newImagesEntities, cancellationToken);
        }

        _context
            .Set<Equipment>()
            .Update(equipment);
        
        equipment.Article = request.Article;
        equipment.SerialNumber = request.SerialNumber;
        equipment.Type = type;
        await _context.SaveChangesAsync(cancellationToken);
        return equipment.Id;
    }
}
