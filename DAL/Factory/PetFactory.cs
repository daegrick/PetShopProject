using DTO;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DAL.Factory
{
    internal class PetFactory
    {
        internal static Pet Get(SqlDataReader reader, bool isFromAdotado = true)
        {
            return new()
            {
                Codigo = reader.GetInt32("Codigo"),
                Nome = reader.GetString("Nome"),
                DataNascimento = reader.GetDateTime("DataNascimento"),
                Sexo = reader.GetString("Sexo"),
                QuantidadeVacinas = reader.GetInt32("QuantidadeVacinas"),
                Ide = reader.GetGuid("Ide"),
                RacaIde = reader.GetGuid("IdeRaca"),
                IsAdotado = !isFromAdotado || reader.GetInt32("IsAdotado") == 1
            };
        }
    }
}
