namespace EquipmentManagement.Application.Abstractions;
public interface IJournalFactory
{
    IJournal Create(string username);
    IJournal Get();
}
