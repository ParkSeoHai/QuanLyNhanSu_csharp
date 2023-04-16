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
        DBConnect connect = new DBConnect();
        public string getMaSQL()
        {
            SqlCommand sqlCMD = new SqlCommand();
            sqlCMD.CommandType = CommandType.Text;
            sqlCMD.CommandText = $"select MaQL from TaiKhoanDangNhap where TenTK = '{txtTaiKhoan.Text}'";
            sqlCMD.Connection = connect.chuoiKetNoi_Mot();
            SqlDataReader reader = sqlCMD.ExecuteReader();
            if (reader.Read())
            {
                return reader.GetString(0);
            }else
            {
                return "";
            }
        }
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            try
            {
                Bus_Dang_Nhap dangNhap = new Bus_Dang_Nhap();
                if (dangNhap.bus_dangNhap(txtTaiKhoan.Text.Trim(), txtMatKhau.Text.Trim()))
                {
                    FormTrang_Chu trangCHu = new FormTrang_Chu(getMaSQL());
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
