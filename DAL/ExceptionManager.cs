using Microsoft.Data.SqlClient;

namespace DAL
{
    internal class ExceptionManager
    {
        public static string TrataException(SqlException exception)
        {
            switch (exception.ErrorCode)
            {
                default:
                    return $"{exception.ErrorCode} - {exception.Message}";
                    break;
            }
            return string.Empty;
        }
    }
}