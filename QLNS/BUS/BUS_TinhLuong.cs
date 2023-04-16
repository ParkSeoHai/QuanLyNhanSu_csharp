using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class BUS_TinhLuong
    {
        QuanLy quanLy = new QuanLy();
        // Hiển thị bảng lương
        public List<LuongNV> HienThi_Luong(int ThangLam, int NamLam)
        {
            return quanLy.HienThi_Luong(ThangLam, NamLam);
        }
    }
}
