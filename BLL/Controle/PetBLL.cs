using BLL.Interface;
using DAL;
using DTO;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace BLL.Controle
{
    public class PetBLL
    {
        public static IEnumerable<Pet> Buscar(string nome, bool? isAdotado)
        {
            return PetDAL.Buscar(nome, isAdotado);
        }

        public static string Inserir(Pet pet)
        {
            if (pet.Ide == Guid.Empty)
                return PetDAL.Insere(pet);
            else
                return PetDAL.Atualiza(pet);
        }

        public static IEnumerable<Pet> BuscarPetsAdotados(Pessoa pessoa)
        {
            return PetDAL.BuscarPetsAdotados(pessoa);
        }
    }
}
