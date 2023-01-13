using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Controle
{
    public class ArqIDBLL
    {
        public static bool TestaConexao(string conexao, out string mensagem)
        {
            return AcessoDB.TestaConexao(conexao, out mensagem);
        }
    }
}
