using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Permissions;
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
        // Dự án
        DataTable HienThiDuAn();
        bool ThemDuAn(DuAn DA);
        bool SuaDuAn(DuAn DA);
        bool XoaDuAn(DuAn DA);
        DataTable TimDuAn(string query, string param, string txtTimKiem);
        DataTable TimKiemDA_Ma(string txtMaDA);
        DataTable TimKiemDA_Ten(string txtTenDA);
        // Tính lương và thống kê lương
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
        public DataTable TimDuAn(string query, string param, string txtTimKiem) {
            SqlConnection conn = DBConnect.ChuoiKetNoi_Hai();
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue($"{param}", txtTimKiem);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
            catch { }
            finally { conn.Close(); }
            return dt;
        }
        public DataTable TimKiemDA_Ma(string txtMaDA)
        {
            string query = "Select * from DuAn where MaDA = @MaDA";
            return TimDuAn(query, "MaDA", txtMaDA);
        }
        public DataTable TimKiemDA_Ten(string txtTenDA)
        {
            string query = "Select * from DuAn where TenDA = @TenDA";
            return TimDuAn(query, "TenDA", txtTenDA);
        }
        // Chấm công cho nhân viên
        public bool ChamCongNV(ChamCong CC)
        {
            List<ChamCong> listCC = Select_CC(CC.NgayLam, CC.ThangLam, CC.NamLam);
            foreach(ChamCong item in listCC)
            {
                if(item.NgayLam == CC.NgayLam && item.ThangLam == CC.ThangLam && item.NamLam == CC.NamLam && item.MaNV == CC.MaNV)
                {
                    return false;
                }
            }
            string query = "INSERT INTO ChamCong VALUES (@NgayLam, @ThangLam, @NamLam, @TrangThai, @GhiChu, @MaNV)";
            SqlConnection conn = DBConnect.ChuoiKetNoi_Hai();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("NgayLam", CC.NgayLam);
                cmd.Parameters.AddWithValue("ThangLam", CC.ThangLam);
                cmd.Parameters.AddWithValue("NamLam", CC.NamLam);
                cmd.Parameters.AddWithValue("TrangThai", CC.TrangThai);
                cmd.Parameters.AddWithValue("GhiChu", CC.GhiChu);
                cmd.Parameters.AddWithValue("MaNV", CC.MaNV);
                if(cmd.ExecuteNonQuery() > 0)
                {
                    return true;
                }
            } catch { }
            finally { conn.Close(); }
            return false;
        }
        // Tạo list Chấm công để so sánh khi chấm công nhân viên
        public List<ChamCong> Select_CC(int NgayLam, int ThangLam, int NamLam)
        {
            List<ChamCong> list = new List<ChamCong>();
            string query = "SELECT NgayLam, ThangLam, NamLam, TrangThai, GhiChu, MaNV FROM ChamCong WHERE NgayLam = @NgayLam AND ThangLam = @ThangLam AND NamLam = @NamLam";
            SqlConnection conn = DBConnect.ChuoiKetNoi_Hai();
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("NgayLam", NgayLam);
            cmd.Parameters.AddWithValue("ThangLam", ThangLam);
            cmd.Parameters.AddWithValue("NamLam", NamLam);
            SqlDataReader read = cmd.ExecuteReader();
            while(read.Read())
            {
                ChamCong temp = new ChamCong();
                temp.NgayLam = read.GetInt32(0);
                temp.ThangLam = read.GetInt32(1);
                temp.NamLam = read.GetInt32(2);
                temp.TrangThai = read.GetString(3);
                temp.GhiChu = read.GetString(4);
                temp.MaNV = read.GetString(5);

                list.Add(temp);
            }
            conn.Close();
            return list;
        }
        // Lấy mã nv đã được chấm công trong ngày
        public List<string> Get_ListMaNV(int day, int month, int year)
        {
            List<string> listMaNV = new List<string>();
            string query = "Select MaNV from ChamCong where NgayLam = @NgayLam and ThangLam = @ThangLam and NamLam = @NamLam";
            SqlConnection conn = DBConnect.ChuoiKetNoi_Hai();
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("NgayLam", day);
            cmd.Parameters.AddWithValue("ThangLam", month);
            cmd.Parameters.AddWithValue("NamLam", year);
            SqlDataReader read = cmd.ExecuteReader();
            while(read.Read())
            {
                string MaNV = read.GetString(0);
                listMaNV.Add(MaNV);
            }
            conn.Close();
            return listMaNV;
        }
        // Hiển thị danh sách nhân viên để chấm công
        public DataTable HienThiNV_ChamCong()
        {
            string query = "SELECT MaNV, HoTen, GioiTinh, ChucVu, ViTriCongViec, MaPB FROM NhanVien";
            SqlConnection conn = DBConnect.ChuoiKetNoi_Hai();
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable("TableNhanVien");
            adapter.Fill(dt);
            conn.Close();
            return dt;
        }

        // Hiển thị chi tiết bảng chấm công
        public DataTable HienThi_CTCC()
        {
            DataTable dt = new DataTable();
            string query = "Select * from ChamCong ORDER BY MaCC DESC";
            SqlConnection conn = DBConnect.ChuoiKetNoi_Hai();
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            conn.Close();
            return dt;
        }
        // Hiển thị chi tiết bảng chấm công theo tháng/năm
        public DataTable HienThi_CTCC_ThangNam(string txtThang, string txtNam)
        {
            string query = "Select * from ChamCong where ThangLam = @ThangLam and NamLam = @NamLam ORDER BY NgayLam ASC";
            DataTable dt = new DataTable();
            SqlConnection conn = DBConnect.ChuoiKetNoi_Hai();
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("ThangLam", txtThang);
            cmd.Parameters.AddWithValue("NamLam", txtNam);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            conn.Close();
            return dt;
        }
        // Thêm vào bảng chi tiết chấm công
        public bool Them_CTCC(ChamCong CC)
        {
            string query = "Insert into ChamCong values (@NgayLam, @ThangLam, @NamLam, @TrangThai, @GhiChu, @MaNV)";
            SqlConnection conn = DBConnect.ChuoiKetNoi_Hai();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("NgayLam", CC.NgayLam);
                cmd.Parameters.AddWithValue("ThangLam", CC.ThangLam);
                cmd.Parameters.AddWithValue("NamLam", CC.NamLam);
                cmd.Parameters.AddWithValue("TrangThai", CC.TrangThai);
                cmd.Parameters.AddWithValue("GhiChu", CC.GhiChu);
                cmd.Parameters.AddWithValue("MaNV", CC.MaNV);
                if(cmd.ExecuteNonQuery() > 0)
                {
                    return true;
                }
            } catch { }
            finally { conn.Close(); }
            return false;
        }
        // Sửa chi tiết chấm công
        public bool Sua_CTCC(ChamCong CC, string txtMaCC)
        {
            string query = "Update ChamCong Set NgayLam = @NgayLam, ThangLam = @ThangLam, NamLam = @NamLam, TrangThai = @TrangThai, GhiChu = @GhiChu, MaNV = @MaNV where MaCC = @MaCC";
            SqlConnection conn = DBConnect.ChuoiKetNoi_Hai();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("MaCC", txtMaCC);
                cmd.Parameters.AddWithValue("NgayLam", CC.NgayLam);
                cmd.Parameters.AddWithValue("ThangLam", CC.ThangLam);
                cmd.Parameters.AddWithValue("NamLam", CC.NamLam);
                cmd.Parameters.AddWithValue("TrangThai", CC.TrangThai);
                cmd.Parameters.AddWithValue("GhiChu", CC.GhiChu);
                cmd.Parameters.AddWithValue("MaNV", CC.MaNV);
                if (cmd.ExecuteNonQuery() > 0)
                {
                    return true;
                }
            }
            catch { }
            finally { conn.Close(); }
            return false;
        }
        // Xóa chi tiết chấm công
        public bool Xoa_CTCC(string txtMaCC)
        {
            string query = "Delete ChamCong where MaCC = @MaCC";
            SqlConnection conn = DBConnect.ChuoiKetNoi_Hai();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("MaCC", txtMaCC);
                if (cmd.ExecuteNonQuery() > 0)
                {
                    return true;
                }
            }
            catch { }
            finally { conn.Close(); }
            return false;
        }
        
        // Tính và lấy tổng ngày công
        public int Get_TongCong(string MaNV, int ThangLam, int NamLam)
        {
            string query = "SELECT COUNT(MaNV) FROM ChamCong WHERE MaNV = @MaNV AND TrangThai = N'Làm' AND ThangLam = @ThangLam AND NamLam = @NamLam";
            SqlConnection conn = DBConnect.ChuoiKetNoi_Hai();
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("MaNV", MaNV);
            cmd.Parameters.AddWithValue("ThangLam", ThangLam);
            cmd.Parameters.AddWithValue("NamLam", NamLam);

            Int32 TongCong = (Int32) cmd.ExecuteScalar();
            conn.Close();
            return TongCong;
        }
        // Hiển thị bảng tính lương
        public List<LuongNV> HienThi_Luong(int ThangLam, int NamLam)
        {
            List<LuongNV> ListLuongNV = new List<LuongNV>();

            string query = "SELECT MaNV, HoTen, ChucVu FROM NhanVien";
            SqlConnection conn = DBConnect.ChuoiKetNoi_Hai();
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader read = cmd.ExecuteReader();
            while(read.Read()) { 
                LuongNV temp =new LuongNV();
                temp.MaNV = read.GetString(0);
                temp.TenNV = read.GetString(1);
                temp.ChucVu = read.GetString(2);
                temp.NgayCong = Get_TongCong(temp.MaNV, ThangLam, NamLam);
                double MucLuong;
                if (temp.ChucVu.ToUpper() == "NHÂN VIÊN")
                {
                    MucLuong = 500000;
                } else
                {
                    MucLuong = 700000;
                } 
                temp.MucLuong = MucLuong;
                temp.TongLuong = temp.NgayCong * MucLuong;

                ListLuongNV.Add(temp);
            }
            conn.Close();
            return ListLuongNV;
        }
        public void ThongKeLuongNV() {
        
        }
    }
}
