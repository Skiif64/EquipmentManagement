using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Domain.Models;

namespace EquipmentManagement.Application.Images.RemoveByNames;
internal class RemoveImagesCommandHandler : ICommandHandler<RemoveImagesCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IImageStorage _imageStorage;
    public RemoveImagesCommandHandler(IApplicationDbContext context, IImageStorage imageStorage)
    {
        _context = context;
        _imageStorage = imageStorage;
    }
    public async Task Handle(RemoveImagesCommand request, CancellationToken cancellationToken)
    {
        var images = _context
            .Set<Image>()
            .Where(x => x.EquipmentId == request.EquipmentId)
            .AsEnumerable()
            .Where(x => request.ImageNames.Any(n => n == x.FullImagePath));

        _context
            .Set<Image>()
            .RemoveRange(images);

        await _imageStorage.RemoveImagesAsync(request.ImageNames, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
