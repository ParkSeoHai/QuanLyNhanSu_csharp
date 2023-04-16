using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class BUS_QLDA
    {
        QuanLy quanLy = new QuanLy();

        // Hàm lấy dữ liệu
        public DataTable HienThiDuLieu()
        {
            return quanLy.HienThiDuAn();
        }
        public bool ThemDA(DuAn DA)
        {
            if (quanLy.ThemDuAn(DA)) { return true; }
            return false;
        }
        public bool SuaDA(DuAn DA)
        {
            if (quanLy.SuaDuAn(DA)) { return true; }
            return false;
        }
        public bool XoaDA(DuAn DA)
        {
            if (quanLy.XoaDuAn(DA)) { return true; }
            return false;
        }
        public DataTable TimKiemDA_Ma(string txtMaDA)
        {
            return quanLy.TimKiemDA_Ma(txtMaDA);
        }
        public DataTable TimKiemDA_Ten(string txtTenDA)
        {
            return quanLy.TimKiemDA_Ten(txtTenDA);
        }
    }
}
