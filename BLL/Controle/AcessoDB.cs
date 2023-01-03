using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Controle
{
    internal class AcessoDB
    {
        internal static SqlConnection AccessDB()
        {
            return new SqlConnection("Server=localhost\\SQLEXPRESS;Database=PetShop;User Id=sa;Password=Senha1;");
        }
    }
}
