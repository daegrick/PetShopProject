using DAL;
using DTO;

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

        public static IEnumerable<Pessoa> ListaPessoas(string nome)
        {
            return PessoaDAL.Busca(nome);
        }
    }
}