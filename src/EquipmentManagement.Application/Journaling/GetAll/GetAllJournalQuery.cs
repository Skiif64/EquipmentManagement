using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Application.Models;

namespace EquipmentManagement.Application.Journaling.GetAll;
public class GetAllJournalQuery : IQuery<IEnumerable<JournalRecord>>
{
}
