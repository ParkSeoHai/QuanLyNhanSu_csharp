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
    public partial class FormQuanLyPhongBan : Form
    {
        public FormQuanLyPhongBan()
        {
            InitializeComponent();
        }
        DBConnect connect = new DBConnect();
        public void hienThi()
        {
            SqlCommand sqlCMD = new SqlCommand();
            sqlCMD.CommandType = CommandType.Text;
            sqlCMD.CommandText = "select * from PhongBan";
            sqlCMD.Connection = connect.chuoiKetNoi_Mot();
            SqlDataReader reader = sqlCMD.ExecuteReader();
            lsvHienThi.Items.Clear();
            while(reader.Read()) {
                ListViewItem item = new ListViewItem(reader.GetString(0));
                item.SubItems.Add(reader.GetString(1));
                item.SubItems.Add(reader.GetString(2));
                item.SubItems.Add(reader.GetString(3));
                item.SubItems.Add(reader.GetString(4));
                lsvHienThi.Items.Add(item);
            }
            reader.Close();
        }
        private void FormQuanLyPhongBan_Load(object sender, EventArgs e)
        {
            hienThi();
        }
        string maPB = "";
        private void lsvHienThi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsvHienThi.SelectedItems.Count == 0) return;
            ListViewItem item = lsvHienThi.SelectedItems[0];
            maPB = item.SubItems[0].Text;
            txtTenPhongBan.Text = item.SubItems[1].Text;
            txtSoDienThoai.Text = item.SubItems[3].Text;
            txtMaQuanLy.Text = item.SubItems[4].Text;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            PhongBan phongBan = new PhongBan("", txtTenPhongBan.Text, dateNgayLap.Value.ToString("dd/MM/yyyy"), txtSoDienThoai.Text, txtMaQuanLy.Text);
            QuanLyPhongBan quanLy = new QuanLyPhongBan();
            if (quanLy.themPhongBan(phongBan))
            {
                hienThi();
                MessageBox.Show("Thêm Phòng Ban Thành Công");
            }else
            {
                MessageBox.Show("Thêm Phòng Ban Không Thành Công");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            PhongBan phongBan = new PhongBan($"{maPB}", txtTenPhongBan.Text, dateNgayLap.Value.ToString("dd/MM/yyyy"), txtSoDienThoai.Text, txtMaQuanLy.Text);
            QuanLyPhongBan quanLy = new QuanLyPhongBan();
            if (quanLy.suaPhongBan(phongBan))
            {
                hienThi();
                MessageBox.Show("Sửa Thành Công");
            }else
            {
                MessageBox.Show("Sửa Không Thành Công");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            QuanLyPhongBan quanLy = new QuanLyPhongBan();
            if (quanLy.xoaPhongBan(maPB))
            {
                hienThi();
                MessageBox.Show("Xóa Thành Công");
            }else
            {
                MessageBox.Show("Vui lòng Chọn Một Phòng Ban Để Xóa");
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            QuanLyPhongBan quanLy = new QuanLyPhongBan();
            if (quanLy.timPhongBan(txtTiemKiem.Text.Trim()) != null)
            {
                lsvHienThi.Items.Clear();
                SqlDataReader reader = quanLy.timPhongBan(txtTiemKiem.Text);
                while (reader.Read())
                {
                    ListViewItem item = new ListViewItem(reader.GetString(0));
                    item.SubItems.Add(reader.GetString(1));
                    item.SubItems.Add(reader.GetString(2));
                    item.SubItems.Add(reader.GetString(3));
                    item.SubItems.Add(reader.GetString(4));
                    lsvHienThi.Items.Add(item);
                }
                reader.Close();
            }else
            {
                MessageBox.Show("Không Tìm Thấy");
            }
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            FormTrang_Chu trangChu = new FormTrang_Chu();
            this.Hide();
            trangChu.ShowDialog();
        }
    }
}
