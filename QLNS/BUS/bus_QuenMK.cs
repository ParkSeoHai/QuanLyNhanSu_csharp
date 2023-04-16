using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class bus_QuenMK
    {
        QuanLy quanLy = new QuanLy();
        public string bus_Quen_Mat_Khau(string tenTK)
        {
            if (quanLy.quenMatKhau(tenTK) != "err")
            {
                return quanLy.quenMatKhau(tenTK);
            }else
            {
                return "err";
            }
        }
    }
}
