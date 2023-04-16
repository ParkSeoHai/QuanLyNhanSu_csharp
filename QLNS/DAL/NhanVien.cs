using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class NhanVien : Person
    {
        private string _MaNV;
        private string _ChucVu;
        private string _ViTriCongViec;
        private string _MaPB;
        public string MaNV { get => _MaNV; set => _MaNV = value; }
        public string ChucVu { get => _ChucVu; set => _ChucVu = value; }
        public string ViTriCongViec { get => _ViTriCongViec; set => _ViTriCongViec = value; }
        public string MaPB { get => _MaPB; set => _MaPB = value; }
        public NhanVien()
        {

        }
        public NhanVien(string maNV, string HoTen, string GioiTinh, string NgaySinh, string DiaChi, string SDT, string Email, string ChucVu, string ViTri, string MaPB) : base(HoTen, GioiTinh, NgaySinh, DiaChi, SDT, Email)
        {
            _MaNV = maNV;
            _ChucVu = ChucVu;
            _ViTriCongViec = ViTri;
            _MaPB = MaPB;
        }
    }
}
