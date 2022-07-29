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
    public class ThuongHieuDAL:DataBase
    {
        public List<ThuongHieu> showdsTH()
        {
            List<ThuongHieu> ds = new List<ThuongHieu>();
            OpenConnection();
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "select * from tblLoaiSP";
            sqlCmd.Connection = sqlCon;
            SqlDataReader reader = sqlCmd.ExecuteReader();
            while (reader.Read())
            {
                string ma = reader.GetString(0);
                string ten = reader.GetString(1);
                ThuongHieu th = new ThuongHieu();
                th.MaLoai = ma;
                th.TenLoai = ten;
                ds.Add(th);
            }
            reader.Close();
            CloseConnection();
            return ds;
        }

        public bool ThemTH(ThuongHieu th)
        {
            OpenConnection();
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "insert into tblLoaiSP values (@ma, @ten)";
            SqlParameter parMa = new SqlParameter("@ma", SqlDbType.NVarChar);
            parMa.Value = th.MaLoai;
            sqlCmd.Parameters.Add(parMa);
            SqlParameter parTen = new SqlParameter("@ten", SqlDbType.NVarChar);
            parTen.Value = th.TenLoai;
            sqlCmd.Parameters.Add(parTen);
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
        public bool XoaTH(ThuongHieu th)
        {
            OpenConnection();
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "delete from tblLoaiSP where MaLoai = @ma ";
            SqlParameter parMa = new SqlParameter("@ma", SqlDbType.Char);
            parMa.Value = th.MaLoai;
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
        public bool SuaTH(ThuongHieu th)
        {
            OpenConnection();
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "update tblLoaiSP set MaLoai= @ma, TenLoai = @ten where MaLoai = @ma ";
            SqlParameter parMa = new SqlParameter("@ma", SqlDbType.NVarChar);
            parMa.Value = th.MaLoai;
            sqlCmd.Parameters.Add(parMa);
            SqlParameter parTen = new SqlParameter("@ten", SqlDbType.NVarChar);
            parTen.Value = th.TenLoai;
            sqlCmd.Parameters.Add(parTen);
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
