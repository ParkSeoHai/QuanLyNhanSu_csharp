using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class PhongBan
    {
        private string _MaPB;
        private string _TenPB;
        private string _NgayLap;
        private string _SDT;
        private string _MaQL;

        public string MaPB { get => _MaPB; set => _MaPB = value; }
        public string TenPB { get => _TenPB; set => _TenPB = value; }
        public string NgayLap { get => _NgayLap; set => _NgayLap = value; }
        public string SDT { get => _SDT; set => _SDT = value; }
        public string MaQL { get => _MaQL; set => _MaQL = value; }

        public PhongBan() { }

        public PhongBan(string maPB, string tenPB, string ngayLap, string sdt, string maQL)
        {
            _MaPB = maPB;
            _TenPB = tenPB;
            _NgayLap = ngayLap;
            _SDT = sdt;
            _MaQL = maQL;
        }
    }
}
