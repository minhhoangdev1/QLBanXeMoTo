using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO;
using BLL;
using System.Data.SqlClient;

namespace GUI.ChildForm
{
    public partial class FormKhachHang : Form
    {
        SqlConnection con;
        SqlCommand com;
        SqlDataReader read;
        string str = @"Data Source=NGIO2ZLP2GRNQT1\MSQLSEVER12;Initial Catalog=QLBanXeMoTo;Integrated Security=True";
        string sql;
        public FormKhachHang()
        {
            InitializeComponent();
        }
        private void showdsKH()
        {
            KhachHangBLL khbll = new KhachHangBLL();
            List<KhachHang> ds = khbll.showdsKH();
            listView1.Items.Clear();
            foreach (KhachHang item in ds)
            {

                ListViewItem lvi = new ListViewItem(item.MaKH);
                lvi.SubItems.Add(item.HoTen);
                lvi.SubItems.Add(item.SDT);
                lvi.SubItems.Add(item.DiaChi);
                listView1.Items.Add(lvi);
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
        private void FormKhachHang_Load(object sender, EventArgs e)
        {
            showdsKH();
            TongKH();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                txtMaKH.Text = listView1.SelectedItems[0].SubItems[0].Text;
                txtTenKH.Text = listView1.SelectedItems[0].SubItems[1].Text;
                txtSDT.Text = listView1.SelectedItems[0].SubItems[2].Text;    
                txtDc.Text = listView1.SelectedItems[0].SubItems[3].Text;
            }
        }

        private void iconButtonThem_Click(object sender, EventArgs e)
        {
            if (txtMaKH.Text.Trim().Length == 0)
            {
                this.errorProvider1.SetError(txtMaKH, "Chưa nhập mã khách hàng");
                txtMaKH.Focus();
            }
            else if (txtTenKH.Text.Trim().Length == 0)
            {
                this.errorProvider1.SetError(txtTenKH, "Chưa nhập tên khách hàng");
                txtTenKH.Focus();
            }
            else if (txtSDT.Text.Trim().Length == 0)
            {
                this.errorProvider1.SetError(txtSDT, "Chưa nhập số điện thoại");
                txtSDT.Focus();
            }
            else if (txtDc.Text.Trim().Length == 0)
            {
                this.errorProvider1.SetError(txtDc, "Chưa nhập địa chỉ");
                txtDc.Focus();
            }
            else if (txtMaKH.Text.Trim().Length != 0 && txtTenKH.Text.Trim().Length != 0
                    && txtSDT.Text.Trim().Length != 0
                    && txtDc.Text.Trim().Length != 0)
            {
                this.errorProvider1.Clear();
                KhachHang kh = new KhachHang();
                kh.MaKH = txtMaKH.Text.Trim();
                kh.HoTen = txtTenKH.Text.Trim();
                kh.SDT = txtSDT.Text.Trim();
                kh.DiaChi = txtDc.Text.Trim();           
                KhachHangBLL khbll = new KhachHangBLL();
                List<KhachHang> ds = khbll.showdsKH();
                foreach (KhachHang item in ds)
                {

                    if (kh.MaKH == item.MaKH)
                    {
                        DialogResult r;
                        r = MessageBox.Show("Mã Khách hàng đã có !", "",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                    else
                    {
                        try
                        {
                            bool kt = khbll.ThemKH(kh);
                            if (kt)
                            {
                                showdsKH();
                                MessageBox.Show("Thêm khách hàng thành công!");
                            }
                        }
                        catch (Exception ) { }
                    }
                }


            }
        }

        private void iconButtonSua_Click(object sender, EventArgs e)
        {
            DialogResult r;
            r = MessageBox.Show("Bạn muốn sữa nhân viên này?", "",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (r == DialogResult.Yes)
            {
                KhachHang kh = new KhachHang();
                kh.MaKH = txtMaKH.Text.Trim();
                kh.HoTen = txtTenKH.Text.Trim();
                kh.SDT = txtSDT.Text.Trim();
                kh.DiaChi = txtDc.Text.Trim();
                KhachHangBLL khbll = new KhachHangBLL();
                bool kt = khbll.SuaKH(kh);
                if (kt)
                {
                    showdsKH();
                    MessageBox.Show("Sửa khách hàng thành công!");
                }
            }
        }

        private void iconButtonXoa_Click(object sender, EventArgs e)
        {
            DialogResult r;
            r = MessageBox.Show("Bạn muốn xóa khách hàng này?", "",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (r == DialogResult.Yes)
            {
                KhachHang kh = new KhachHang();
                kh.MaKH = txtMaKH.Text.Trim();
                KhachHangBLL khbll = new KhachHangBLL();
                bool kt = khbll.XoaKH(kh);
                if (kt)
                {
                    showdsKH();
                    MessageBox.Show("Xóa khách hàng thành công!");
                }
            }
        }
       
        private void searchTen()
        {
            con = new SqlConnection(str);
            sql = @"select * from tblKhachHang where HoTen = N'" + txtSearch.Text + "'";
            con.Open();
            com = new SqlCommand(sql, con);
            read = com.ExecuteReader();
            while (read.Read())
            {
                ListViewItem lvi = new ListViewItem(read[0].ToString());
                lvi.SubItems.Add(read[1].ToString());
                lvi.SubItems.Add(read[2].ToString());
                lvi.SubItems.Add(read[3].ToString());
                listView1.Items.Add(lvi);
            }
            con.Close();
        }
        private void searchMa()
        {
            con = new SqlConnection(str);
            sql = @"select * from tblKhachHang where MaKH = N'" + txtSearch.Text + "'";
            con.Open();
            com = new SqlCommand(sql, con);
            read = com.ExecuteReader();
            while (read.Read())
            {
                ListViewItem lvi = new ListViewItem(read[0].ToString());
                lvi.SubItems.Add(read[1].ToString());
                lvi.SubItems.Add(read[2].ToString());
                lvi.SubItems.Add(read[3].ToString());
                listView1.Items.Add(lvi);
            }
            con.Close();
        }
        private void iconButtonSearch_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            if(radioButtonTen.Checked==true)
            {
                searchTen();
            }
            else
            {
                searchMa();
            }
        }

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Search")
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = Color.White;
            }
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (txtSearch.Text == "")
            {
                txtSearch.Text = "Search";
                txtSearch.ForeColor = Color.Gray;
            }
        }

        private void iconButtonReset_Click(object sender, EventArgs e)
        {
            showdsKH();
        }
    }
}
