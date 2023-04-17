using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Application.Models;

namespace EquipmentManagement.Application.Journaling.GetAll;
internal class GetAllJournalQueryHandler : IQueryHandler<GetAllJournalQuery, IEnumerable<JournalRecord>>
{
    private readonly IApplicationDbContext _context;

    public GetAllJournalQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public Task<IEnumerable<JournalRecord>> Handle(GetAllJournalQuery request, CancellationToken cancellationToken)
    {
        var records = _context
            .Set<JournalRecord>();
        return Task.FromResult(records.AsEnumerable());
    }
}
