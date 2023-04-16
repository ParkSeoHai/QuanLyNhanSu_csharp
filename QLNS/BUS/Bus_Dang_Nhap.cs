using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BUS
{
    public class Bus_Dang_Nhap
    {
        QuanLy quanLy = new QuanLy();
        public bool bus_dangNhap(string tenTK, string matKhau)
        {
            if (quanLy.DangNhap(tenTK, matKhau))
            {
                return true;
            }else
            {
                return false;
            }
        }
    }
}
