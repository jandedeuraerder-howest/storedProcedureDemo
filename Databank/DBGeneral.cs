using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Databank
{
    public class DBGeneral
    {
        public static DataTable ExecuteSPWithDataTable(string SPNaam, SqlParameter[] parameters)
        {
            string constring = ConfigurationManager.ConnectionStrings["HBO5School"].ToString();
            SqlConnection verbinding = new SqlConnection(constring);
            SqlCommand opdracht = new SqlCommand(SPNaam, verbinding);
            opdracht.CommandType = CommandType.StoredProcedure;
            if (parameters != null)
            {
                foreach (SqlParameter p in parameters)
                {
                    opdracht.Parameters.Add(p);
                }
            }
            try
            {
                opdracht.Connection.Open();
                SqlDataReader dr = opdracht.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                verbinding.Close();
                return dt;
            }
            catch
            {
                return null;
            }
        }
        public static int ExecuteSP(string SPNaam, SqlParameter[] parameters)
        {
            string constring = ConfigurationManager.ConnectionStrings["HBO5School"].ToString();
            SqlConnection verbinding = new SqlConnection(constring);
            SqlCommand opdracht = new SqlCommand(SPNaam, verbinding);
            opdracht.CommandType = CommandType.StoredProcedure;
            if (parameters != null)
            {
                foreach (SqlParameter p in parameters)
                {
                    opdracht.Parameters.Add(p);
                }
            }

            opdracht.Parameters.Add("@retourwaarde", SqlDbType.Int).Direction = ParameterDirection.Output;
            try
            {
                opdracht.Connection.Open();
                opdracht.ExecuteNonQuery();
                int retourwaarde = (int)opdracht.Parameters["@retourwaarde"].Value;
                verbinding.Close();
                return retourwaarde;

            }
            catch (Exception fout)
            {
                return -1;
            }
        }
    }
}
