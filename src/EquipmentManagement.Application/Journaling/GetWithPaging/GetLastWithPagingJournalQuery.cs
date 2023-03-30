using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Application.Models;

namespace EquipmentManagement.Application.Journaling.GetWithPaging;
public class GetLastWithPagingJournalQuery : IQuery<IEnumerable<JournalRecord>>
{
    public int Count { get; set; }
    public int Offset { get; set; }

    public GetLastWithPagingJournalQuery(int count, int offset)
    {
        Count = count;
        Offset = offset;
    }
}
