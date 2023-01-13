using DTO;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DAL.Factory
{
    internal class RacaFactory
    {
        internal static Raca Get(SqlDataReader reader)
        {
            return new()
            {
                Codigo = reader.GetInt32("Codigo"),
                Nome = reader.GetString("Nome"),
                Ide = reader.GetGuid("Ide")
            };
        }  
    }
}
