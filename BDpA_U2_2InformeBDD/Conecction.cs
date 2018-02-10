using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDpA_U2_2InformeBDD
{
    class Conecction
    {
         
        public static MySqlConnection ObtenerConexion()
        {            
            MySqlConnection mySqlConnection = new MySqlConnection("server=127.0.0.1; database=ferretera; Uid=root; pwd=;");            
            try
            {
                mySqlConnection.Open();
            }
            catch (Exception)
            {

                throw;
            }

            return mySqlConnection;           
        }

        public static MySqlConnection ObtenerConexion2()
        {
            MySqlConnection mySqlConnection = new MySqlConnection("server=127.0.0.1; database=cobranza; Uid=root; pwd=;");
            try
            {
                mySqlConnection.Open();
            }
            catch (Exception)
            {

                throw;
            }

            return mySqlConnection;
        }

       
    }
}
