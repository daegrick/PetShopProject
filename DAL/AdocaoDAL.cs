using DTO;
using Microsoft.Data.SqlClient;

namespace DAL
{
    public class AdocaoDAL
    {
        public static string Adotar(Pessoa pessoa, IEnumerable<Pet> pets)
        {
            try
            {
                string query = "INSERT INTO PetsDaPessoa (CodigoPessoa, IdePessoa, CodigoPet, IdePet) VALUES (@CodigoPessoa, @IdePessoa, @CodigoPet, @IdePet);";
                using var conn = AcessoDB.DBAccess();
                using var transaction = conn.BeginTransaction();
                try
                {
                    ApagaRegistrosAntigos(conn, transaction, pessoa);
                    InserePetsNovos(conn, query, transaction, pessoa, pets);
                    transaction.Commit();
                    AcessoDB.CloseConnection();
                    return "Pets adotados com sucesso!";
                }catch(SqlException ex)
                {
                    transaction.Rollback();
                    return ExceptionManager.TrataException(ex);
                }
            }catch(SqlException ex)
            {
               return ExceptionManager.TrataException(ex);
            }
            finally
            {
                AcessoDB.CloseConnection();
            }
        }
        private static void InserePetsNovos(SqlConnection conn, string query, SqlTransaction transaction, Pessoa pessoa, IEnumerable<Pet> pets)
        {
            using var command = new SqlCommand(query, conn);
            command.Transaction = transaction;
            foreach (var pet in pets)
            {
                command.Parameters.Clear();
                string[] names = new string[] { "@CodigoPessoa", "@IdePessoa", "@CodigoPet", "@IdePet" };
                object[] values = new object[] { pessoa.Codigo, pessoa.Ide, pet.Codigo, pet.Ide };
                AcessoDB.FillParameters(command, names, values);
                AcessoDB.ExecuteCommand(command);
            }
        }
        private static void ApagaRegistrosAntigos(SqlConnection conn, SqlTransaction transaction, Pessoa pessoa)
        {
            string query = "DELETE FROM PetsDaPessoa WHERE IdePessoa = @Ide";
            string[] names = new string[] { "@Ide" };
            object[] values = new object[] { pessoa.Ide };
            using var command = new SqlCommand(query, conn);
            command.Transaction = transaction;
            AcessoDB.FillParameters(command, names, values);
            AcessoDB.ExecuteCommand(command);
        }
    }
}
