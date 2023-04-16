using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class LuongNV
    {
        private string _MaNV;
        private string _TenNV;
        private string _ChucVu;
        private int _NgayCong;
        private double _MucLuong;
        private double _TongLuong;

        public string MaNV { get => _MaNV; set => _MaNV = value; }
        public string TenNV { get => _TenNV; set => _TenNV = value; }
        public string ChucVu { get => _ChucVu; set => _ChucVu = value; }
        public int NgayCong { get => _NgayCong; set => _NgayCong = value; }
        public double MucLuong { get => _MucLuong; set => _MucLuong = value; }
        public double TongLuong { get => _TongLuong; set => _TongLuong = value; }

        public LuongNV() { }
        public LuongNV(string maNV, string tenNV, string chucVu, int ngayCong, double mucLuong, double tongLuong)
        {
            _MaNV = maNV;
            _TenNV = tenNV;
            _ChucVu = chucVu;
            _NgayCong = ngayCong;
            _MucLuong = mucLuong;
            _TongLuong = tongLuong;
        }
    }
}
