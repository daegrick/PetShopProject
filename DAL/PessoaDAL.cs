using DTO;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DAL
{
    public class PessoaDAL
    {
        public static string Atualizar(Pessoa pessoa)
        {
            string query = "UPDATE pessoa set Nome = @Nome, Sexo = @Sexo, DataNascimento = @DataNascimento WHERE Ide = @Ide;";
            try
            {
                using var conn = AcessoDB.DBAccess();
                using var command = new SqlCommand(query, conn);
                string[] names = new string[] { "@Nome", "@Sexo", "@DataNascimento", "@Ide" };
                object[] values = new object[] { pessoa.Nome, pessoa.Sexo, pessoa.Nascimento, Guid.NewGuid() };
                AcessoDB.FillParameters(command, names, values);
                AcessoDB.ExecuteCommand(command);
                AcessoDB.CloseConnection();
                return "Pessoa alterada com suceso!";
            }
            catch (SqlException ex)

            {
               return ExceptionManager.TrataException(ex);
            }
        }

        public static IEnumerable<Pessoa> Busca()
        {
            List<Pessoa> pessoas = new();
            string query = "SELECT * FROM Pessoa ORDER BY Codigo";
            try
            {
                var conn = AcessoDB.DBAccess();
                var command = new SqlCommand(query, conn);
                var reader = AcessoDB.Read(command);
                while (reader.Read())
                {
                    Pessoa pessoa = new()
                    {
                        Codigo = reader.GetInt32("Codigo"),
                        Nome = reader.GetString("Nome"),
                        Nascimento = reader.GetDateTime("DataNascimento"),
                        Sexo = reader.GetString("Sexo"),
                        Ide = reader.GetGuid("Ide")
                    };
                    pessoas.Add(pessoa);
                }
            }
            catch (SqlException exception)
            {
                ExceptionManager.TrataException(exception);
            }
            finally
            {
                AcessoDB.CloseConnection();
            }
            return pessoas;
        }

        public static Pessoa Busca(Guid ide)
        {
            Pessoa pessoa = new Pessoa();
            string query = "SELECT Codigo, Nome, DataNascimento, Sexo, Ide FROM Pessoa WHERE Ide = @Ide;";
            try
            {
                using var conn = AcessoDB.DBAccess();
                using var command = new SqlCommand(query, conn);
                string[] names = new string[] { "@Ide" };
                object[] values = new object[] { ide };
                using var reader = AcessoDB.Read(command);
                if (reader.Read())
                {
                    pessoa.Codigo = reader.GetInt32("Codigo");
                    pessoa.Nome = reader.GetString("Nome");
                    pessoa.Nascimento = reader.GetDateTime("DataNascimento");
                    pessoa.Sexo = reader.GetString("Sexo");
                    pessoa.Ide = reader.GetGuid("Ide");
                }
                AcessoDB.CloseConnection();

            }
            catch (SqlException exception)
            {
                ExceptionManager.TrataException(exception);
            }
            return pessoa;
        }

        public static string Incluir(Pessoa pessoa)
        {
            string query = "INSERT INTO pessoa  (Nome, Sexo, DataNascimento, Ide) OUTPUT inserted.Codigo VALUES (@Nome, @Sexo,@DataNascimento, @Ide) ;";
            try
            {
                using var conn = AcessoDB.DBAccess();
                using var command = new SqlCommand(query, conn);
                string[] names = new string[] { "@Nome", "@Sexo", "@DataNascimento", "@Ide" };
                object[] values = new object[] { pessoa.Nome, pessoa.Sexo, pessoa.Nascimento, Guid.NewGuid() };
                AcessoDB.FillParameters(command, names, values);
                pessoa.Codigo = (int)command.ExecuteScalar();
                AcessoDB.CloseConnection();
                return "Pessoa incluida com sucesso!";
            }
            catch (SqlException ex)

            {
                return ExceptionManager.TrataException(ex);
            }
        }
    }
}