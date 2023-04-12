using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DAL
{
    interface IQuanLy
    {
        void DangNhap();
        void ThemNhanVien();
        void SuaNhanVien();
        void XoaNhanVien();
        void TimKiemNhanVien();
        void ThemPhongBan();
        void SuaPhongBan();
        void XoaPhongBan();
        void TimPhongBan();

        DataTable HienThiDuAn();
        bool ThemDuAn(DuAn DA);
        bool SuaDuAn(DuAn DA);
        bool XoaDuAn(DuAn DA);
        void TimDuAn();
        void ThongKeLuongNV();
    }
    public class QuanLy: Person, IQuanLy
    {
        private string _MaQL;

        public string MaQL { get => _MaQL; set => _MaQL = value; }
        public QuanLy() { }
        public QuanLy(string MaQL, string HoTen, string GioiTinh, string NgaySinh, string DiaChi, string SDT, string Email) : base(HoTen, GioiTinh, NgaySinh, DiaChi, SDT, Email)
        {
            _MaQL = MaQL;
        }

        DBConnect DBConnect = new DBConnect();

        public void DangNhap() { 
        
        }
        public void ThemNhanVien() {
        
        }
        public void SuaNhanVien() {
        
        }
        public void XoaNhanVien() {
        
        }
        public void TimKiemNhanVien() {
        
        }
        public void ThemPhongBan() {
        
        }
        public void SuaPhongBan() {
        
        }
        public void XoaPhongBan() {
        
        }
        public void TimPhongBan() {
        
        }
        // Dự Án
        public DataTable HienThiDuAn() {
            string query = "Select * from DuAn";
            SqlConnection conn = DBConnect.ChuoiKetNoi_Hai();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }
        public bool QuanLyDuAn(DuAn DA, string query)
        {
            SqlConnection conn = DBConnect.ChuoiKetNoi_Hai();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("MaDA", DA.MaDA);
                cmd.Parameters.AddWithValue("TenDA", DA.TenDA);
                cmd.Parameters.AddWithValue("SoNV", DA.SoNV);
                cmd.Parameters.AddWithValue("MoTaDA", DA.MoTaDA);
                cmd.Parameters.AddWithValue("MaPB", DA.MaPB);
                if (cmd.ExecuteNonQuery() > 0)
                {
                    return true;
                }
            }
            catch { }
            finally { conn.Close(); }
            return false;
        }
        public bool ThemDuAn(DuAn DA) {
            string query = "Insert into DuAn values (@MaDA, @TenDA, @SoNV, @MoTaDA, @MaPB)";
            if(QuanLyDuAn(DA, query))
            {
                return true;
            }
            return false;
        }
        public bool SuaDuAn(DuAn DA) {
            string query = "Update DuAn set TenDA = @TenDA, SoNV = @SoNV, MoTaDA = @MoTaDA, MaPB = @MaPB where MaDA = @MaDA";
            if(QuanLyDuAn(DA, query)) { return true; }
            return false;
        }
        public bool XoaDuAn(DuAn DA) {
            string query = "Delete DuAn where MaDA = @MaDA";
            if(QuanLyDuAn(DA, query)) { return true; }
            return false;
        }
        public void TimDuAn() {
        
        }
        public void ThongKeLuongNV() {
        
        }
    }
}
