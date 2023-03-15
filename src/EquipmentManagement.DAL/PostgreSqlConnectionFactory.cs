using Npgsql;
using System.Data;

namespace EquipmentManagement.DAL;

internal class PostgreSqlConnectionFactory : IDbConnectionFactory
{
    public IDbConnection Create(string connectionString)
    {
        return new NpgsqlConnection(connectionString);
    }
}
