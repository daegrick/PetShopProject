using DAL;
using DTO;

namespace BLL.Controle
{
    public class RacaBLL
    {
        public static IEnumerable<Raca> Busca()
        {
            return RacaDAL.Busca();
        }

        public static Dictionary<Guid, string> BuscaDictionary()
        {
            return RacaDAL.BuscaDictionary();
        }

        public static string Insere(Raca raca)
        {
            if (raca.Ide == Guid.Empty)
                return RacaDAL.Insere(raca);
            else
                return RacaDAL.Altera(raca);
        }
    }
}