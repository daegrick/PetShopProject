using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class AcessoDB
    {
        private const string arqIdFile = "ArqId.txt";
        private static SqlConnection? connection;
        public static SqlConnection DBAccess()
        {
            connection = new(GetStringConnection(File.ReadAllLines(arqIdFile)));
            connection.Open();
            return connection;
        }

        private static string GetStringConnection(string[] dadosAcesso, string dbName = "PetShop")
        {
            string[] host = dadosAcesso[0].Split(',');
            string optionalPort = host.Length > 1 ? $"Port={host[1]};" : string.Empty;
            return $"Server={dadosAcesso[0]};Database={dbName};{optionalPort}User Id={dadosAcesso[1]};Password={dadosAcesso[2]};Trust Server Certificate=true;";
        }

        internal static void ExecuteCommand(SqlCommand command)
        {
            command.ExecuteNonQuery();
        }

        internal static void FillParameters(SqlCommand command, string[] names, object[] values)
        {
            if (names.Length <= 0 || values.Length <= 0 || names.Length != values.Length)
                throw new Exception();
            for (int i = 0; i < names.Length; i++)
            {
                command.Parameters.AddWithValue(names[i], values[i]);
            }
        }

        internal static SqlDataReader Read(SqlCommand command)
        {
            return command.ExecuteReader();
        }

        internal static void CloseConnection()
        {
            if (connection != null && connection.State != ConnectionState.Closed)
                connection.Close();
        }

        public static bool TestaConexao(string conexao, out string mensagem)
        {
            var dadosAcesso = conexao.Split("\n");
            using SqlConnection conn = new(GetStringConnection(dadosAcesso));
            try
            {
                conn.Open();
                conn.Close();
                mensagem = "Conectado com Sucesso!";
                return true;
            }
            catch (SqlException ex)
            {
                mensagem = ExceptionManager.TrataException(ex);
                return false;
            }
        }
    }
}