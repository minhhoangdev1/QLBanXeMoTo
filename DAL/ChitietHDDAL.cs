using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DTO;


namespace DAL
{
    public class ChitietHDDAL:DataBase
    {
        public List<ChitietHDcs> showdsctHD()
        {
            List<ChitietHDcs> ds = new List<ChitietHDcs>();
            OpenConnection();
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "select a.MaSP,b.TenSP,a.SoLuong,a.GiamGia,a.DonGia,a.ThanhTien,a.MaHD,c.MaNV,c.NgayBan,c.MaKH,c.TongTien from tblChiTietHD As a, tblSP As b,tblHoaDon As c where a.MaSP=b.MaSP and a.MaHD=c.MaHD ";
            sqlCmd.Connection = sqlCon;
            SqlDataReader reader = sqlCmd.ExecuteReader();
            while (reader.Read())
            {    
                string masp = reader.GetString(0);
                string tensp = reader.GetString(1);
                double sl = (double)reader.GetDouble(2);
                double dg = (double)reader.GetDouble(4);
                double gg = (double)reader.GetDouble(3);
                double tt = (double)reader.GetDouble(5);
                string mahd = reader.GetString(6);
                string manv = reader.GetString(7);
                DateTime ngayban = reader.GetDateTime(8);
                string makh = reader.GetString(9);
                double tongtien = (double)reader.GetDouble(10);

                ChitietHDcs cthd = new ChitietHDcs();
                cthd.MaSP = masp;
                cthd.TenSP = tensp;
                cthd.SoLuong = sl;
                cthd.GiamGia = gg;
                cthd.DonGia = dg;     
                cthd.ThanhTien = tt;
                cthd.MaHD = mahd;
                cthd.MaNV = manv;
                cthd.NgayBan = ngayban;
                cthd.MaKH = makh;
                cthd.TongTien = tongtien;
                ds.Add(cthd);
            }
            reader.Close();
            CloseConnection();
            return ds;
        }
        public List<NhanVien> showdsNV()
        {
            List<NhanVien> ds = new List<NhanVien>();
            OpenConnection();
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "select MaNV,TenNV from tblNhanVien";
            sqlCmd.Connection = sqlCon;
            SqlDataReader reader = sqlCmd.ExecuteReader();
            while (reader.Read())
            {
                string ma = reader.GetString(0);
                string ten = reader.GetString(1);
                NhanVien nv = new NhanVien();
                nv.MaNV = ma;
                nv.TenNV = ten;
                ds.Add(nv);
            }
            reader.Close();
            CloseConnection();
            return ds;
        }
        public List<SanPham> dsSP()
        {
            List<SanPham> ds = new List<SanPham>();
            OpenConnection();
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "select MaSP,TenSP,DonGiaBan from tblSP";
            sqlCmd.Connection = sqlCon;
            SqlDataReader reader = sqlCmd.ExecuteReader();
            while (reader.Read())
            {
                string ma = reader.GetString(0);
                string ten = reader.GetString(1);
                double dgb =(double)reader.GetDouble(2);
                SanPham sp = new SanPham();
                sp.MaSP = ma;
                sp.TenSP = ten;
                sp.DonGiaBan = dgb;
                ds.Add(sp);
            }
            reader.Close();
            CloseConnection();
            return ds;
        }
        public bool ThemctHD(ChitietHDcs cthd)
        {
            OpenConnection();
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "insert into tblChiTietHD values (@mahd,@masp, @sl,@dg,@gg,@tt)" +
                                 " insert into tblHoaDon values (@mahd, @manv,@ngayban,@makh,@tongtien)";
            SqlParameter parMahd = new SqlParameter("@mahd", SqlDbType.NVarChar);
            parMahd.Value = cthd.MaHD;
            sqlCmd.Parameters.Add(parMahd);
            SqlParameter parMasp = new SqlParameter("@masp", SqlDbType.NVarChar);
            parMasp.Value = cthd.MaSP;
            sqlCmd.Parameters.Add(parMasp);
            SqlParameter parsl = new SqlParameter("@sl", SqlDbType.Float);
            parsl.Value = cthd.SoLuong;
            sqlCmd.Parameters.Add(parsl);
            SqlParameter pardg = new SqlParameter("@dg", SqlDbType.Float);
            pardg.Value = cthd.DonGia;
            sqlCmd.Parameters.Add(pardg);
            SqlParameter pargg = new SqlParameter("@gg", SqlDbType.Float);
            pargg.Value = cthd.GiamGia;
            sqlCmd.Parameters.Add(pargg);
            SqlParameter partt = new SqlParameter("@tt", SqlDbType.Float);
            partt.Value = cthd.ThanhTien;
            sqlCmd.Parameters.Add(partt);
            SqlParameter parMaNV = new SqlParameter("@manv", SqlDbType.NVarChar);
            parMaNV.Value = cthd.MaNV;
            sqlCmd.Parameters.Add(parMaNV);
            SqlParameter parMaKH = new SqlParameter("@makh", SqlDbType.NVarChar);
            parMaKH.Value = cthd.MaKH;
            sqlCmd.Parameters.Add(parMaKH);
            SqlParameter parTT = new SqlParameter("@tongtien", SqlDbType.Float);
            parTT.Value = cthd.TongTien;
            sqlCmd.Parameters.Add(parTT);
            SqlParameter parNgayb = new SqlParameter("@ngayban", SqlDbType.DateTime);
            parNgayb.Value = cthd.NgayBan;
            sqlCmd.Parameters.Add(parNgayb);
            sqlCmd.Connection = sqlCon;
            int kt = sqlCmd.ExecuteNonQuery();
            CloseConnection();
            if (kt > 0)
            {
                return true;
            }
            else
            {
                return false;
            }  
        }
        public bool XoactHD(ChitietHDcs hd)
        {
            OpenConnection();
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "delete from tblChiTietHD where MaHD = @mahd" +
                                 " delete from tblHoaDon where MaHD = @mahd";
            SqlParameter parMa = new SqlParameter("@mahd", SqlDbType.NVarChar);
            parMa.Value = hd.MaHD;
            sqlCmd.Parameters.Add(parMa);
            sqlCmd.Connection = sqlCon;
            int kt = sqlCmd.ExecuteNonQuery();
            CloseConnection();
            if (kt > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /*public bool SuaNV(NhanVien nv)
        {
            OpenConnection();
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            //@ma, @ten, @gt, @dc, @sdt, @ngaysinh
            sqlCmd.CommandText = "update tblNhanVien set MaNV= @ma, TenNV = @ten, GioiTinh =@gt, DiaChi = @dc,SDT=@sdt,NgaySinh=@ngaysinh where MaNV = @ma ";
            SqlParameter parMa = new SqlParameter("@ma", SqlDbType.NVarChar);
            parMa.Value = nv.MaNV;
            sqlCmd.Parameters.Add(parMa);
            SqlParameter parTen = new SqlParameter("@ten", SqlDbType.NVarChar);
            parTen.Value = nv.TenNV;
            sqlCmd.Parameters.Add(parTen);
            SqlParameter parGT = new SqlParameter("@gt", SqlDbType.NVarChar);
            parGT.Value = nv.GioiTinh;
            sqlCmd.Parameters.Add(parGT);
            SqlParameter parDC = new SqlParameter("@dc", SqlDbType.NVarChar);
            parDC.Value = nv.DiaChi;
            sqlCmd.Parameters.Add(parDC);
            SqlParameter parSDT = new SqlParameter("@sdt", SqlDbType.NVarChar);
            parSDT.Value = nv.SDT;
            sqlCmd.Parameters.Add(parSDT);
            SqlParameter parNS = new SqlParameter("@ngaysinh", SqlDbType.DateTime);
            parNS.Value = nv.NgaySinh;
            sqlCmd.Parameters.Add(parNS);
            sqlCmd.Connection = sqlCon;
            int kt = sqlCmd.ExecuteNonQuery();
            if (kt > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }*/
    }
}
