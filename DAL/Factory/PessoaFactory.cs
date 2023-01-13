using DTO;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DAL.Factory
{
    internal class PessoaFactory
    {
        internal static Pessoa Get(SqlDataReader reader)
        {
            return new()
            {
                Codigo = reader.GetInt32("Codigo"),
                Nome = reader.GetString("Nome"),
                Nascimento = reader.GetDateTime("DataNascimento"),
                Sexo = reader.GetString("Sexo"),
                Ide = reader.GetGuid("Ide")
            };
        }
    }
}
