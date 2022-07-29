using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;

namespace BLL
{
    public class  NhanVienBLL
    {
        NhanVienDAL nvDAL = new NhanVienDAL();
        public List<NhanVien> showdsNV()
        {
            return nvDAL.showdsNV();
        }
        public bool ThemNV(NhanVien nv)
        {
            return nvDAL.ThemNV(nv);
        }
        public bool XoaNV(NhanVien nv)
        {
            return nvDAL.XoaNV(nv);
        }
        public bool SuaNV(NhanVien nv)
        {
            return nvDAL.SuaNV(nv);
        }
        public List<NhanVien> TongNV()
        {
            return nvDAL.tongNV();
        }
    }
}
