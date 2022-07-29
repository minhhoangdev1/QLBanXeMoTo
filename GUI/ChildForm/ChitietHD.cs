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
using System.IO;
using System.Reflection;
using word=Microsoft.Office.Interop.Word;

namespace GUI.ChildForm
{
    public partial class ChitietHD : Form
    {
        SqlConnection con;
        SqlCommand com;
        SqlDataReader read;
        string str = @"Data Source=NGIO2ZLP2GRNQT1\MSQLSEVER12;Initial Catalog=QLBanXeMoTo;Integrated Security=True";
        string sql;
        private void FinAndReplace(word.Application wordApp,object ToFinText,object replaceWithText)
        {
            object matchCase = true;
            object matchWholeWord = true;
            object matchWildCards = false;
            object matchSoundLike = false;
            object nmatchAllforms = false;
            object forward = true;
            object format = false;
            object matchKashida = false;
            object matchDiactitics = false;
            object matchAlefHamza = false;
            object matchCocntrol = false;
            object read_only = false;
            object visible = true;
            object replace = 2;
            object wrap = 1;

            wordApp.Selection.Find.Execute(ref ToFinText,
                ref matchCase,ref matchWholeWord,
                ref matchWildCards,ref matchSoundLike,
                ref nmatchAllforms,ref forward,
                ref wrap,ref format,ref replaceWithText,
                ref replace,ref matchKashida,
                ref matchDiactitics,ref matchAlefHamza,
                ref matchCocntrol);
        }
        private void CreateWordDocument(object filename,object SaveAs)
        {
            word.Application wordApp = new word.Application();
            object missing = Missing.Value;
            word.Document myWordDoc = null;
            if(File.Exists((string )filename))
            {
                object readOnly = false;
                object isVisible = false;
                wordApp.Visible = false;

                myWordDoc = wordApp.Documents.Open(ref filename, ref missing, ref readOnly,
                                                 ref missing, ref missing, ref missing,
                                                 ref missing, ref missing, ref missing,
                                                 ref missing, ref missing, ref missing,
                                                 ref missing, ref missing, ref missing, ref missing);
                myWordDoc.Activate();

                //find and replace
                this.FinAndReplace(wordApp, "{MaHD}",txtMaHD.Text);
                this.FinAndReplace(wordApp, "{TenNV}", txtTenNV.Text);
                this.FinAndReplace(wordApp, "{NgayBan}", dateTimePicker1.Value);
                this.FinAndReplace(wordApp, "{HoTen}", txtTenKH.Text);
                this.FinAndReplace(wordApp, "{Diachi}", txtDc.Text);
                this.FinAndReplace(wordApp, "{SDT}", txtSDT.Text);
                this.FinAndReplace(wordApp, "{MaSP}", txtMaSP.Text);
                this.FinAndReplace(wordApp, "{TenSP}", txtTenSP.Text);
                this.FinAndReplace(wordApp, "{Soluong}", txtSL.Text);
                this.FinAndReplace(wordApp, "{DonGia}", txtDG.Text);
                this.FinAndReplace(wordApp, "{Giamgia}", txtGG.Text);
                this.FinAndReplace(wordApp, "{Thanhtien}", txtTT.Text);
            }
            else
            {
                MessageBox.Show("File is not Found!");
            }

            //save
            myWordDoc.SaveAs2(ref SaveAs,ref missing, ref missing, ref missing,
                              ref missing, ref missing, ref missing,
                              ref missing, ref missing, ref missing,
                              ref missing, ref missing, ref missing, 
                              ref missing, ref missing, ref missing);
            myWordDoc.Close();
            wordApp.Quit();
            MessageBox.Show(" In hóa đơn thành công !");        }
        public ChitietHD()
        {
            InitializeComponent();
        }
        private void showdsCtHD()
        {
            ChitietHDBLL cthdbll = new ChitietHDBLL();
            List<ChitietHDcs> ds = cthdbll.showdsctHD();         
            listView1.Items.Clear();
            foreach (ChitietHDcs item in ds)
            {

                ListViewItem lvi = new ListViewItem(item.MaSP);
                lvi.SubItems.Add(item.TenSP);
                lvi.SubItems.Add(item.SoLuong.ToString());
                lvi.SubItems.Add(item.GiamGia.ToString());
                lvi.SubItems.Add(item.DonGia.ToString());
                lvi.SubItems.Add(item.ThanhTien.ToString());
                lvi.SubItems.Add(item.MaHD);
                lvi.SubItems.Add(item.MaNV);
                lvi.SubItems.Add(item.NgayBan.ToString());
                lvi.SubItems.Add(item.MaKH);
                listView1.Items.Add(lvi);
            }

        }
        private void dsKH()
        {
            ChitietHDBLL cthdbll = new ChitietHDBLL();
            List<KhachHang> ds = cthdbll.showdsKH();
            foreach(KhachHang item in ds)
            {
                comboBoxMaKH.Items.Add(item.MaKH);     
            }
        }
        private void dsNV()
        {
            ChitietHDBLL cthdbll = new ChitietHDBLL();
            List<NhanVien> ds = cthdbll.dsNV();
            foreach (NhanVien item in ds)
            {
                comboBoxMaNV.Items.Add(item.MaNV);
            }
        }
        private void dsSP()
        {
            ChitietHDBLL cthdbll = new ChitietHDBLL();
            List<SanPham> ds = cthdbll.dsSP();
            foreach (SanPham item in ds)
            {
                comboBoxMaSP.Items.Add(item.MaSP);
            }
        }
        private void ChitietHD_Load(object sender, EventArgs e)
        {
            showdsCtHD();
            dsKH();
            dsNV();
            dsSP();

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                txtMaSP.Text = listView1.SelectedItems[0].SubItems[0].Text;
                txtTenSP.Text = listView1.SelectedItems[0].SubItems[1].Text;
                txtGG.Text = listView1.SelectedItems[0].SubItems[3].Text;
                txtSL.Text = listView1.SelectedItems[0].SubItems[2].Text;
                txtDG.Text = listView1.SelectedItems[0].SubItems[4].Text;
                txtTT.Text = listView1.SelectedItems[0].SubItems[5].Text;
                txtMaHD.Text = listView1.SelectedItems[0].SubItems[6].Text;
                txtMaNV.Text = listView1.SelectedItems[0].SubItems[7].Text;
                dateTimePicker1.Value = DateTime.Parse(listView1.SelectedItems[0].SubItems[8].Text);
                txtMaKH.Text = listView1.SelectedItems[0].SubItems[9].Text;
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChitietHDBLL cthdbll = new ChitietHDBLL();
            List<KhachHang> ds = cthdbll.showdsKH();
            foreach (KhachHang item in ds)
            {
                if (comboBoxMaKH.SelectedItem.ToString() == item.MaKH)
                {
                    txtTenKH.Text = item.HoTen;
                    txtDc.Text = item.DiaChi;
                    txtSDT.Text = item.SDT;
                    txtMaKH.Text = item.MaKH;
                }
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChitietHDBLL cthdbll = new ChitietHDBLL();
            List<NhanVien> ds = cthdbll.dsNV();
            foreach (NhanVien item in ds)
            {
                if(comboBoxMaNV.SelectedItem.ToString()==item.MaNV)
                {
                    txtTenNV.Text = item.TenNV;
                    txtMaNV.Text = item.MaNV;
                }
            }
        }

        private void iconButtonThem_Click(object sender, EventArgs e)
        {
            if (txtMaHD.Text.Trim().Length == 0)
            {
                this.errorProvider1.SetError(txtMaHD, "Chưa nhập mã hóa đơn");
                txtMaHD.Focus();
            }
            else if (txtSL.Text.Trim().Length == 0)
            {
                this.errorProvider1.SetError(txtSL, "Chưa nhập số lượng");
                txtSL.Focus();
                if (txtSL.Text.Trim().Length > 0 && !char.IsDigit(txtSL.Text, txtSL.Text.Length - 1))
                {
                    this.errorProvider1.SetError(txtSL, "số lượng phải là số");
                    txtSL.Focus();
                }
            }
            else if (txtGG.Text.Trim().Length == 0)
            {
                this.errorProvider1.SetError(txtGG, "Chưa nhập giảm giá");
                txtGG.Focus();
                if (txtGG.Text.Trim().Length > 0 && !char.IsDigit(txtGG.Text, txtGG.Text.Length - 1))
                {
                    this.errorProvider1.SetError(txtGG, "giảm giá phải là số");
                    txtGG.Focus();
                }
            }
            else if (txtMaHD.Text.Trim().Length != 0 )
            {
                this.errorProvider1.Clear();
                ChitietHDcs cthd = new ChitietHDcs();
                cthd.MaHD = txtMaHD.Text.Trim();
                cthd.MaNV = txtMaNV.Text.Trim();               
                cthd.NgayBan = dateTimePicker1.Value;
                cthd.MaKH = txtMaKH.Text.Trim();
                cthd.MaSP = txtMaSP.Text.Trim();
                cthd.TenSP = txtTenSP.Text.Trim();
                cthd.GiamGia =double.Parse(txtGG.Text.Trim());
                cthd.SoLuong = double.Parse(txtSL.Text.Trim());
                cthd.DonGia = double.Parse(txtDG.Text.Trim());
                cthd.ThanhTien = double.Parse(txtTT.Text.Trim());
                cthd.TongTien = double.Parse(txtTT.Text.Trim());
                ChitietHDBLL cthdbll = new ChitietHDBLL();
                List<ChitietHDcs> ds = cthdbll.showdsctHD();
                foreach (ChitietHDcs item in ds)
                {

                    if (cthd.MaHD==item.MaHD)
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
                            bool kt = cthdbll.ThemctHD(cthd);
                            if (kt)
                            {
                                showdsCtHD();
                                MessageBox.Show("Thêm hóa đơn thành công!");
                            }
                        }
                        catch (Exception) { }
                    }
                }


            }
        }

