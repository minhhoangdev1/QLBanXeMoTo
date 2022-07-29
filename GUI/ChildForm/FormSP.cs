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
    public partial class FormSP : Form
    {
        SqlConnection con;
        SqlCommand com;
        SqlDataReader read;
        string str = @"Data Source=NGIO2ZLP2GRNQT1\MSQLSEVER12;Initial Catalog=QLBanXeMoTo;Integrated Security=True";
        string sql;
        public FormSP()
        {
            InitializeComponent();
        }
        private void showdsSP()
        {
            SanPhamBLL spbll = new SanPhamBLL();
            List<SanPham> ds = spbll.showdsSP();
            listView1.Items.Clear();
            foreach (SanPham item in ds)
            {

                ListViewItem lvi = new ListViewItem(item.MaSP);
                lvi.SubItems.Add(item.TenSP);
                lvi.SubItems.Add(item.MaLoai);
                lvi.SubItems.Add(item.SoLuong.ToString());
                lvi.SubItems.Add(item.DonGiaNhap.ToString());
                lvi.SubItems.Add(item.DonGiaBan.ToString());
                lvi.SubItems.Add(item.Anh);
                lvi.SubItems.Add(item.GhiChu);
                listView1.Items.Add(lvi);
            }

        }

        private void FormSP_Load(object sender, EventArgs e)
        {
            showdsSP();
            TenTH();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                txtMaSP.Text = listView1.SelectedItems[0].SubItems[0].Text;
                txtTenSP.Text = listView1.SelectedItems[0].SubItems[1].Text;
                txtML.Text = listView1.SelectedItems[0].SubItems[2].Text;
                txtSL.Text = listView1.SelectedItems[0].SubItems[3].Text;
                txtDGN.Text = listView1.SelectedItems[0].SubItems[4].Text;
                txtDGB.Text = listView1.SelectedItems[0].SubItems[5].Text;
                txtA.Text = listView1.SelectedItems[0].SubItems[6].Text;
                txtGC.Text = listView1.SelectedItems[0].SubItems[7].Text;
                con = new SqlConnection(str);
                sql = @"select Anh  from tblSP where MaSP = N'" + txtMaSP.Text + "'";
                con.Open();
                com = new SqlCommand(sql, con);
                string img = com.ExecuteScalar().ToString();
                try
                {
                    pictureBox1.Image = Image.FromFile(img);
                }
                catch (Exception) { }            
                con.Close();
            }
        }

        private void iconButtonThem_Click(object sender, EventArgs e)
        {
            if (txtMaSP.Text.Trim().Length == 0)
            {
                this.errorProvider1.SetError(txtMaSP, "Chưa nhập mã sản phẩm");
                txtMaSP.Focus();
            }
            else if (txtTenSP.Text.Trim().Length == 0)
            {
                this.errorProvider1.SetError(txtTenSP, "Chưa nhập tên sản phẩm");
                txtTenSP.Focus();
            }
            else if (txtML.Text.Trim().Length == 0)
            {
                this.errorProvider1.SetError(txtML, "Chưa nhập mã loại sản  phẩm");
                txtML.Focus();
            }
            else if (txtSL.Text.Length == 0)
            {
                this.errorProvider1.SetError(txtSL, "Chưa nhập số lượng");
                txtSL.Focus();
                if (txtSL.Text.Trim().Length > 0 && !char.IsDigit(txtSL.Text, txtSL.Text.Length - 1))
                {
                    this.errorProvider1.SetError(txtSL, "số lượng phải là số");
                    txtSL.Focus();
                }
            }
            else if (txtDGN.Text.Length == 0)
            {
                this.errorProvider1.SetError(txtDGN, "Chưa nhập đơn giá nhập");
                txtDGN.Focus();
                if (txtDGN.Text.Trim().Length > 0 && !char.IsDigit(txtDGN.Text, txtDGN.Text.Length - 1))
                {
                    this.errorProvider1.SetError(txtDGN, "đơn giá nhập phải là số");
                    txtDGN.Focus();
                }
            }
            else if (txtDGB.Text.Length == 0)
            {
                this.errorProvider1.SetError(txtDGB, "Chưa nhập đơn giá bán");
                txtDGB.Focus();
                if (txtDGB.Text.Trim().Length > 0 && !char.IsDigit(txtDGB.Text, txtDGB.Text.Length - 1))
                {
                    this.errorProvider1.SetError(txtDGB, "đơn giá bán phải là số");
                    txtDGB.Focus();
                }
            }
            else if (txtMaSP.Text.Trim().Length != 0 
                    && txtTenSP.Text.Trim().Length != 0
                    && txtSL.Text.Trim().Length != 0
                    && txtML.Text.Trim().Length != 0
                    &&txtDGN.Text.Trim().Length!=0
                    &&txtDGB.Text.Trim().Length!=0)
            {
                this.errorProvider1.Clear();
                SanPham sp = new SanPham();
                sp.MaSP = txtMaSP.Text.Trim();
                sp.TenSP = txtTenSP.Text.Trim();
                sp.MaLoai = txtML.Text.Trim();
                sp.SoLuong = double.Parse(txtSL.Text.Trim());
                sp.DonGiaNhap = double.Parse(txtDGN.Text.Trim());
                sp.DonGiaBan = double.Parse(txtDGB.Text.Trim());
                sp.Anh = txtA.Text.Trim();
                sp.GhiChu = txtGC.Text.Trim();
                SanPhamBLL spbll = new SanPhamBLL();
                List<SanPham> ds = spbll.showdsSP();
                foreach (SanPham item in ds)
                {

                    if (sp.MaSP==item.MaSP)
                    {
                        DialogResult r;
                        r = MessageBox.Show("Mã sản phẩm đã có!", "",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                    else
                    {
                        try
                        {
                            bool kt = spbll.ThemSP(sp);
                            if (kt)
                            {
                                showdsSP();
                                MessageBox.Show("Thêm sản phẩm thành công!");
                            }
                        }
                        catch (Exception) { }
                    }
                }


            }
        }

        private void iconButtonSua_Click(object sender, EventArgs e)
        {
            DialogResult r;
            r = MessageBox.Show("Bạn muốn sản phẩm viên này?", "",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (r == DialogResult.Yes)
            {
                SanPham sp = new SanPham();
                sp.MaSP = txtMaSP.Text.Trim();
                sp.TenSP = txtTenSP.Text.Trim();
                sp.MaLoai = txtML.Text.Trim();
                sp.SoLuong = double.Parse(txtSL.Text.Trim());
                sp.DonGiaNhap = double.Parse(txtDGN.Text.Trim());
                sp.DonGiaBan = double.Parse(txtDGB.Text.Trim());
                sp.Anh = txtA.Text.Trim();
                sp.GhiChu = txtGC.Text.Trim();
                SanPhamBLL spbll = new SanPhamBLL();
                bool kt = spbll.SuaSP(sp);
                if (kt)
                {
                    showdsSP();
                    MessageBox.Show("Sửa sản phẩm thành công!");
                }
            }

        }

        private void iconButtonXoa_Click(object sender, EventArgs e)
        {
            DialogResult r;
            r = MessageBox.Show("Bạn muốn xóa sản phẩm này?", "",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (r == DialogResult.Yes)
            {
                SanPham sp = new SanPham();
                sp.MaSP = txtMaSP.Text.Trim();
                SanPhamBLL spbll = new SanPhamBLL();
                bool kt = spbll.XoaSP(sp);
                if (kt)
                {
                    showdsSP();
                    MessageBox.Show("Xóa sản phẩm thành công!");
                }
            }
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Bitmap file|*.bmp|JPEG|*.jpg|PNG|*.png|GIF|*.gif|All files(*.*)|*.*";
            dlg.FilterIndex = 2;
            dlg.Title = "Chọn ảnh cho sản phẩm";
            if(dlg.ShowDialog()==DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(dlg.FileName);
                txtA.Text = dlg.FileName;
            }
        }
        private void TenTH()
        {
            ThuongHieuBLL thbll = new ThuongHieuBLL();
            List<ThuongHieu> ds = thbll.showdsTH();
            foreach (ThuongHieu item in ds)
            {
                comboBoxTen.Items.Add(item.TenLoai);
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
            sql = @"select * from tblSP where TenSP = N'" + txtSearch.Text + "'";
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
                lvi.SubItems.Add(read[5].ToString());
                lvi.SubItems.Add(read[6].ToString());
                lvi.SubItems.Add(read[7].ToString());
                listView1.Items.Add(lvi);
            }
            con.Close();
        }
        private void searchMa()
        {
            con = new SqlConnection(str);
            sql = @"select * from tblSP where MaSP = N'" + txtSearch.Text + "'";
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
                lvi.SubItems.Add(read[5].ToString());
                lvi.SubItems.Add(read[6].ToString());
                lvi.SubItems.Add(read[7].ToString());
                listView1.Items.Add(lvi);
            }
            con.Close();
        }
        private void searchtenTH()
        {
            con = new SqlConnection(str);
            sql = @"select a.MaSP,a.TenSP,a.MaLoai,a.SoLuong,a.DonGiaNhap,a.DonGiaBan,a.Anh,a.GhiChu,b.TenLoai from tblSP As a,tblLoaiSP As b where b.TenLoai = N'" + txtTen.Text + "' and a.MaLoai=b.MaLoai" ;
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
                lvi.SubItems.Add(read[5].ToString());
                lvi.SubItems.Add(read[6].ToString());
                lvi.SubItems.Add(read[7].ToString());
                listView1.Items.Add(lvi);
            }
            con.Close();
        }
        private void iconButtonSearch_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            if (radioButtonTen.Checked == true)
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
            showdsSP();
        }

        private void comboBoxTen_SelectedIndexChanged(object sender, EventArgs e)
        {
            ThuongHieuBLL thbll = new ThuongHieuBLL();
            List<ThuongHieu> ds = thbll.showdsTH();
            foreach (ThuongHieu item in ds)
            {
                if(comboBoxTen.SelectedItem.ToString()==item.TenLoai)
                {
                    txtTen.Text = item.TenLoai;
                }
            }
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            searchtenTH();
        }
    }
}
