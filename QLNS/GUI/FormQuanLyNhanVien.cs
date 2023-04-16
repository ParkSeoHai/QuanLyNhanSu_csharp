using BUS;
using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class FormQuanLyNhanVien : Form
    {
        public FormQuanLyNhanVien()
        {
            InitializeComponent();
        }
        DBConnect CONNECT = new DBConnect();
        public void HienThi()
        {
            SqlCommand sqlCMD = new SqlCommand();   
            sqlCMD.CommandType = CommandType.Text;
            sqlCMD.CommandText = "select * from NhanVien";
            sqlCMD.Connection = CONNECT.chuoiKetNoi_Mot();
            SqlDataReader reader = sqlCMD.ExecuteReader();
            lsvHienThi.Items.Clear();
            while(reader.Read())
            {
                ListViewItem item = new ListViewItem(reader.GetString(0));
                item.SubItems.Add(reader.GetString(1));
                item.SubItems.Add(reader.GetString(2));
                item.SubItems.Add(reader.GetString(3));
                item.SubItems.Add(reader.GetString(4));
                item.SubItems.Add(reader.GetString(5));
                item.SubItems.Add(reader.GetString(6));
                item.SubItems.Add(reader.GetString(7));
                item.SubItems.Add(reader.GetString(8));
                item.SubItems.Add(reader.GetString(9));
                lsvHienThi.Items.Add(item);
            }
            reader.Close();
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            NhanVien nhanVien = new NhanVien("", txtTenNhanVien.Text, txtGioiTinh.Text, dateNgaySinh.Value.ToString("dd/MM/yyyy"), txtDiaChi.Text, txtSoDienThoai.Text, txtEmail.Text, txtChucVu.Text, txtViTriCongViec.Text, txtMaPhong.Text);
            Quan_Ly_Nhan_Vien quanLy = new Quan_Ly_Nhan_Vien();
            if (quanLy.themNhanVien(nhanVien))
            {
                HienThi();
                MessageBox.Show("Thêm Thành Công");
            }else
            {
                MessageBox.Show("Thêm Thất Bại");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            NhanVien nhanVien = new NhanVien($"{MaNV}", txtTenNhanVien.Text, txtGioiTinh.Text, dateNgaySinh.Value.ToString("dd/MM/yyyy"), txtDiaChi.Text, txtSoDienThoai.Text, txtEmail.Text, txtChucVu.Text, txtViTriCongViec.Text, txtMaPhong.Text);
            Quan_Ly_Nhan_Vien quanLy = new Quan_Ly_Nhan_Vien();
            if (quanLy.suaNhanVien(nhanVien))
            {
                HienThi();
                MessageBox.Show("Sửa Thành Công");
            }else
            {
                MessageBox.Show("Sửa Không Thành Công");
            }
        }
        string MaNV = "";
        private void lsvHienThi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsvHienThi.SelectedItems.Count == 0) return;
            ListViewItem item = lsvHienThi.SelectedItems[0];
            MaNV = item.SubItems[0].Text;
            txtTenNhanVien.Text = item.SubItems[1].Text;
            txtGioiTinh.Text = item.SubItems[2].Text;
            txtDiaChi.Text = item.SubItems[4].Text;
            txtSoDienThoai.Text = item.SubItems[5].Text;
            txtEmail.Text = item.SubItems[6].Text;
            txtChucVu.Text = item.SubItems[7].Text;
            txtViTriCongViec.Text = item.SubItems[8].Text;
            txtMaPhong.Text = item.SubItems[9].Text;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            Quan_Ly_Nhan_Vien quanLy = new Quan_Ly_Nhan_Vien();
            if (quanLy.xoaNhanVien(MaNV))
            {
                HienThi();
                MessageBox.Show("Xóa Thành Công");
            }else
            {
                MessageBox.Show("Xóa Không Thành Công, vui lòng chọn dữ liệu trước khi xóa");
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            Quan_Ly_Nhan_Vien quanLy = new Quan_Ly_Nhan_Vien();
            if (quanLy.timNhanVien(txtTiemKiem.Text) != null)
            {
                SqlDataReader data = quanLy.timNhanVien(txtTiemKiem.Text);
                lsvHienThi.Items.Clear();
                while(data.Read())
                {
                    ListViewItem item = new ListViewItem(data.GetString(0));
                    item.SubItems.Add(data.GetString(1));
                    item.SubItems.Add(data.GetString(2));
                    item.SubItems.Add(data.GetString(3));
                    item.SubItems.Add(data.GetString(4));
                    item.SubItems.Add(data.GetString(5));
                    item.SubItems.Add(data.GetString(6));
                    item.SubItems.Add(data.GetString(7));
                    item.SubItems.Add(data.GetString(8));
                    item.SubItems.Add(data.GetString(9));
                    lsvHienThi.Items.Add(item);
                }
                data.Close();
            }else
            {
                MessageBox.Show("Không Có Dữ Liệu Đó");
            }
        }

        private void FormQuanLyNhanVien_Load(object sender, EventArgs e)
        {
            HienThi();
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            FormTrang_Chu trangChu = new FormTrang_Chu();
            this.Hide();
            trangChu.ShowDialog();
        }
    }
}
