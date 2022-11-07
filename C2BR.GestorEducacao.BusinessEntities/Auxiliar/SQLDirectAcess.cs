using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace C2BR.GestorEducacao.BusinessEntities.Auxiliar
{
    public class SQLDirectAcess
    {
        public DataTable retornacolunas(string SQL)
        {
            SqlConnection conexao = null;
            DataSet ds = new DataSet();
            string strcon = ConfigurationManager.AppSettings["BackupConnectionString"].ToString();

            try
            {
                conexao = new SqlConnection(strcon);
                conexao.Open();
                SqlDataAdapter da = new SqlDataAdapter(SQL, conexao);
                da.SelectCommand.CommandTimeout = TimeSpan.FromMinutes(180).Seconds;
                da.Fill(ds);
            }
            catch
            {
                return null;
            }
            finally
            {
                if (null != conexao)
                    if (conexao.State == ConnectionState.Open)
                        conexao.Close();
                conexao.Dispose();
                ds.Dispose();
            }
            return ds.Tables[0];
        }
        public bool InsereAltera(string SQL)
        {
            bool retorno = false;
            SqlConnection conexao = null;
            DataSet ds = new DataSet();
            string strcon = ConfigurationManager.AppSettings["BackupConnectionString"].ToString();

            try
            {
                conexao = new SqlConnection(strcon);
                conexao.Open();
                SqlDataAdapter da = new SqlDataAdapter(SQL, conexao);
                da.SelectCommand.CommandTimeout = TimeSpan.FromMinutes(180).Seconds;
                da.Fill(ds);
                retorno = true;
            }
            catch
            {
                retorno = false;
            }
            finally
            {
                if (null != conexao)
                    if (conexao.State == ConnectionState.Open)
                        conexao.Close();
                conexao.Dispose();
                ds.Dispose();
            }
            return retorno;
        }
    }
}
