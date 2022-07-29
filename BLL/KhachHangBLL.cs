using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;

namespace BLL
{
    public class KhachHangBLL
    {
        KhachHangDAL khDAL = new KhachHangDAL();
        public List<KhachHang> showdsKH()
        {
            return khDAL.showdsKH();
        }
        public bool ThemKH(KhachHang kh)
        {
            return khDAL.ThemKH(kh);
        }
        public bool XoaKH(KhachHang kh)
        {
            return khDAL.XoaKH(kh);
        }
        public bool SuaKH(KhachHang kh)
        {
            return khDAL.SuaKH(kh);
        }
        public List<KhachHang> TongKH()
        {
            return khDAL.tongKH();
        }
    }
}
