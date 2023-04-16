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
    public partial class FormQuenMatKhau : Form
    {
        public FormQuenMatKhau()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Form_Dang_Nhap form_Dang_Nhap = new Form_Dang_Nhap();
            this.Hide();
            form_Dang_Nhap.ShowDialog();
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            bus_QuenMK bus_Quen_Mat_Khau = new bus_QuenMK();
            if (bus_Quen_Mat_Khau.bus_Quen_Mat_Khau(txtTenTKQUEN.Text.Trim()) != "err")
            {
                txtHienThiLaiMatKhau.Text = bus_Quen_Mat_Khau.bus_Quen_Mat_Khau(txtTenTKQUEN.Text);
            }else
            {
                txtHienThiLaiMatKhau.Text = "";
            }
        }
    }
}
