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
        Conecction Conexion = new Conecction();

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
            this.spmostrar_ReporteTableAdapter.DataSource = null;
            Conexion.MostrarDatos(this.spmostrar_ReporteTableAdapter);
        }
               

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            this.spmostrar_ReporteTableAdapter.Rows.Clear();
            Conexion.MostrarDatos(this.spmostrar_ReporteTableAdapter);
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
            this.spmostrar_ReporteTableAdapter.Rows.Clear();
            var dtInforme = ToDataTable(Conexion.MostrarDatos(this.spmostrar_ReporteTableAdapter));
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
            this.spmostrar_ReporteTableAdapter.Rows.Clear();
            var dtInforme = ToDataTable(Conexion.MostrarDatos(this.spmostrar_ReporteTableAdapter));
            string HtmlBody = exportHTML.ExportDatatableToHtml(dtInforme);
            System.IO.File.WriteAllText(@"C:\Users\ibrah\Documents\Materias\Octavo Cuatrimestre\Base de Datos\Informe.HTML", HtmlBody);
            Process.Start(@"C:\Users\ibrah\Documents\Materias\Octavo Cuatrimestre\Base de Datos\Informe.HTML");

        }
        
    }
}
