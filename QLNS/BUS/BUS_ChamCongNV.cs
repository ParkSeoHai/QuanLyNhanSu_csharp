using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class BUS_ChamCongNV
    {
        QuanLy quanLy = new QuanLy();
        // Hiển thị danh sách nhân viên để chấm công
        public DataTable HienThiNV_ChamCong()
        {
            return quanLy.HienThiNV_ChamCong();
        }
        // Chấm công nhân viên
        public bool ChamCongNV(ChamCong CC)
        {
            if(quanLy.ChamCongNV(CC))
            {
                return true;
            }
            return false;
        }
        // Lấy list mã nv để check chấm công
        public List<string> Get_ListMaNV(int day, int month, int year)
        {
            return quanLy.Get_ListMaNV(day, month, year);
        }
    }
}
