using DTO;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.SqlTypes;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace DAL
{
    public class PetDAL
    {
        public static string Insere(Pet pet)
        {
            try
            {
                string query = "INSERT INTO Pet (Nome, DataNascimento, Sexo, QuantidadeVacinas, IdeRaca, CodigoRaca, Ide) OUTPUT inserted.Codigo VALUES (@Nome, @DataNascimento, @Sexo, @QuantidadeVacinas, @IdeRaca, @CodigoRaca, @Ide);";
                using var conn = AcessoDB.DBAccess();
                using var command = new SqlCommand(query, conn);
                string[] names = { "@Nome", "@DataNascimento", "@Sexo", "@QuantidadeVacinas", "@IdeRaca", "@CodigoRaca", "@Ide" };
                object[] values = { pet.Nome, pet.DataNascimento, pet.Sexo, pet.QuantidadeVacinas, pet.RacaIde, pet.CodigoRaca, Guid.NewGuid() };
                AcessoDB.FillParameters(command, names, values);
                pet.Codigo = (int)command.ExecuteScalar();
                AcessoDB.CloseConnection();
                return "Pet inserido com sucesso!";
            } catch(SqlException exception)
            {
                return ExceptionManager.TrataException(exception);
            }
        }
        public static string Atualiza(Pet pet)
        {
            try
            {
                string query = "UPDATE Pet set Nome = @Nome, DataNascimento = @DataNascimento, Sexo = @Sexo, QuantidadeVacinas =@QuantidadeVacinas, IdeRaca = @IdeRaca WHERE Ide = @Ide);";
                using var conn = AcessoDB.DBAccess();
                using var command = new SqlCommand(query, conn);
                string[] names = { "@Nome", "@DataNascimento", "@Sexo", "@QuantidadeVacinas", "@IdeRaca", "@Ide" };
                object[] values = { pet.Nome, pet.DataNascimento, pet.Sexo, pet.QuantidadeVacinas, pet.RacaIde, pet.Ide };
                AcessoDB.FillParameters(command, names, values);
                pet.Codigo = (int)command.ExecuteScalar();
                AcessoDB.CloseConnection();
                return "Pet alterado com sucesso!";
            }
            catch (SqlException exception)
            {
                return ExceptionManager.TrataException(exception);
            }
        }
        public static IEnumerable<Pet> Buscar(string nome, bool? isAdotado)
        {
            List<Pet> pets = new();
            string query = 
                "WITH PetSet as " +
                "(SELECT Codigo, Nome, DataNascimento, Sexo, QuantidadeVacinas, IdeRaca, Ide, CASE WHEN IdePet IS NULL THEN 0 ELSE 1 END as IsAdotado " +
                "FROM Pet LEFT JOIN PetsDaPessoa ON Pet.Ide = PetsDaPessoa.IdePet) " +
                "select * from PetSet WHERE(1 = 1 AND Nome LIKE @Nome) " /*AND (1 = 1 AND IsAdotado IN (@IsAdotado))*/+" ORDER BY Codigo; ";
            try
            {
                using var conn = AcessoDB.DBAccess();
                using var command = new SqlCommand(query, conn);
                //var numeros = isAdotado is null ? new object[] { 0, 1 } : new object[] {Convert.ToInt32( isAdotado.Value )};
                string[] names = new string[] { "@Nome", /*"@IsAdotado"*/ };
                object[] values = new object[] { $"%{nome}%",/*numeros*/ };
                AcessoDB.FillParameters(command, names, values);
                using var reader = AcessoDB.Read(command);
                while (reader.Read())
                {
                    Pet pet = new()
                    {
                        Codigo = reader.GetInt32("Codigo"),
                        Nome = reader.GetString("Nome"),
                        DataNascimento = reader.GetDateTime("DataNascimento"),
                        Sexo = reader.GetString("Sexo"),
                        QuantidadeVacinas = reader.GetInt32("QuantidadeVacinas"),
                        Ide = reader.GetGuid("Ide"),
                        RacaIde = reader.GetGuid("IdeRaca"),
                        IsAdotado = reader.GetInt32("IsAdotado") == 1
                    };
                    pets.Add(pet);
                }
                AcessoDB.CloseConnection();
            } catch(SqlException exception)
            {
                ExceptionManager.TrataException(exception);
            }
            return pets;
        }
        public static IEnumerable<Pet> BuscarPetsAdotados(Pessoa pessoa)
        {
            List<Pet> pets = new();
            string query = "SELECT Codigo, Nome, DataNascimento, Sexo, QuantidadeVacinas, IdeRaca, Ide FROM Pet JOIN PetsdaPessoa on Pet.Ide = PetsdaPessoa.IdePet AND PetsDaPessoa.IdePessoa = @IdePessoa;";
            try
            {
                using var conn = AcessoDB.DBAccess();
                using var command = new SqlCommand(query, conn);
                string[] names = new string[] { "@IdePessoa" };
                object[] values = new object[] { pessoa.Ide };
                AcessoDB.FillParameters(command, names, values);
                using var reader = AcessoDB.Read(command);
                while (reader.Read())
                {
                    Pet pet = new()
                    {
                        Codigo = reader.GetInt32("Codigo"),
                        Nome = reader.GetString("Nome"),
                        DataNascimento = reader.GetDateTime("DataNascimento"),
                        Sexo = reader.GetString("Sexo"),
                        QuantidadeVacinas = reader.GetInt32("QuantidadeVacinas"),
                        Ide = reader.GetGuid("Ide"),
                        RacaIde = reader.GetGuid("IdeRaca")
                    };
                    pets.Add(pet);
                }
                AcessoDB.CloseConnection();
            }
            catch (SqlException exception)
            {
                ExceptionManager.TrataException(exception);
            }
            return pets;
        }
    }
}
