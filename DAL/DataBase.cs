using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class DataBase
    {
        //formChitietHD, formHoDon, formKhachHang,formNhanvien,formSanPham, formLogin, formThongKe sẽ có các đường dẫn như này để thực hiện việc tìm kiếm
        string strCon = @"Data Source=NGIO2ZLP2GRNQT1\MSQLSEVER12;Initial Catalog=QLBanXeMoTo;Integrated Security=True";
        protected SqlConnection sqlCon = null;
        public void OpenConnection()
        {
            if (sqlCon == null)
            {
                sqlCon = new SqlConnection(strCon);
            }
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
        }
        public void CloseConnection()
        {
            if (sqlCon.State == ConnectionState.Open && sqlCon != null)
            {
                sqlCon.Close();
            }
        }
    }
}
