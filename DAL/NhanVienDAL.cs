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
    public class NhanVienDAL:DataBase
    {
        public List<NhanVien> showdsNV()
        {
            List<NhanVien> ds = new List<NhanVien>();
            OpenConnection();
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "select * from tblNhanVien";
            sqlCmd.Connection = sqlCon;
            SqlDataReader reader = sqlCmd.ExecuteReader();
            while (reader.Read())
            {
                string ma = reader.GetString(0);
                string ten = reader.GetString(1);
                string gt = reader.GetString(2);
                string dc = reader.GetString(3);
                string sdt = reader.GetString(4);
                DateTime ngaysinh = reader.GetDateTime(5);
                NhanVien nv = new NhanVien();
                nv.MaNV = ma;
                nv.TenNV = ten;
                nv.GioiTinh = gt;
                nv.DiaChi = dc;
                nv.SDT = sdt;
                nv.NgaySinh = ngaysinh;
                ds.Add(nv);
            }
            reader.Close();
            CloseConnection();
            return ds;
        }
        public List<NhanVien> tongNV()
        {
            List<NhanVien> ds = new List<NhanVien>();
            OpenConnection();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.CommandType = CommandType.Text;
            sqlcmd.CommandText = "select count(MaNV) As [TongNV] from tblNhanVien";
            sqlcmd.Connection = sqlCon;
            SqlDataReader reader = sqlcmd.ExecuteReader();
            while (reader.Read())
            {
                int tongnv = (int)reader.GetInt32(0);
                NhanVien nv = new NhanVien();
                nv.TongNV = tongnv;
                ds.Add(nv);    
            }
            reader.Close();
            CloseConnection();
            return ds;
        }
        public bool ThemNV(NhanVien nv)
        {
            OpenConnection();
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "insert into tblNhanVien values (@ma, @ten, @gt, @dc, @sdt, @ngaysinh)";
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
        public bool XoaNV(NhanVien nv)
        {
            OpenConnection();
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "delete from tblNhanVien where MaNV = @ma ";
            SqlParameter parMa = new SqlParameter("@ma", SqlDbType.NVarChar);
            parMa.Value = nv.MaNV;
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
        public bool SuaNV(NhanVien nv)
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
    }
}
