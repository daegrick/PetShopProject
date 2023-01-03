using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    internal interface IPet
    {
        string Inserir(Pet pet);
        IEnumerable<Pet> Buscar();

    }
}
