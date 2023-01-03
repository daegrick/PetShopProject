using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    internal interface IPessoa
    {
        void Incluir(Pessoa pessoa);
        IEnumerable<Pessoa> ListaPessoas();
        void IncluirPets(Pessoa pessoa, IEnumerable<Pet> pets);

    }
}
