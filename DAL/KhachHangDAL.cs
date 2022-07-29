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
    public class KhachHangDAL:DataBase
    {
        public List<KhachHang> showdsKH()
        {
            List<KhachHang> ds = new List<KhachHang>();
            OpenConnection();
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "select * from tblKhachHang";
            sqlCmd.Connection = sqlCon;
            SqlDataReader reader = sqlCmd.ExecuteReader();
            while (reader.Read())
            {
                string ma = reader.GetString(0);
                string ten = reader.GetString(1);
                string sdt = reader.GetString(2);
                string dc = reader.GetString(3);
                KhachHang kh = new KhachHang();
                kh.MaKH = ma;
                kh.HoTen = ten;
                kh.SDT = sdt;
                kh.DiaChi = dc;
                ds.Add(kh);
            }
            reader.Close();
            CloseConnection();
            return ds;
        }
        public List<KhachHang> tongKH()
        {
            List<KhachHang> ds = new List<KhachHang>();
            OpenConnection();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.CommandType = CommandType.Text;
            sqlcmd.CommandText = "select count(MaKH) As [TongKH] from tblKhachHang";
            sqlcmd.Connection = sqlCon;
            SqlDataReader reader = sqlcmd.ExecuteReader();
            while (reader.Read())
            {
                int tongkh = (int)reader.GetInt32(0);
                KhachHang kh = new KhachHang();
                kh.TongKH = tongkh;
                ds.Add(kh);
            }
            reader.Close();
            CloseConnection();
            return ds;
        }
        public bool ThemKH(KhachHang kh)
        {
            OpenConnection();
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "insert into tblKhachHang values (@ma, @ten, @sdt, @dc)";
            SqlParameter parMa = new SqlParameter("@ma", SqlDbType.NVarChar);
            parMa.Value = kh.MaKH;
            sqlCmd.Parameters.Add(parMa);
            SqlParameter parTen = new SqlParameter("@ten", SqlDbType.NVarChar);
            parTen.Value = kh.HoTen;
            sqlCmd.Parameters.Add(parTen);
            SqlParameter parSDT = new SqlParameter("@sdt", SqlDbType.NVarChar);
            parSDT.Value = kh.SDT;
            sqlCmd.Parameters.Add(parSDT);
            SqlParameter parDC = new SqlParameter("@dc", SqlDbType.NVarChar);
            parDC.Value = kh.DiaChi;
            sqlCmd.Parameters.Add(parDC);
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
        public bool XoaKH(KhachHang kh)
        {
            OpenConnection();
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "delete from tblKhachHang where MaKH = @ma ";
            SqlParameter parMa = new SqlParameter("@ma", SqlDbType.Char);
            parMa.Value = kh.MaKH;
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
        public bool SuaKH(KhachHang kh)
        {
            OpenConnection();
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            //@ma, @ten, @gt, @dc, @sdt, @ngaysinh
            sqlCmd.CommandText = "update tblKhachHang set MaKH= @ma, HoTen = @ten, DiaChi = @dc,SDT=@sdt where MaKH = @ma ";
            SqlParameter parMa = new SqlParameter("@ma", SqlDbType.NVarChar);
            parMa.Value = kh.MaKH;
            sqlCmd.Parameters.Add(parMa);
            SqlParameter parTen = new SqlParameter("@ten", SqlDbType.NVarChar);
            parTen.Value = kh.HoTen;
            sqlCmd.Parameters.Add(parTen);
            SqlParameter parSDT = new SqlParameter("@sdt", SqlDbType.NVarChar);
            parSDT.Value = kh.SDT;
            sqlCmd.Parameters.Add(parSDT);
            SqlParameter parDC = new SqlParameter("@dc", SqlDbType.NVarChar);
            parDC.Value = kh.DiaChi;
            sqlCmd.Parameters.Add(parDC);
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
