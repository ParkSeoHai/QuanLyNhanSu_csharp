using DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class Quan_Ly_Nhan_Vien
    {
        QuanLy quanLy = new QuanLy();
        public bool themNhanVien(NhanVien nhanVien)
        {
            if (quanLy.ThemNhanVien(nhanVien))
            {
                return true;
            }else
            {
                return false;
            }
        }
        public bool suaNhanVien(NhanVien nhanVien)
        {
            if (quanLy.SuaNhanVien(nhanVien))
            {
                return true;
            }else
            {
                return false;
            }
        }
        public bool xoaNhanVien(string daTa)
        {
            if (quanLy.XoaNhanVien(daTa))
            {
                return true;
            }else
            {
                return false;
            }
        }
        public SqlDataReader timNhanVien(string daTa)
        {
            if (quanLy.TimKiemNhanVien(daTa) != null)
            {
                return quanLy.TimKiemNhanVien(daTa);
            }else
            {
                return null;
            }
        }
    }
}
