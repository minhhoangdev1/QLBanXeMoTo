using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;

namespace BLL
{
    public class SanPhamBLL
    {
        SanPhamDAL spDAL = new SanPhamDAL();
        public List<SanPham> showdsSP()
        {
            return spDAL.showdsSP();
        }
        public bool ThemSP(SanPham sp)
        {
            return spDAL.ThemSP(sp);
        }
        public bool XoaSP(SanPham sp)
        {
            return spDAL.XoaSP(sp);
        }
        public bool SuaSP(SanPham sp)
        {
            return spDAL.SuaSP(sp);
        }
    }
}
