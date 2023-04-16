using BUS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class FormTrang_Chu : Form
    {
        private string maQl = ""; 
        public FormTrang_Chu()
        {
            InitializeComponent();
        }
        public FormTrang_Chu(string maQl)
        {
            InitializeComponent();
            this.maQl = maQl;
        }
        private void btnQuanLyNhanVien_Click(object sender, EventArgs e)
        {
            FormQuanLyNhanVien quanLyNhanVien = new FormQuanLyNhanVien(maQl);
            this.Hide();
            quanLyNhanVien.ShowDialog();
        }

        private void btnQuanLyPhongBan_Click(object sender, EventArgs e)
        {
            FormQuanLyPhongBan quanLyPhongBan = new FormQuanLyPhongBan(maQl);
            this.Hide();
            quanLyPhongBan.ShowDialog();
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn Có Chắc Chắn Muốn Thoát không?", "Thống Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                foreach (Form form in Application.OpenForms)
                {
                    // Nếu Form đang hiển thị, đóng Form
                    if (form.Visible)
                    {
                        form.Close();
                    }
                    // Nếu Form đã bị ẩn, giải phóng tài nguyên
                    else
                    {
                        form.Dispose();
                    }
                }
            }
        }

        private void btnQuanLyDuAn_Click(object sender, EventArgs e)
        {
            frmQuanLyDuAn fromDA = new frmQuanLyDuAn(maQl);
            this.Hide();
            fromDA.ShowDialog();
        }

        private void btnQuanLyLuong_Click(object sender, EventArgs e)
        {
            frmChamCongNV frmChamCong = new frmChamCongNV(maQl);
            this.Hide();
            frmChamCong.ShowDialog();
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            frmTinhLuong fromTinhLuong = new frmTinhLuong(maQl);
            this.Hide();
            fromTinhLuong.ShowDialog();
        }

        private void FormTrang_Chu_Load(object sender, EventArgs e)
        {

        }
    }
}
