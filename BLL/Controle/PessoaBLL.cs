using BLL.Interface;
using DAL;
using DTO;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace BLL.Controle
{
    public class PessoaBLL
    {
        public static string Incluir(Pessoa pessoa)
        {
            if (pessoa.Ide == Guid.Empty)
               return PessoaDAL.Incluir(pessoa);
            else
               return PessoaDAL.Atualizar(pessoa);
        }

        public void IncluirPets(Pessoa pessoa, IEnumerable<Pet> pets)
        {
            try
            {
                using var conn = AcessoDB.AccessDB();
                using var transaction = conn.BeginTransaction();
                try
                {
                    string query = "INSERT INTO pets_do_dono (codigo_pet, codigo_dono, ide_pet, ide_dono) values (@codigo_pet, @codigo_dono, @ide_pet, @ide_dono)";
                    using var command = new SqlCommand(query, conn);
                    foreach (var pet in pets)
                    {
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@codigo_dono", pessoa.Codigo);
                        command.Parameters.AddWithValue("@codigo_pet", pet.Codigo);
                        command.Parameters.AddWithValue("@ide_dono", pessoa.Ide);
                        command.Parameters.AddWithValue("@ide_pet", pet.Ide);
                        command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                }
            }
            catch (SqlException ex)
            {

            }
        }

        public static IEnumerable<Pessoa> ListaPessoas()
        {
            return PessoaDAL.Busca();
        }
    }
}