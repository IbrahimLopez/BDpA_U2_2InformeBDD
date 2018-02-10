using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop;
using MySql.Data.MySqlClient;

namespace BDpA_U2_2InformeBDD
{
    public partial class Form1 : Form
    {

        public Form1()
        {               
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            this.spmostrar_ReporteTableAdapter.Columns.Add("Cli", "Cli");
            this.spmostrar_ReporteTableAdapter.Columns.Add("Cliente", "Cliente");
            this.spmostrar_ReporteTableAdapter.Columns.Add("Limite de Credito", "Limite de Credito");
            this.spmostrar_ReporteTableAdapter.Columns.Add("Compras", "Compras");
            this.spmostrar_ReporteTableAdapter.Columns.Add("Abonos", "Abonos");
            this.spmostrar_ReporteTableAdapter.Columns.Add("Saldo", "Saldo");
            this.MostrarDatos();
        }
       
        public DataGridView MostrarDatos()
        {
            DataTable DtResultado = new DataTable("cliente");
            //DataTable DtResultado = new DataTable("cliente");            
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
                    mySqlCommand2.Connection =Conecction.ObtenerConexion2();
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
                            this.spmostrar_ReporteTableAdapter.Rows.Add(Lector.GetValue(0), Lector.GetValue(1), Lector.GetValue(2), Lector2.GetValue(1), Lector2.GetValue(2), Lector2.GetValue(3));
                            
                        }
                        
                    }
                    

                }
                double total = 0;
                foreach (DataGridViewRow row in spmostrar_ReporteTableAdapter.Rows)
                {
                    total += Convert.ToDouble(row.Cells["Saldo"].Value);
                }
                this.spmostrar_ReporteTableAdapter.Rows.Add(null, null, null, null, "Cartera: ", total);
                return this.spmostrar_ReporteTableAdapter;
            }
            catch (Exception ex)
            {
                var Mensaje = ex.Message;
                Mensaje = @"Su consulta tiene un error";
                return this.spmostrar_ReporteTableAdapter;
            }            


        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            if (spmostrar_ReporteTableAdapter.RowCount > 1)
            {                          
            }
            else
            {
                this.MostrarDatos();
            }
            
        }

        //DataGridView to DataTable
        private DataTable ToDataTable(DataGridView dataGridView)
        {
            var dt = new DataTable();
            foreach (DataGridViewColumn dataGridViewColumn in dataGridView.Columns)
            {
                if (dataGridViewColumn.Visible)
                {
                    dt.Columns.Add(dataGridViewColumn.Name);
                }
            }
            var cell = new object[dataGridView.Columns.Count];
            foreach (DataGridViewRow dataGridViewRow in dataGridView.Rows)
            {
                for (int i = 0; i < dataGridViewRow.Cells.Count; i++)
                {
                    cell[i] = dataGridViewRow.Cells[i].Value;
                }
                dt.Rows.Add(cell);
            }
            return dt;
        }
        private void btnGenerarExcel_Click(object sender, EventArgs e)
        {
            ExportExcel exportExcel = new ExportExcel();
            string excelFilename = "Informe";
            var dtInforme = ToDataTable(this.spmostrar_ReporteTableAdapter);
            exportExcel.Export_Ctr_Excel(dtInforme, excelFilename);
            //Abrir el documento de excel
            string mySheet = @"C:\Users\ibrah\Documents\Materias\Octavo Cuatrimestre\Base de Datos\" + excelFilename + ".xlsx";
            var excelApp = new Excel.Application();
            excelApp.Visible = true;
            Excel.Workbooks books = excelApp.Workbooks;
            Excel.Workbook sheet = books.Open(mySheet);

        }

        private void btnGenerarHtml_Click(object sender, EventArgs e)
        {
            ExportHTML exportHTML = new ExportHTML();
            var dtInforme = ToDataTable(this.spmostrar_ReporteTableAdapter);
            string HtmlBody = exportHTML.ExportDatatableToHtml(dtInforme);
            System.IO.File.WriteAllText(@"C:\Users\ibrah\Documents\Materias\Octavo Cuatrimestre\Base de Datos\Informe.HTML", HtmlBody);
            Process.Start(@"C:\Users\ibrah\Documents\Materias\Octavo Cuatrimestre\Base de Datos\Informe.HTML");

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.spmostrar_ReporteTableAdapter.Rows.Clear();
        }
    }
}
