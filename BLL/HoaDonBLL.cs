using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;

namespace BLL
{
    public class HoaDonBLL
    {
        HoaDonDAL hdDAL = new HoaDonDAL();
        public List<HoaDon> showdsHD()
        {
            return hdDAL.showdsHD();
        }
        public bool ThemHD(HoaDon hd)
        {
            return hdDAL.ThemHD(hd);
        }
        public bool XoaHD(HoaDon hd)
        {
            return hdDAL.XoaHD(hd);
        }
        public bool SuaHD(HoaDon hd)
        {
            return hdDAL.SuaHD(hd);
        }
        public List<HoaDon> TongHD()
        {
            return hdDAL.tongHD();
        }
        public List<HoaDon> TongDT()
        {
            return hdDAL.tongDT();
        }
    }
}
