using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;

namespace BLL
{
    public class ChitietHDBLL
    {
        ChitietHDDAL cthdDAL = new ChitietHDDAL();
        public List<ChitietHDcs> showdsctHD()
        {
            return cthdDAL.showdsctHD();
        }
        KhachHangDAL khDAL = new KhachHangDAL();
        public List<KhachHang> showdsKH()
        {
            return khDAL.showdsKH();
        }
        public List<NhanVien> dsNV()
        {
            return cthdDAL.showdsNV();
        }
        public List<SanPham> dsSP()
        {
            return cthdDAL.dsSP();
        }
        public bool ThemctHD(ChitietHDcs cthd)
        {
            return cthdDAL.ThemctHD(cthd);
        }
        public bool XoactHD(ChitietHDcs cthd)
        {
            return cthdDAL.XoactHD(cthd);
        }
    }
}
