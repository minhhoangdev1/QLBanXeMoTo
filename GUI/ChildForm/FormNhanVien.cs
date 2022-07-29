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
using System.Globalization;
using System.Data.SqlClient;

namespace GUI.ChildForm
{
    public partial class FormNhanVien : Form
    {
        SqlConnection con;
        SqlCommand com;
        SqlDataReader read;
        string str = @"Data Source=NGIO2ZLP2GRNQT1\MSQLSEVER12;Initial Catalog=QLBanXeMoTo;Integrated Security=True";
        string sql;
        public FormNhanVien()
        {
            InitializeComponent();
        }
        private void showdsNV()
        {
            NhanVienBLL nvbll = new NhanVienBLL();
            List<NhanVien> ds = nvbll.showdsNV();
            listView1.Items.Clear();
            foreach (NhanVien item in ds)
            {

                ListViewItem lvi = new ListViewItem(item.MaNV);
                lvi.SubItems.Add(item.TenNV);
                lvi.SubItems.Add(item.GioiTinh);
                lvi.SubItems.Add(item.DiaChi);
                lvi.SubItems.Add(item.SDT);
                lvi.SubItems.Add(item.NgaySinh.ToString());
                listView1.Items.Add(lvi);
            }

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
        private void FormNhanVien_Load(object sender, EventArgs e)
        {
            showdsNV();
            TongNV();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                txtMaNV.Text = listView1.SelectedItems[0].SubItems[0].Text;
                txtTenNV.Text = listView1.SelectedItems[0].SubItems[1].Text;
                lblGT.Text = listView1.SelectedItems[0].SubItems[2].Text;
                if (lblGT.Text == "Nam")
                {
                    radioButtonNam.Checked = true;
                }
                else
                    radioButtonNu.Checked = true;
                txtDc.Text = listView1.SelectedItems[0].SubItems[3].Text;
                txtSDT.Text = listView1.SelectedItems[0].SubItems[4].Text;               
                dateTimePicker1.Value = DateTime.Parse(listView1.SelectedItems[0].SubItems[5].Text);
            }
        }

        private void iconButtonThem_Click(object sender, EventArgs e)
        {
            if (txtMaNV.Text.Trim().Length==0)
            {
                this.errorProvider1.SetError(txtMaNV,"Chưa nhập mã nhân viên");
                txtMaNV.Focus();
            }
            else if(txtTenNV.Text.Trim().Length == 0)
            {
                this.errorProvider1.SetError(txtTenNV, "Chưa nhập tên nhân viên");
                txtTenNV.Focus();
            }
            else if(txtSDT.Text.Trim().Length == 0)
            {
                this.errorProvider1.SetError(txtSDT, "Chưa nhập số điện thoại");
                txtSDT.Focus();
            }
            else if(lblGT.Text.Length == 0)
            {
                this.errorProvider1.SetError(lblGT, "Chưa chọn giới tính");
            }         
            else if(txtMaNV.Text.Trim().Length != 0 && txtTenNV.Text.Trim().Length!=0 
                    && txtSDT.Text.Trim().Length!=0
                    && lblGT.Text.Length!=0)
            {
                this.errorProvider1.Clear();
                NhanVien nv = new NhanVien();
                nv.MaNV = txtMaNV.Text.Trim();
                nv.TenNV = txtTenNV.Text.Trim();
                if (radioButtonNam.Checked == true)
                {
                    lblGT.Text = "Nam";
                }
                else
                    lblGT.Text = "Nữ";
                nv.GioiTinh = lblGT.Text;
                nv.DiaChi = txtDc.Text.Trim();
                nv.SDT = txtSDT.Text.Trim();
                nv.NgaySinh = dateTimePicker1.Value;    
                NhanVienBLL nvbll = new NhanVienBLL();
                List<NhanVien> ds = nvbll.showdsNV();
                foreach (NhanVien item in ds)
                {

                    if (nv.MaNV == item.MaNV)
                    {
                        DialogResult r;
                        r = MessageBox.Show("Mã nhân viên đã có!", "",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);                
                    }
                    else
                    {
                        try
                        {
                            bool kt = nvbll.ThemNV(nv);
                            if (kt)
                            {
                                showdsNV();
                                MessageBox.Show("Thêm nhân viên thành công!");
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
                NhanVien nv = new NhanVien();
                nv.MaNV = txtMaNV.Text.Trim();
                nv.TenNV = txtTenNV.Text.Trim();
                if (radioButtonNam.Checked == true)
                {
                    lblGT.Text = "Nam";
                }
                else
                    lblGT.Text = "Nữ";
                nv.GioiTinh = lblGT.Text;
                nv.DiaChi = txtDc.Text.Trim();
                nv.SDT = txtSDT.Text.Trim();
                nv.NgaySinh = dateTimePicker1.Value;
                NhanVienBLL nvbll = new NhanVienBLL();
                bool kt = nvbll.SuaNV(nv);
                if (kt)
                {
                    showdsNV();
                    MessageBox.Show("Sửa nhân viên thành công!");
                }
            } 
            
        }

        private void iconButtonXoa_Click(object sender, EventArgs e)
        {
            DialogResult r;
            r = MessageBox.Show("Bạn muốn xóa nhân viên này?", "",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if(r==DialogResult.Yes)
            {
                NhanVien nv = new NhanVien();
                nv.MaNV = txtMaNV.Text.Trim();
                NhanVienBLL nvbll = new NhanVienBLL();
                bool kt = nvbll.XoaNV(nv);
                if (kt)
                {
                    showdsNV();
                    MessageBox.Show("Xóa nhân viên thành công!");  
                }
            } 
        }

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            if(txtSearch.Text=="Search")
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
            sql = @"select * from tblNhanVien where TenNV = N'" + txtSearch.Text + "'";
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
                listView1.Items.Add(lvi);
            }
            con.Close();
        }
        private void searchMa()
        {
            con = new SqlConnection(str);
            sql = @"select * from tblNhanVien where MaNV = N'" + txtSearch.Text + "'";
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
            showdsNV();
        }
    }
}
