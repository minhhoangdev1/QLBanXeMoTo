using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class FormThongKe : Form
    {
        public FormThongKe()
        {
            InitializeComponent();
        }
        private SqlConnection con;
        string str = @"Data Source=NGIO2ZLP2GRNQT1\MSQLSEVER12;Initial Catalog=QLBanXeMoTo;Integrated Security=True";
        protected SqlConnection sqlCon = null;
        private void FormThongKe_Load(object sender, EventArgs e)
        {
            if (sqlCon == null)
            {
                sqlCon = new SqlConnection(str);
            }
            string sql = "select * from tblHoaDon";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, sqlCon);
            DataSet ds = new DataSet();
            // TODO: This line of code loads data into the 'QLBanXeMoToDataSet.tblHoaDon' table. You can move, or remove it, as needed.
            this.tblHoaDonTableAdapter.Fill(this.QLBanXeMoToDataSet.tblHoaDon);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "GUI.ReportThongKe.rdlc";
            ReportDataSource rp = new ReportDataSource();
            rp.Name = "tbhoadon";
            rp.Value = ds.Tables["tblHoaDon"];
            this.reportViewer1.LocalReport.DataSources.Add(rp);
            this.reportViewer1.RefreshReport();
        }

        private void iconButtonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //Drag Form
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
    }
}
