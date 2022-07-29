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
    public partial class FormHoaDon : Form
    {
        SqlConnection con;
        SqlCommand com;
        SqlDataReader read;
        string str = @"Data Source=NGIO2ZLP2GRNQT1\MSQLSEVER12;Initial Catalog=QLBanXeMoTo;Integrated Security=True";
        string sql;
        public FormHoaDon()
        {
            InitializeComponent();
        }
        private void showdsHD()
        {
            HoaDonBLL hdbll = new HoaDonBLL();
            List<HoaDon> ds = hdbll.showdsHD();
            listView1.Items.Clear();
            foreach (HoaDon item in ds)
            {

                ListViewItem lvi = new ListViewItem(item.MaHD);
                lvi.SubItems.Add(item.MaNV);
                lvi.SubItems.Add(item.NgayBan.ToString());
                lvi.SubItems.Add(item.MaKH);
                lvi.SubItems.Add(item.TongTien.ToString());
                listView1.Items.Add(lvi);
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
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                txtMaHD.Text = listView1.SelectedItems[0].SubItems[0].Text;
                txtMaNV.Text = listView1.SelectedItems[0].SubItems[1].Text;
                dateTimePicker1.Value = DateTime.Parse(listView1.SelectedItems[0].SubItems[2].Text);
                txtMaKH.Text = listView1.SelectedItems[0].SubItems[3].Text;
                txtTT.Text = listView1.SelectedItems[0].SubItems[4].Text;
            }           
        }

        /*private void iconButtonThem_Click(object sender, EventArgs e)
        {
            if (txtMaNV.Text.Trim().Length == 0)
            {
                this.errorProvider1.SetError(txtMaNV, "Chưa nhập mã nhân viên");
                txtMaNV.Focus();
            }
            else if (txtMaHD.Text.Trim().Length == 0)
            {
                this.errorProvider1.SetError(txtMaHD, "Chưa nhập mã hóa đơn");
                txtMaHD.Focus();
            }
            else if (txtMaKH.Text.Trim().Length == 0)
            {
                this.errorProvider1.SetError(txtMaKH, "Chưa nhập mã khách hàng");
                txtMaKH.Focus();
            }
            else if (txtTT.Text.Trim().Length == 0)
            {
                this.errorProvider1.SetError(panel1, "Chưa nhập gía tiền");
                txtTT.Focus();
            }
            else if (txtMaNV.Text.Trim().Length != 0 && txtMaHD.Text.Trim().Length != 0
                    && txtMaKH.Text.Trim().Length != 0
                    && txtTT.Text.Length != 0)
            {
                this.errorProvider1.Clear();
                HoaDon hd = new HoaDon();
                hd.MaHD = txtMaHD.Text.Trim();
                hd.MaNV = txtMaNV.Text.Trim();
                hd.NgayBan = dateTimePicker1.Value;
                hd.MaKH = txtMaKH.Text.Trim();
                hd.TongTien = double.Parse(txtTT.Text.Trim());
                HoaDonBLL hdbll = new HoaDonBLL();
                List<HoaDon> ds = hdbll.showdsHD();
                foreach (HoaDon item in ds)
                {

                    if (hd.MaHD==item.MaHD)
                    {
                        DialogResult r;
                        r = MessageBox.Show("Mã hóa đơn đã có!", "",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                    else
                    {
                        try
                        {
                            bool kt = hdbll.ThemHD(hd);
                            if (kt)
                            {
                                showdsHD();
                                MessageBox.Show("Thêm hóa đơn thành công!");
                            }
                        }
                        catch (Exception) { }
                    }
                }


            }
        }*/

        /*private void iconButtonSua_Click(object sender, EventArgs e)
        {
            DialogResult r;
            r = MessageBox.Show("Bạn muốn sữa hóa đơn này?", "",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (r == DialogResult.Yes)
            {
                HoaDon hd = new HoaDon();
                hd.MaHD = txtMaHD.Text.Trim();
                hd.MaNV = txtMaNV.Text.Trim();
                hd.NgayBan = dateTimePicker1.Value;
                hd.MaKH = txtMaKH.Text.Trim();
                hd.TongTien = double.Parse(txtTT.Text.Trim());
                HoaDonBLL hdbll = new HoaDonBLL();
                bool kt = hdbll.SuaHD(hd);
                if (kt)
                {
                    showdsHD();
                    MessageBox.Show("Sửa hóa đơn thành công!");
                }
            }
        }*/

        private void iconButtonXoa_Click(object sender, EventArgs e)
        {
            DialogResult r;
            r = MessageBox.Show("Bạn muốn xóa hóa đơn này?", "",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (r == DialogResult.Yes)
            {
                HoaDon hd = new HoaDon();
                hd.MaHD = txtMaHD.Text.Trim();
                HoaDonBLL hdbll = new HoaDonBLL();
                bool kt = hdbll.XoaHD(hd);
                if (kt)
                {
                    showdsHD();
                    MessageBox.Show("Xóa hóa đơn thành công!");
                }
            }
        }

        private void FormHoaDon_Load(object sender, EventArgs e)
        {
            showdsHD();
            TongDT();
            TongHD();
        }

        private void iconButtonInTK_Click(object sender, EventArgs e)
        {
            FormThongKe frmtk = new FormThongKe();
            frmtk.Show();
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
        
        private void searchMaHD()
        {
            con = new SqlConnection(str);
            sql = @"select * from tblHoaDon where MaHD = N'" + txtSearch.Text + "'";
            con.Open();
            com = new SqlCommand(sql, con);
            read = com.ExecuteReader();
            while (read.Read())
            {
                ListViewItem lvi = new ListViewItem(read[0].ToString());
                lvi.SubItems.Add(read[1].ToString());
                lvi.SubItems.Add(read[2].ToString());
                lvi.SubItems.Add(read[3].ToString());
                lvi.SubItems.Add(read[4].ToString());
                listView1.Items.Add(lvi);
            }
            con.Close();
        }
        private void searchMaNV()
        {
            con = new SqlConnection(str);
            sql = @"select * from tblHoaDon where MaNV = N'" + txtSearch.Text + "'";
            con.Open();
            com = new SqlCommand(sql, con);
            read = com.ExecuteReader();
            while (read.Read())
            {
                ListViewItem lvi = new ListViewItem(read[0].ToString());
                lvi.SubItems.Add(read[1].ToString());
                lvi.SubItems.Add(read[2].ToString());
                lvi.SubItems.Add(read[3].ToString());
                lvi.SubItems.Add(read[4].ToString());
                listView1.Items.Add(lvi);
            }
            con.Close();
        }
        private void iconButtonSearch_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            if (radioButtonMaHD.Checked == true)
            {
                searchMaHD();
            }
            else
            {
                searchMaNV();
            }
        }

        private void iconButtonReset_Click(object sender, EventArgs e)
        {
            showdsHD();
        }
    }
}
