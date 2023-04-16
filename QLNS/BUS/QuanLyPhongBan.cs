using DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class QuanLyPhongBan
    {
        QuanLy quanLy = new QuanLy();
        public bool themPhongBan(PhongBan phongBan)
        {
            if (quanLy.ThemPhongBan(phongBan))
            {
                return true;
            }else
            {
                return false;
            }
        }
        public bool suaPhongBan(PhongBan phongBan)
        {
            if (quanLy.SuaPhongBan(phongBan))
                return true;
            return false;
        }
        public bool xoaPhongBan(string maPB)
        {
            if (quanLy.XoaPhongBan(maPB))
            {
                return true;
            }else
            {
                 return false;
            }
        }
        public SqlDataReader timPhongBan(string data)
        {
            if (quanLy.TimPhongBan(data) != null)
            {
                return quanLy.TimPhongBan(data);
            }else
            {
                return null;
            }
        }
    }
}
