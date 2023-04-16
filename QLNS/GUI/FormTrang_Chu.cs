using BUS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class FormTrang_Chu : Form
    {
        public FormTrang_Chu()
        {
            InitializeComponent();
        }

        private void btnQuanLyNhanVien_Click(object sender, EventArgs e)
        {
            FormQuanLyNhanVien quanLyNhanVien = new FormQuanLyNhanVien();
            this.Hide();
            quanLyNhanVien.ShowDialog();
        }

        private void btnQuanLyPhongBan_Click(object sender, EventArgs e)
        {
            FormQuanLyPhongBan quanLyPhongBan = new FormQuanLyPhongBan();
            this.Hide();
            quanLyPhongBan.ShowDialog();
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn Có Chắc Chắn Muốn Thoát không?", "Thống Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
