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
    public partial class FormThuongHieu : Form
    {
        SqlConnection con;
        SqlCommand com;
        SqlDataReader read;
        string str = @"Data Source=NGIO2ZLP2GRNQT1\MSQLSEVER12;Initial Catalog=QLBanXeMoTo;Integrated Security=True";
        string sql;
        public FormThuongHieu()
        {
            InitializeComponent();
        }
        private void showdsTH()
        {
            ThuongHieuBLL thbll = new ThuongHieuBLL();
            List<ThuongHieu> ds = thbll.showdsTH();
            listView1.Items.Clear();
            foreach (ThuongHieu item in ds)
            {

                ListViewItem lvi = new ListViewItem(item.MaLoai);
                lvi.SubItems.Add(item.TenLoai);
                listView1.Items.Add(lvi);
            }

        }

        private void FormThuongHieu_Load(object sender, EventArgs e)
        {
            showdsTH();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                txtMaTH.Text = listView1.SelectedItems[0].SubItems[0].Text;
                txtTenTH.Text = listView1.SelectedItems[0].SubItems[1].Text;
            }
        }

        private void iconButtonThem_Click(object sender, EventArgs e)
        {
            if (txtMaTH.Text.Trim().Length == 0)
            {
                this.errorProvider1.SetError(txtMaTH, "Chưa nhập mã thương hiệu");
                txtMaTH.Focus();
            }
            else if (txtTenTH.Text.Trim().Length == 0)
            {
                this.errorProvider1.SetError(txtTenTH, "Chưa nhập tên tthương hiệu");
                txtTenTH.Focus();
            }
            
            else if (txtMaTH.Text.Trim().Length != 0 && txtTenTH.Text.Trim().Length != 0)
            {
                this.errorProvider1.Clear();
                ThuongHieu th = new ThuongHieu();
                th.MaLoai = txtMaTH.Text.Trim();
                th.TenLoai = txtTenTH.Text.Trim();
                ThuongHieuBLL thbll = new ThuongHieuBLL();
                List<ThuongHieu> ds = thbll.showdsTH();
                foreach (ThuongHieu item in ds)
                {

                    if (th.MaLoai==item.MaLoai)
                    {
                        DialogResult r;
                        r = MessageBox.Show("Mã thương hiệu đã có !", "",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                    else
                    {
                        try
                        {
                            bool kt = thbll.ThemTH(th);
                            if (kt)
                            {
                                showdsTH();
                                MessageBox.Show("Thêm thương hiệu thành công!");
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
            r = MessageBox.Show("Bạn muốn sữa thương hiệu này?", "",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (r == DialogResult.Yes)
            {
                ThuongHieu th = new ThuongHieu();
                th.MaLoai = txtMaTH.Text.Trim();
                th.TenLoai = txtTenTH.Text.Trim();
                ThuongHieuBLL thbll = new ThuongHieuBLL();
                bool kt = thbll.SuaTH(th);
                if (kt)
                {
                    showdsTH();
                    MessageBox.Show("Sửa thương hiệu thành công!");
                }
            }
        }

        private void iconButtonXoa_Click(object sender, EventArgs e)
        {
            DialogResult r;
            r = MessageBox.Show("Bạn muốn xóa thương hiệu này?", "",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (r == DialogResult.Yes)
            {
                ThuongHieu th = new ThuongHieu();
                th.MaLoai = txtMaTH.Text.Trim();
                ThuongHieuBLL thbll = new ThuongHieuBLL();
                bool kt = thbll.XoaTH(th);
                if (kt)
                {
                    showdsTH();
                    MessageBox.Show("Xóa thượng hiệu thành công!");
                }
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
        
        private void searchTen()
        {
            con = new SqlConnection(str);
            sql = @"select * from tblLoaiSP where TenLoai = N'" + txtSearch.Text + "'";
            con.Open();
            com = new SqlCommand(sql, con);
            read = com.ExecuteReader();
            while (read.Read())
            {
                ListViewItem lvi = new ListViewItem(read[0].ToString());
                lvi.SubItems.Add(read[1].ToString());
                listView1.Items.Add(lvi);
            }
            con.Close();
        }
        private void searchMa()
        {
            con = new SqlConnection(str);
            sql = @"select * from tblLoaiSP where MaLoai = N'" + txtSearch.Text + "'";
            con.Open();
            com = new SqlCommand(sql, con);
            read = com.ExecuteReader();
            while (read.Read())
            {
                ListViewItem lvi = new ListViewItem(read[0].ToString());
                lvi.SubItems.Add(read[1].ToString());
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

        private void iconButtonReset_Click(object sender, EventArgs e)
        {
            showdsTH();
        }
    }
}
