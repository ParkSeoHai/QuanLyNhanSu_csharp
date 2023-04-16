using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ChamCong
    {
        private int _MaCC;
        private int _NgayLam;
        private int _ThangLam;
        private int _NamLam;
        private string _TrangThai;
        private string _GhiChu;
        private string _MaNV;

        public int MaCC { get => _MaCC; set => _MaCC = value; }
        public int NgayLam { get => _NgayLam; set => _NgayLam = value; }
        public int ThangLam { get => _ThangLam; set => _ThangLam = value; }
        public int NamLam { get => _NamLam; set => _NamLam = value; }
        public string TrangThai { get => _TrangThai; set => _TrangThai = value; }
        public string GhiChu { get => _GhiChu; set => _GhiChu = value; }
        public string MaNV { get => _MaNV; set => _MaNV = value; }

        public ChamCong() { }

        public ChamCong(int ngayLam, int thangLam, int namLam, string trangThai, string ghiChu, string maNV)
        {
            _NgayLam = ngayLam;
            _ThangLam = thangLam;
            _NamLam = namLam;
            _TrangThai = trangThai;
            _GhiChu = ghiChu;
            _MaNV = maNV;
        }
    }
}
