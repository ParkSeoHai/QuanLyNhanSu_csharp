using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class TaiKhoanDangNhap
    {
        private string _TenTK;
        private string _MatKhau;
        private string _MaQL;

        public string TenTK { get => _TenTK; set => _TenTK = value; }
        public string MatKhau { get => _MatKhau; set => _MatKhau = value; }
        public string MaQL { get => _MaQL; set => _MaQL = value; }

        public TaiKhoanDangNhap() { }
        public TaiKhoanDangNhap(string tenTK, string mk, string maQL)
        {
            _TenTK = tenTK;
            _MatKhau = mk;
            _MaQL = maQL;
        }
    }
}
