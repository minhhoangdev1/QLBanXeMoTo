using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;

namespace BLL
{
    public class ThuongHieuBLL
    {
        ThuongHieuDAL thDAL = new ThuongHieuDAL();
        public List<ThuongHieu> showdsTH()
        {
            return thDAL.showdsTH();
        }
        public bool ThemTH(ThuongHieu th)
        {
            return thDAL.ThemTH(th);
        }
        public bool XoaTH(ThuongHieu th)
        {
            return thDAL.XoaTH(th);
        }
        public bool SuaTH(ThuongHieu th)
        {
            return thDAL.SuaTH(th);
        }
    }
}
