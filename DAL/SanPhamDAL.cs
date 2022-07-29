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
    public class SanPhamDAL:DataBase
    {
        public List<SanPham> showdsSP()
        {
            List<SanPham> ds = new List<SanPham>();
            OpenConnection();
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "select * from tblSP";
            sqlCmd.Connection = sqlCon;
            SqlDataReader reader = sqlCmd.ExecuteReader();
            while (reader.Read())
            {
                string ma = reader.GetString(0);
                string ten = reader.GetString(1);
                string mal = reader.GetString(2);
                double sl = (double)reader.GetDouble(3);
                double dgn = (double)reader.GetDouble(4);
                double dgb = (double)reader.GetDouble(5);
                string anh = reader.GetString(6);
                string ghichu = reader.GetString(7);
                SanPham sp = new SanPham();
                sp.MaSP = ma;
                sp.TenSP = ten;
                sp.MaLoai = mal;
                sp.SoLuong = sl;
                sp.DonGiaNhap = dgn;
                sp.DonGiaBan = dgb;
                sp.Anh = anh;
                sp.GhiChu = ghichu;
                ds.Add(sp);
            }
            reader.Close();
            CloseConnection();
            return ds;
        }
        public bool ThemSP(SanPham sp)
        {
            OpenConnection();
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "insert into tblSP values (@ma, @ten, @mal, @sl, @dgn, @dgb,@anh,@ghichu)";
            SqlParameter parMa = new SqlParameter("@ma", SqlDbType.NVarChar);
            parMa.Value = sp.MaSP;
            sqlCmd.Parameters.Add(parMa);
            SqlParameter parTen = new SqlParameter("@ten", SqlDbType.NVarChar);
            parTen.Value = sp.TenSP;
            sqlCmd.Parameters.Add(parTen);
            SqlParameter parMaL = new SqlParameter("@mal", SqlDbType.NVarChar);
            parMaL.Value = sp.MaLoai;
            sqlCmd.Parameters.Add(parMaL);
            SqlParameter parSL = new SqlParameter("@sl", SqlDbType.Float);
            parSL.Value = sp.SoLuong;
            sqlCmd.Parameters.Add(parSL);
            SqlParameter parDGN = new SqlParameter("@dgn", SqlDbType.Float);
            parDGN.Value = sp.DonGiaNhap;
            sqlCmd.Parameters.Add(parDGN);
            SqlParameter parDGB = new SqlParameter("@dgb", SqlDbType.NVarChar);
            parDGB.Value = sp.DonGiaBan;
            sqlCmd.Parameters.Add(parDGB);
            SqlParameter parA = new SqlParameter("@anh", SqlDbType.NVarChar);
            parA.Value = sp.Anh;
            sqlCmd.Parameters.Add(parA);
            SqlParameter parGC = new SqlParameter("@ghichu", SqlDbType.NVarChar);
            parGC.Value = sp.GhiChu;
            sqlCmd.Parameters.Add(parGC);
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
        public bool XoaSP(SanPham sp)
        {
            OpenConnection();
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "delete from tblSP where MaSP = @ma ";
            SqlParameter parMa = new SqlParameter("@ma", SqlDbType.NVarChar);
            parMa.Value = sp.MaSP;
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
        public bool SuaSP(SanPham sp)
        {
            OpenConnection();
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            //@ma, @ten, @mal, @sl, @dgn, @dgb,@anh,@ghichu
            sqlCmd.CommandText = "update tblSP set MaSP= @ma, TenSP = @ten, MaLoai =@mal, SoLuong = @sl,DonGiaNhap=@dgn,DonGiaBan=@dgb,Anh=@anh,GhiChu=@ghichu where MaSP = @ma ";
            SqlParameter parMa = new SqlParameter("@ma", SqlDbType.NVarChar);
            parMa.Value = sp.MaSP;
            sqlCmd.Parameters.Add(parMa);
            SqlParameter parTen = new SqlParameter("@ten", SqlDbType.NVarChar);
            parTen.Value = sp.TenSP;
            sqlCmd.Parameters.Add(parTen);
            SqlParameter parMaL = new SqlParameter("@mal", SqlDbType.NVarChar);
            parMaL.Value = sp.MaLoai;
            sqlCmd.Parameters.Add(parMaL);
            SqlParameter parSL = new SqlParameter("@sl", SqlDbType.Float);
            parSL.Value = sp.SoLuong;
            sqlCmd.Parameters.Add(parSL);
            SqlParameter parDGN = new SqlParameter("@dgn", SqlDbType.Float);
            parDGN.Value = sp.DonGiaNhap;
            sqlCmd.Parameters.Add(parDGN);
            SqlParameter parDGB = new SqlParameter("@dgb", SqlDbType.NVarChar);
            parDGB.Value = sp.DonGiaBan;
            sqlCmd.Parameters.Add(parDGB);
            SqlParameter parA = new SqlParameter("@anh", SqlDbType.NVarChar);
            parA.Value = sp.Anh;
            sqlCmd.Parameters.Add(parA);
            SqlParameter parGC = new SqlParameter("@ghichu", SqlDbType.NVarChar);
            parGC.Value = sp.GhiChu;
            sqlCmd.Parameters.Add(parGC);
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
