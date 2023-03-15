using System.Data;

namespace EquipmentManagement.DAL;

internal interface IDbConnectionFactory
{
    IDbConnection Create(string connectionString);
}
