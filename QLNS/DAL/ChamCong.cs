using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ChamCong
    {
        private string _MaNV;
        private string _MaDA;
        private double _SoNgayLam;

        public string MaNV { get => _MaNV; set => _MaNV = value; }
        public string MaDA { get => _MaDA; set => _MaDA = value; }
        public double SoNgayLam { get => _SoNgayLam; set => _SoNgayLam = value; }

        public ChamCong() { }
        public ChamCong(string maNV, string maDA, double soNgayLam)
        {
            _MaNV = maNV;
            _MaDA = maDA;
            _SoNgayLam = soNgayLam;
        }
    }
}
