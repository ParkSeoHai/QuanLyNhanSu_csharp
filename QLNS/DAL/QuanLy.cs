using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Permissions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DAL
{
    interface IQuanLy
    {
        // Dự án
        bool DangNhap(string tk, string mk);
        string quenMatKhau(string s);
        bool ThemNhanVien(NhanVien nhanVien);
        bool SuaNhanVien(NhanVien nhanVien);
        bool XoaNhanVien(string maNV);
        SqlDataReader TimKiemNhanVien(string daTa_tim);
        bool ThemPhongBan(PhongBan phongBan);
        bool SuaPhongBan(PhongBan phongBan);
        bool XoaPhongBan(string maPB);
        SqlDataReader TimPhongBan(string tenPB);
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
        private List<string> tenTK = new List<string>();
        private List<string> matKhau = new List<string>();
        private List<string> maNV = new List<string>();
        private List<string> maPB = new List<string>();
        public string MaQL { get => _MaQL; set => _MaQL = value; }
        public QuanLy() { }
        public QuanLy(string MaQL, string HoTen, string GioiTinh, string NgaySinh, string DiaChi, string SDT, string Email) : base(HoTen, GioiTinh, NgaySinh, DiaChi, SDT, Email)
        {
            _MaQL = MaQL;
        }
        DBConnect DBConnect = new DBConnect();
        public void getTK ()
        {
            SqlCommand sqlCMD = new SqlCommand();
            sqlCMD.CommandType = CommandType.Text;
            sqlCMD.CommandText = "select TenTK, MatKhau from TaiKhoanDangNhap";
            sqlCMD.Connection = DBConnect.chuoiKetNoi_Mot();
            SqlDataReader reader = sqlCMD.ExecuteReader();
            while(reader.Read())
            {
                tenTK.Add(reader.GetString(0));
                matKhau.Add(reader.GetString(1));
            }
            reader.Close();
        }
        public bool DangNhap(string tenNguoiDung, string matKhauNguoiDung) {
            getTK();
            int cnt = 0;
            foreach (string s in tenTK)
            {
                if (tenNguoiDung.Trim() == s.Trim())
                {
                    cnt++;
                    break;
                }
            }
            foreach(string s in matKhau)
            {
                if (matKhauNguoiDung.Trim() == s.Trim())
                {
                    cnt++;
                    break;
                }
            }
            if (cnt == 2)
            {
                return true;
            }else
            {
                return false;
            }
        }
        public string quenMatKhau(string tenTK)
        {
            SqlCommand sqlCMD = new SqlCommand();
            sqlCMD.CommandType = CommandType.Text;
            sqlCMD.CommandText = $"select MatKhau from TaiKhoanDangNhap where TenTK = N'{tenTK}'";
            sqlCMD.Connection = DBConnect.chuoiKetNoi_Mot();
            SqlDataReader reader = sqlCMD.ExecuteReader();
            if (reader.Read())
            {
                return reader.GetString(0);
            }else
            {
                return "err";
            }
        }
        public List<string> layMaNV()
        {
            SqlCommand sqlCMD = new SqlCommand();
            sqlCMD.CommandType = CommandType.Text;
            sqlCMD.CommandText = "select MaNV from NhanVien";
            sqlCMD.Connection = DBConnect.chuoiKetNoi_Mot();
            SqlDataReader reader = sqlCMD.ExecuteReader();
            while(reader.Read())
            {
                maNV.Add(reader.GetString(0)[reader.GetString(0).Length - 1].ToString());
            }
            reader.Close();
            return maNV;
        }
        public List<string> layMaPB()
        {
            SqlCommand sqlCMD = new SqlCommand();
            sqlCMD.CommandType = CommandType.Text;
            sqlCMD.CommandText = "select MaPB from PhongBan";
            sqlCMD.Connection = DBConnect.chuoiKetNoi_Mot();
            SqlDataReader reader = sqlCMD.ExecuteReader();
            while (reader.Read())
            {
                maPB.Add(reader.GetString(0)[reader.GetString(0).Length - 1].ToString());
            }
            reader.Close();
            return maPB;
        }
        public bool ThemNhanVien(NhanVien nhanVien) {
            layMaNV();
            Random random = new Random();
            back:
            string x = random.Next(1000).ToString();
            foreach (string j in maNV)
            {
                if (j == x)
                {
                    goto back;   
                }
            }
            SqlCommand sqlCMD = new SqlCommand();
            sqlCMD.CommandType = CommandType.Text;
            sqlCMD.CommandText = $"insert into NhanVien values ('NV00{x}',N'{nhanVien.HoTen}', N'{nhanVien.GioiTinh}', '{nhanVien.NgaySinh}', N'{nhanVien.DiaChi}', '{nhanVien.SDT}', N'{nhanVien.Email}', N'{nhanVien.ChucVu}', N'{nhanVien.ViTriCongViec}', '{nhanVien.MaPB}')";
            sqlCMD.Connection = DBConnect.chuoiKetNoi_Mot();
            if (sqlCMD.ExecuteNonQuery() > 0)
            {
                return true;
            }else
            {
                return false;
            }
        }
        public bool SuaNhanVien(NhanVien nhanVien) {
            SqlCommand sqlCMD = new SqlCommand();
            sqlCMD.CommandType = CommandType.Text;
            sqlCMD.CommandText = $"update NhanVien set MaNV = '{nhanVien.MaNV}', HoTen = N'{nhanVien.HoTen}', GioiTinh = N'{nhanVien.GioiTinh}', NgaySinh = '{nhanVien.NgaySinh}', DiaChi = N'{nhanVien.DiaChi}', SDT = '{nhanVien.SDT}', Email = '{nhanVien.Email}', ChucVu = N'{nhanVien.ChucVu}', ViTriCongViec = N'{nhanVien.ViTriCongViec}', MaPB = '{nhanVien.MaPB}' where MaNV ='{nhanVien.MaNV}'";
            sqlCMD.Connection = DBConnect.chuoiKetNoi_Mot();
            if (sqlCMD.ExecuteNonQuery() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool XoaNhanVien(string maNV) {
            SqlCommand sqlCMD = new SqlCommand();
            sqlCMD.CommandType = CommandType.Text;
            sqlCMD.CommandText = $"delete from NhanVien where MaNV = '{maNV}'";
            sqlCMD.Connection = DBConnect.chuoiKetNoi_Mot();
            if (sqlCMD.ExecuteNonQuery() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public SqlDataReader TimKiemNhanVien(string daTa) {
            SqlCommand sqlCMD = new SqlCommand();
            sqlCMD.CommandType = CommandType.Text;
            sqlCMD.CommandText = $"select * from NhanVien where HoTen like '%{daTa}%' or GioiTinh like '%{daTa}%' or ViTriCongViec like '%{daTa}%'";
            sqlCMD.Connection = DBConnect.chuoiKetNoi_Mot();
            SqlDataReader reader = sqlCMD.ExecuteReader();
           if (reader.HasRows)
            {
                return reader;
            }else
            {
                return null;
            }
        }
        public bool ThemPhongBan(PhongBan phongBan) {
            layMaPB();
            Random random = new Random();
            back:
            string x = random.Next(1000).ToString();
            foreach (string j in maPB)
            {
                if (j == x)
                {
                    goto back;
                }
            }
            SqlCommand sqlCMD = new SqlCommand();
            sqlCMD.CommandType = CommandType.Text;
            sqlCMD.CommandText = $"insert into PhongBan values ('PB00{x}', N'{phongBan.TenPB}', '{phongBan.NgayLap}', '{phongBan.SDT}', '{phongBan.MaQL}')";
            sqlCMD.Connection = DBConnect.chuoiKetNoi_Mot();
            if (sqlCMD.ExecuteNonQuery () > 0)
            {
                return true;
            }else
            {
                return false;
            }
        }
        public bool SuaPhongBan(PhongBan phongBan) {
            SqlCommand sqlCMD = new SqlCommand();
            sqlCMD.CommandType = CommandType.Text;
            sqlCMD.CommandText = $"update PhongBan set MaPB = '{phongBan.MaPB}', TenPB = N'{phongBan.TenPB}', NgayLap = '{phongBan.NgayLap}', SDT = '{phongBan.SDT}', MaQL = '{phongBan.MaQL}' where MaPB = '{phongBan.MaPB}'";
            sqlCMD.Connection = DBConnect.chuoiKetNoi_Mot();
            if (sqlCMD.ExecuteNonQuery() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool XoaPhongBan(string maPhongBan) {
            SqlCommand sqlCMD = new SqlCommand();
            sqlCMD.CommandType = CommandType.Text;
            sqlCMD.CommandText = $"delete from PhongBan where MaPB = '{maPhongBan}'";
            sqlCMD.Connection = DBConnect.chuoiKetNoi_Mot();
            if (sqlCMD.ExecuteNonQuery() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public SqlDataReader TimPhongBan(string data) {
            SqlCommand sqlCMD = new SqlCommand();
            sqlCMD.CommandType = CommandType.Text;
            sqlCMD.CommandText = $"select * from PhongBan where TenPB like '%{data}%' or SDT like'%{data}%'";
            sqlCMD.Connection = DBConnect.chuoiKetNoi_Mot();
            SqlDataReader reader = sqlCMD.ExecuteReader();
            if (reader.HasRows)
            {
                return reader;
            }
            else
            {
                return null;
            }
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
