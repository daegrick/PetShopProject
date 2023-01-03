using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Controle
{
    public class AdocaoBLL
    {
        public static string Adotar(Pessoa pessoa, IEnumerable<Pet> pets)
        {
            return AdocaoDAL.Adotar(pessoa, pets);
        }
    }
}