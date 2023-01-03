using DTO;
using Microsoft.Data.SqlClient;
using System.Text;
using System.Data;

namespace DAL
{
    public class RacaDAL
    {
        public static IEnumerable<Raca> Busca()
        {
            List<Raca> racas = new();

            string query = "SELECT Codigo, Nome, Ide FROM Raca ORDER BY Nome;";
            using var conn = AcessoDB.DBAccess();
            using var command = new SqlCommand(query, conn);
            using var reader = AcessoDB.Read(command);
            while (reader.Read())
            {
                Raca raca = new()
                {
                    Codigo = reader.GetInt32("Codigo"),
                    Nome = reader.GetString("Nome"),
                    Ide = reader.GetGuid("Ide")
                };
                racas.Add(raca);
            }
            AcessoDB.CloseConnection();
            return racas;
        }

        public static Dictionary<Guid, string> BuscaDictionary()
        {
            Dictionary<Guid, string> retorno = new();
            string query = "SELECT Ide, Nome FROM Raca ORDER BY Nome;";
            try
            {
                using var conn = AcessoDB.DBAccess();
                using var command = new SqlCommand(query, conn);
                using var reader = AcessoDB.Read(command);
                while (reader.Read())
                {
                    retorno.Add(reader.GetGuid("Ide"), reader.GetString("Nome"));
                }
                AcessoDB.CloseConnection();
            } catch (SqlException exception) {

                ExceptionManager.TrataException(exception);
            }

            return retorno;
        }

        public static string Insere(Raca raca)
        {
            try
            {
            string query = "INSERT INTO Raca (Nome, Ide) OUTPUT inserted.Codigo VALUES (@Codigo, @Ide);";
            using var conn = AcessoDB.DBAccess();
            using var command = new SqlCommand(query, conn);
            string[] fields = { "@Codigo", "@Ide" };
            object[] values = { raca.Nome, Guid.NewGuid() };
            AcessoDB.FillParameters(command, fields, values);
            raca.Codigo = (int)command.ExecuteScalar();
            AcessoDB.CloseConnection();
            return "Raça inserida com sucesso!";
            }catch (SqlException exception)
            {
                return ExceptionManager.TrataException(exception);
            }
        }

        public static string Altera(Raca raca)
        {
            try
            {
                string query = "UPDATE Raca SET Nome = @Nome WHERE Ide = @Ide;";
                using var conn = AcessoDB.DBAccess();
                using var command = new SqlCommand(query, conn);
                string[] fields = { "@Nome", "@Ide" };
                object[] values = { raca.Nome, raca.Ide };
                AcessoDB.FillParameters(command, fields, values);
                AcessoDB.ExecuteCommand(command);
                AcessoDB.CloseConnection();
                return "Raça alterada com sucesso!";
            }
            catch (SqlException exception)
            {
                return ExceptionManager.TrataException(exception);
            }
        }
    }
}
