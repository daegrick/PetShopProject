using DAL.Factory;
using DTO;
using Microsoft.Data.SqlClient;

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
                object[] values = new object[] { pessoa.Nome, pessoa.Sexo, pessoa.DataNascimento, pessoa.Ide };
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

        public static IEnumerable<Pessoa> Busca(string nome)
        {
            List<Pessoa> pessoas = new();
            string query = "SELECT * FROM Pessoa WHERE (1 = 1 AND Nome LIKE @Nome) ORDER BY Codigo";
            try
            {
                var conn = AcessoDB.DBAccess();
                var command = new SqlCommand(query, conn);
                string[] names = new string[] { "@Nome" };
                object[] values = new object[] { $"%{nome}%" };
                AcessoDB.FillParameters(command,names, values);
                var reader = AcessoDB.Read(command);
                while (reader.Read())
                {
                    pessoas.Add(PessoaFactory.Get(reader));
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
                    pessoa = PessoaFactory.Get(reader);
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
                object[] values = new object[] { pessoa.Nome, pessoa.Sexo, pessoa.DataNascimento, Guid.NewGuid() };
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