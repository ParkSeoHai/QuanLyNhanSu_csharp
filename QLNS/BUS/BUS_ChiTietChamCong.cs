using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BUS
{
    public class BUS_ChiTietChamCong
    {
        QuanLy quanLy = new QuanLy();
        // Hiển thị chi tiết bảng chấm công
        public DataTable HienThi_CTCC()
        {
            return quanLy.HienThi_CTCC();
        }
        // Hiển thị chi tiết bảng chấm công theo tháng/năm
        public DataTable HienThi_CTCC_ThangNam(string txtThang, string txtNam)
        {
            return quanLy.HienThi_CTCC_ThangNam(txtThang, txtNam);
        }
        // Thêm bảng chi tiết chấm công
        public bool Them_CCTC(ChamCong CC)
        {
            if (quanLy.Them_CTCC(CC)) { return true; }
            return false;
        }
        // Sửa bảng chi tiết chấm công
        public bool Sua_CTCC(ChamCong CC, string txtMaCC)
        {
            if (quanLy.Sua_CTCC(CC, txtMaCC)) { return true; }
            return false;
        }
        // Xóa
        public bool Xoa_CTCC(string txtMaCC) {
            if(quanLy.Xoa_CTCC(txtMaCC)) { return true; }
            return false;
        }
    }
}
