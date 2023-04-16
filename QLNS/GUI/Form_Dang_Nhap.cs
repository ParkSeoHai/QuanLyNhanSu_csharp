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
    public partial class Form_Dang_Nhap : Form
    {
        public Form_Dang_Nhap()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormQuenMatKhau formQuenMK = new FormQuenMatKhau();
            this.Hide();
            formQuenMK.ShowDialog();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            try
            {
                Bus_Dang_Nhap dangNhap = new Bus_Dang_Nhap();
                if (dangNhap.bus_dangNhap(txtTaiKhoan.Text.Trim(), txtMatKhau.Text.Trim()))
                {
                    FormTrang_Chu trangCHu = new FormTrang_Chu();
                    MessageBox.Show("Đăng Nhập Thành Công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    trangCHu.ShowDialog(); 
                }
                else
                {
                    MessageBox.Show("Đăng Nhập Không Thành Thành Công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            {
                MessageBox.Show("Không Thành Công");
            }
        }
    }
}
