using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DuAn
    {
        private string _MaDA;
        private string _TenDA;
        private int _SoNV;
        private string _MoTaDA;
        private string _MaPB;

        public string MaDA { get => _MaDA; set => _MaDA = value; }
        public string TenDA { get => _TenDA; set => _TenDA = value; }
        public int SoNV { get => _SoNV; set => _SoNV = value; }
        public string MoTaDA { get => _MoTaDA; set => _MoTaDA = value; }
        public string MaPB { get => _MaPB; set => _MaPB = value; }

        public DuAn() { }
        public DuAn(string maDA, string tenDA, int soNV, string moTaDA, string maPB)
        {
            _MaDA = maDA;
            _TenDA = tenDA;
            _SoNV = soNV;
            _MoTaDA = moTaDA;
            _MaPB = maPB;
        }
    }
}
