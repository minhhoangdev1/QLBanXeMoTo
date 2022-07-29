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
    public class HoaDonDAL:DataBase
    {
        public List<HoaDon> showdsHD()
        {
            List<HoaDon> ds = new List<HoaDon>();
            OpenConnection();
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "select * from tblHoaDon";
            sqlCmd.Connection = sqlCon;
            SqlDataReader reader = sqlCmd.ExecuteReader();
            while (reader.Read())
            {
                string mahd = reader.GetString(0);
                string manv = reader.GetString(1);
                DateTime ngban = reader.GetDateTime(2);
                string makh = reader.GetString(3);
                double tt = (double)reader.GetDouble(4);
                
                HoaDon hd = new HoaDon();
                hd.MaHD = mahd;
                hd.MaNV = manv;
                hd.MaKH = makh;
                hd.TongTien = tt;
                hd.NgayBan = ngban;
                ds.Add(hd);
            }
            reader.Close();
            CloseConnection();
            return ds;
        }
        public List<HoaDon> tongHD()
        {
            List<HoaDon> ds = new List<HoaDon>();
            OpenConnection();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.CommandType = CommandType.Text;
            sqlcmd.CommandText = "select count(MaHD) As [TongHD] from tblHoaDon";
            sqlcmd.Connection = sqlCon;
            SqlDataReader reader = sqlcmd.ExecuteReader();
            while (reader.Read())
            {
                int tonghd = (int)reader.GetInt32(0);
                HoaDon hd = new HoaDon();
                hd.TongHD = tonghd;
                ds.Add(hd);
            }
            reader.Close();
            CloseConnection();
            return ds;
        }
        public List<HoaDon> tongDT()
        {
            List<HoaDon> ds = new List<HoaDon>();
            OpenConnection();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.CommandType = CommandType.Text;
            sqlcmd.CommandText = "select sum(TongTien) As [TongDT] from tblHoaDon";
            sqlcmd.Connection = sqlCon;
            SqlDataReader reader = sqlcmd.ExecuteReader();
            while (reader.Read())
            {
                double tongdt = (double)reader.GetDouble(0);
                HoaDon hd = new HoaDon();
                hd.TongDT = tongdt;
                ds.Add(hd);
            }
            reader.Close();
            CloseConnection();
            return ds;
        }
        public bool ThemHD(HoaDon hd)
        {
            OpenConnection();
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "insert into tblHoaDon values (@mahd, @manv,@ngban,@makh,@tt)";
            SqlParameter parMaHD = new SqlParameter("@mahd", SqlDbType.NVarChar);
            parMaHD.Value = hd.MaHD;
            sqlCmd.Parameters.Add(parMaHD);
            SqlParameter parMaNV = new SqlParameter("@manv", SqlDbType.NVarChar);
            parMaNV.Value = hd.MaNV;
            sqlCmd.Parameters.Add(parMaNV);
            SqlParameter parMaKH = new SqlParameter("@makh", SqlDbType.NVarChar);
            parMaKH.Value = hd.MaKH;
            sqlCmd.Parameters.Add(parMaKH);
            SqlParameter parTT = new SqlParameter("@tt", SqlDbType.Float);
            parTT.Value = hd.TongTien;
            sqlCmd.Parameters.Add(parTT);
            SqlParameter parNgayb = new SqlParameter("@ngban", SqlDbType.DateTime);
            parNgayb.Value = hd.NgayBan;
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
        public bool XoaHD(HoaDon hd)
        {
            OpenConnection();
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "delete from tblHoaDon where MaHD = @mahd ";
            SqlParameter parMaHD = new SqlParameter("@mahd", SqlDbType.NVarChar);
            parMaHD.Value = hd.MaHD;
            sqlCmd.Parameters.Add(parMaHD);
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
        public bool SuaHD(HoaDon hd)
        {
            OpenConnection();
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "update tblHoaDon set MaHD= @mahd, MaNV = @manv,MaKH=@makh,TongTien=@tt,NgayBan=@ngban where MaHD = @mahd ";
            SqlParameter parMaHD = new SqlParameter("@mahd", SqlDbType.NVarChar);
            parMaHD.Value = hd.MaHD;
            sqlCmd.Parameters.Add(parMaHD);
            SqlParameter parMaNV = new SqlParameter("@manv", SqlDbType.NVarChar);
            parMaNV.Value = hd.MaNV;
            sqlCmd.Parameters.Add(parMaNV);
            SqlParameter parMaKH = new SqlParameter("@makh", SqlDbType.NVarChar);
            parMaKH.Value = hd.MaKH;
            sqlCmd.Parameters.Add(parMaKH);
            SqlParameter parTT = new SqlParameter("@tt", SqlDbType.Float);
            parTT.Value = hd.TongTien;
            sqlCmd.Parameters.Add(parTT);
            SqlParameter parNgayb = new SqlParameter("@ngban", SqlDbType.DateTime);
            parNgayb.Value = hd.NgayBan;
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

    }
}
