using Microsoft.Data.SqlClient;

namespace DAL
{
    internal class ExceptionManager
    {
        public static string TrataException(SqlException exception)
        {
            switch (exception.Number)
            {
                case 18456:
                    return "Erro ao conectar com o login especificado. Verifique o usuário e senha e tente novamente.";
                case 53:
                    return "Erro de conexão - verifique as informações e tente novamente.";
                default:
                    return $"{exception.Number} - {exception.Message}";
            }
        }
    }
}