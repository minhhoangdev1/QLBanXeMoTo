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
    public partial class FormLogin : Form
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        public FormLogin()
        {
            InitializeComponent();
            con.ConnectionString = @"Data Source=NGIO2ZLP2GRNQT1\MSQLSEVER12;Initial Catalog=QLBanXeMoTo;Integrated Security=True";
        }
        //Drag Form
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void iconButtonExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void iconButtonMin_Click(object sender, EventArgs e)
        {
            if(WindowState==FormWindowState.Normal)
            {
                WindowState = FormWindowState.Minimized;
            }
        }
        private void FormLogin_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    btnLogin.PerformClick();
                    break;
            }
        }

        private void btnLogin_Click_1(object sender, EventArgs e)
        {
            con.Open();
            com.Connection = con;
            com.CommandText = "select * from tblLogin";
            SqlDataReader dr = com.ExecuteReader();
            if (dr.Read())
            {
                if (txtuser.Text.Equals(dr["username"].ToString()) && txtpass.Text.Equals(dr["password"].ToString()))
                {
                    this.Hide();
                    FormLoad frmld = new FormLoad();
                    frmld.ShowDialog();
                    Form1 frm = new Form1();
                    frm.Show();
                }
                else
                {
                    MessageBox.Show("Username & Password không đúng!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            con.Close();
        }

        private void txtuser_Enter(object sender, EventArgs e)
        {
            if (txtuser.Text.Trim() == "username")
            {
                txtuser.Text = "";
                txtuser.ForeColor = Color.LightGray;
            }
        }

        private void txtuser_Leave(object sender, EventArgs e)
        {
            if (txtuser.Text.Trim() == "")
            {
                txtuser.Text = "username";
                txtuser.ForeColor = Color.Gray;
            }
        }

        private void txtpass_Enter(object sender, EventArgs e)
        {
            if (txtpass.Text == "password")
            {
                txtpass.Text = "";
                txtpass.ForeColor = Color.LightGray;
                txtpass.UseSystemPasswordChar = true;
            }
        }

        private void txtpass_Leave(object sender, EventArgs e)
        {
            if (txtpass.Text == "")
            {
                txtpass.Text = "password";
                txtpass.ForeColor = Color.Gray;
                txtpass.UseSystemPasswordChar = false;
            }
        }

        
    }
}