        private void iconButtonSua_Click(object sender, EventArgs e)
        {

        }

        private void iconButtonXoa_Click(object sender, EventArgs e)
        {
            DialogResult r;
            r = MessageBox.Show("Bạn muốn xóa hoá đơn này?", "",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (r == DialogResult.Yes)
            {
                ChitietHDcs cthd = new ChitietHDcs();
                cthd.MaHD = txtMaHD.Text;
                ChitietHDBLL cthdbll = new ChitietHDBLL();
                bool kt = cthdbll.XoactHD(cthd);
                if (kt)
                {
                    showdsCtHD();
                    MessageBox.Show("Xóa hóa đơn thành công!");
                }
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChitietHDBLL cthdbll = new ChitietHDBLL();
            List<SanPham> ds = cthdbll.dsSP();
            foreach (SanPham item in ds)
            {
                if (comboBoxMaSP.SelectedItem.ToString() == item.MaSP)
                {
                    txtMaSP.Text = item.MaSP;
                    txtTenSP.Text = item.TenSP;
                    txtDG.Text = item.DonGiaBan.ToString();
                }
            }
        }

        private void iconButtonCal_Click(object sender, EventArgs e)
        {
            if (txtDG.Text.Trim().Length == 0)
            { 
                this.errorProvider1.SetError(txtDG, "Chưa nhập đơn giá");
                txtDG.Focus();
                if (txtDG.Text.Trim().Length > 0 && !char.IsDigit(txtDG.Text, txtDG.Text.Length - 1))
                {
                    this.errorProvider1.SetError(txtDG, "đơn giá phải là số");
                    txtDG.Focus();
                }
            }
            else if(txtSL.Text.Trim().Length == 0)
            {
                this.errorProvider1.SetError(txtSL, "Chưa nhập số lượng ");
                txtSL.Focus();
                if (txtSL.Text.Trim().Length > 0 && !char.IsDigit(txtSL.Text, txtSL.Text.Length - 1))
                {
                    this.errorProvider1.SetError(txtSL, "số lượng phải là số");
                    txtSL.Focus();
                }
            }
            else if (txtGG.Text.Trim().Length == 0)
            {
                this.errorProvider1.SetError(txtGG, "Chưa nhập giảm giá");
                txtGG.Focus();
                if (txtGG.Text.Trim().Length > 0 && !char.IsDigit(txtGG.Text, txtGG.Text.Length - 1))
                {
                    this.errorProvider1.SetError(txtSL, "giảm giá phải là số");
                    txtGG.Focus();
                }
            }
            else
            {
                this.errorProvider1.Clear();
                double dg = double.Parse(txtDG.Text.Trim());
                double sl = double.Parse(txtSL.Text.Trim());
                double gg = double.Parse(txtGG.Text.Trim());
                double tt = (dg * sl) - gg;
                txtTT.Text = tt.ToString();
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
     
        private void searchMaHD()
        {
            con = new SqlConnection(str);
            sql = @"select a.MaSP,b.TenSP,a.SoLuong,a.GiamGia,a.DonGia,a.ThanhTien,a.MaHD,c.MaNV,c.NgayBan,c.MaKH,c.TongTien from tblChiTietHD As a, tblSP As b,tblHoaDon As c where a.MaSP=b.MaSP and a.MaHD=c.MaHD and a.MaHD = N'" + txtSearch.Text + "'";
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
                lvi.SubItems.Add(read[8].ToString());
                lvi.SubItems.Add(read[9].ToString());
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
        }

        private void iconButtonReset_Click(object sender, EventArgs e)
        {
            showdsCtHD();
        }

        private void iconButtonInHD_Click(object sender, EventArgs e)
        {
            CreateWordDocument(@"D:\Chuyen De .Net\QLBanXeMoTo\GUI\temp.docx", @"C:\Users\Administrator\Desktop\hoadon.docx");
        }
    }
}
