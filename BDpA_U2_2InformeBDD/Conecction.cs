using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            catch (Exception ex)
            {
                var Mensaje = ex.Message;
                Mensaje = @"Ocurrio un error en la conexion a la base de datos numero uno,
                            consulte al administrador.";
                MessageBox.Show(Mensaje);
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
            catch (Exception ex)
            {
                var Mensaje = ex.Message;
                Mensaje = @"Ocurrio un error en la conexion a la base de datos numero dos,
                            consulte al administrador.";
                MessageBox.Show(Mensaje);
                throw;
            }

            return mySqlConnection;
        }

        //Function show info
        public DataGridView MostrarDatos( DataGridView dataGridView)
        {                  
            try
            {
                //Query
                MySqlCommand mySqlCommand = new MySqlCommand();
                mySqlCommand.Connection = Conecction.ObtenerConexion();
                mySqlCommand.CommandText = (@"SELECT Cli, Cliente, LimCredito AS Limite
                                                FROM Ferretera.clientes           
                                                GROUP BY cli");
                mySqlCommand.CommandType = CommandType.Text;
                MySqlDataAdapter MySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
                MySqlDataReader Lector = mySqlCommand.ExecuteReader();

                while (Lector.Read())
                {
                    MySqlCommand mySqlCommand2 = new MySqlCommand();
                    mySqlCommand2.Connection = Conecction.ObtenerConexion2();
                    mySqlCommand2.CommandText = (@"select t1.cli, t2.total1 as compras, t3.abonos as abonos, sum(t2.total1 - t3.abonos) as Saldo  from 
                                                    (
                                                        (select * from clientes) t1
                                                        join
                                                        (select venta, cli, sum(total) total1 from ventas group by cli) t2
                                                        on t1.cli = t2.cli
                                                        join
                                                        (select venta, sum(importe) abonos  from pagos GROUP BY venta) t3
                                                        on t2.venta = t3.venta
                                                    ) group by t2.venta");
                    mySqlCommand2.CommandType = CommandType.Text;
                    MySqlDataAdapter MySqlDataAdapter2 = new MySqlDataAdapter(mySqlCommand2);
                    MySqlDataReader Lector2 = mySqlCommand2.ExecuteReader();

                    while (Lector2.Read())
                    {

                        var x = Lector2.GetValue(3).ToString();
                        if (Lector.GetInt64(0) == Lector2.GetInt64(0))
                        {
                           dataGridView.Rows.Add(Lector.GetValue(0), Lector.GetValue(1), Lector.GetValue(2), Lector2.GetValue(1), Lector2.GetValue(2), Lector2.GetValue(3));

                        }

                    }


                }
                double total = 0;
                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    total += Convert.ToDouble(row.Cells["Saldo"].Value);
                }
                dataGridView.Rows.Add(null, null, null, null, "Cartera: ", total);
                return dataGridView;
            }
            catch (Exception ex)
            {
                var Mensaje = ex.Message;
                Mensaje = @"¡Ocurrio un error de conexion!.";
                MessageBox.Show(Mensaje);
                return dataGridView;
            }


        }
    }
}
