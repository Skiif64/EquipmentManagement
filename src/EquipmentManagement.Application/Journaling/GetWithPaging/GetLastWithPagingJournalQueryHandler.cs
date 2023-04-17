using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Application.Models;

namespace EquipmentManagement.Application.Journaling.GetWithPaging;
internal class GetLastWithPagingJournalQueryHandler
    : IQueryHandler<GetLastWithPagingJournalQuery, IEnumerable<JournalRecord>>
{
    private readonly IApplicationDbContext _context;

    public GetLastWithPagingJournalQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public Task<IEnumerable<JournalRecord>> Handle(GetLastWithPagingJournalQuery request, CancellationToken cancellationToken)
    {
        var records = _context
            .Set<JournalRecord>()
            .OrderByDescending(x => x.DateCreated)
            .Skip(request.Offset)
            .Take(request.Count);
        return Task.FromResult(records.AsEnumerable());
    }
}
