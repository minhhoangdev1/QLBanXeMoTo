using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FontAwesome.Sharp;
using System.Runtime.InteropServices;
using DTO;
using BLL;

namespace GUI
{
    public partial class Form1 : Form
    {
        private IconButton currentBtn;
        private Panel leftBoderbtn;
        private Form currentChildForm;
        public Form1()
        {
            InitializeComponent();
            leftBoderbtn = new Panel();
            leftBoderbtn.Size = new Size(7, 42);
            panelMenu.Controls.Add(leftBoderbtn);
        }
        private void TongNV()
        {
            NhanVienBLL nvbll = new NhanVienBLL();
            List<NhanVien> ds = nvbll.TongNV();
            foreach (NhanVien item in ds)
            {
                lblNV.Text = item.TongNV.ToString();
            }
        }
        private void TongKH()
        {
            KhachHangBLL khbll = new KhachHangBLL();
            List<KhachHang> ds = khbll.TongKH();
            foreach (KhachHang item in ds)
            {
                lblKH.Text = item.TongKH.ToString();
            }
        }
        private void TongHD()
        {
            HoaDonBLL hdbll = new HoaDonBLL();
            List<HoaDon> ds = hdbll.TongHD();
            foreach (HoaDon item in ds)
            {
                lblHD.Text = item.TongHD.ToString();
            }
        }
        private void TongDT()
        {
            HoaDonBLL hdbll = new HoaDonBLL();
            List<HoaDon> ds = hdbll.TongDT();
            foreach (HoaDon item in ds)
            {
                lblDT.Text = item.TongDT.ToString();
            }
        }
        //strust
        private struct RGBColors
        {
            public static Color color1 = Color.FromArgb(172, 126, 241);
            public static Color color2 = Color.FromArgb(249, 118, 176);
            public static Color color3 = Color.FromArgb(253, 138, 114);
            public static Color color4 = Color.FromArgb(95, 77, 221);
            public static Color color5 = Color.FromArgb(249, 88, 155);
            public static Color color6 = Color.FromArgb(24, 161, 251);
        }
        private void ActivateButton(object senderBtn, Color color)
        {
            if (senderBtn != null)
            {
                DisableButton();
                //Button
                currentBtn = (IconButton)senderBtn;
                currentBtn.BackColor = Color.FromArgb(37, 36, 81);
                currentBtn.ForeColor = color;
                currentBtn.TextAlign = ContentAlignment.MiddleCenter;
                currentBtn.IconColor = color;
                currentBtn.TextImageRelation = TextImageRelation.TextBeforeImage;
                currentBtn.ImageAlign = ContentAlignment.MiddleRight;
                //left boder button
                leftBoderbtn.BackColor = color;
                leftBoderbtn.Location = new Point(0, currentBtn.Location.Y);
                leftBoderbtn.Visible = true;
                leftBoderbtn.BringToFront();
                //Current Icon picturebox
                iconPictureBoxTitle.IconChar = currentBtn.IconChar;
                iconPictureBoxTitle.IconColor = color;

                iconButtonClose.Visible = true;
                iconButtonReset.Visible = false;
            }
        }
        private void DisableButton()
        {
            if (currentBtn != null)
            {
                currentBtn.BackColor = Color.FromArgb(31, 30, 68);
                currentBtn.ForeColor = Color.Gainsboro;
                currentBtn.TextAlign = ContentAlignment.MiddleLeft;
                currentBtn.IconColor = Color.WhiteSmoke;
                currentBtn.TextImageRelation = TextImageRelation.ImageBeforeText;
                currentBtn.ImageAlign = ContentAlignment.MiddleLeft;
            }
        }

        private void OpenChildForm(Form childForm)
        {
            //open only form
            if (currentChildForm != null)
            {
                currentChildForm.Close();
            }
            currentChildForm = childForm;
            //End
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelDesktop.Controls.Add(childForm);
            panelDesktop.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            lblTitle.Text = childForm.Text;
        }

        private void Reset()
        {
            DisableButton();
            leftBoderbtn.Visible = false;
            iconPictureBoxTitle.IconChar = IconChar.Home;
            iconPictureBoxTitle.IconColor = Color.WhiteSmoke;
            lblTitle.Text = "Home";
            iconButtonClose.Visible = false;
            iconButtonReset.Visible = true;
        }
        //Drag Form
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void panelTitle_MouseDown(object sender, MouseEventArgs e)
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
            WindowState = FormWindowState.Minimized;
        }

        private void iconButtonClose_Click(object sender, EventArgs e)
        {
            if (currentChildForm != null)
            {
                currentChildForm.Close();
            }
            Reset();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            iconButtonClose.Visible = false;
            TongNV();
            TongKH();
            TongHD();
            TongDT();
        }

        private void iconButtonNhanVien_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color2);
            OpenChildForm(new ChildForm.FormNhanVien());
        }

        private void iconButtonKhachHang_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color3);
            OpenChildForm(new ChildForm.FormKhachHang());
        }

        private void iconButtonSP_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color4);
            OpenChildForm(new ChildForm.FormSP());
        }

        private void iconButtonTH_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color5);
            OpenChildForm(new ChildForm.FormThuongHieu());
        }

        private void iconButtonHD_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color1);
            OpenChildForm(new ChildForm.FormHoaDon());
        }


        private void pictureBox5_Click(object sender, EventArgs e)
        {
            if (currentChildForm != null)
            {
                currentChildForm.Close();
            }
            Reset();
        }

        private void iconButtonCtHD_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color6);
            OpenChildForm(new ChildForm.ChitietHD());
        }

        private void iconButtonReset_Click(object sender, EventArgs e)
        {
            TongNV();
            TongKH();
            TongHD();
            TongDT();
        }

        private void iconPictureBoxSignOut_Click(object sender, EventArgs e)
        {
            DialogResult r;
            r = MessageBox.Show("Bạn có chắc muốn thoát? ", "",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if(r==DialogResult.Yes)
            {
                this.Hide();
                FormLogin frmlg = new FormLogin();
                frmlg.Show();
            }
        }

        private void lblSignout_Click(object sender, EventArgs e)
        {
            DialogResult r;
            r = MessageBox.Show("Bạn có chắc muốn thoát? ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r == DialogResult.Yes)
            {
                this.Hide();
                FormLogin frmlg = new FormLogin();
                frmlg.Show();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label3.Text = DateTime.Now.ToString("HH:mm:ss");
            label1.Text = DateTime.Now.ToString("tt");
            label4.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy"); 
        }
    }
    
}
