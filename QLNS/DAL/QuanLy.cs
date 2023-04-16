using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DAL
{
    interface IQuanLy
    {
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
        void TimDuAn();
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
        public void TimDuAn() {
        
        }
        public void ThongKeLuongNV() {
        
        }
    }
}
