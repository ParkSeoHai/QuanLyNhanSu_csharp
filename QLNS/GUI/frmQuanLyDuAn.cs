using BUS;
using DAL;
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
    public partial class frmQuanLyDuAn : Form
    {
        BUS_QLDA BUS_QLDA = new BUS_QLDA();
        public frmQuanLyDuAn()
        {
            InitializeComponent();
        }

        private void frmQuanLyDuAn_Load(object sender, EventArgs e)
        {
            Load_dtGridQLDA(BUS_QLDA.HienThiDuLieu());
        }

        // Hàm load datagridview
        private void Load_dtGridQLDA(DataTable dt)
        {
            dtGridQLDA.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtGridQLDA.DataSource = dt;
        }
        // Sự kiện cell click
        int index;
        private void dtGridQLDA_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            index = e.RowIndex;
            txtMaDA.Text = dtGridQLDA.Rows[index].Cells[0].Value.ToString();
            txtTenDA.Text = dtGridQLDA.Rows[index].Cells[1].Value.ToString();
            txtSoNV.Text = dtGridQLDA.Rows[index].Cells[2].Value.ToString();
            txtMoTaDA.Text = dtGridQLDA.Rows[index].Cells[3].Value.ToString();
            txtMaPB.Text = dtGridQLDA.Rows[index].Cells[4].Value.ToString();
        }

        // Hàm check textbox
        private bool Check_TextBox()
        {
            if (string.IsNullOrWhiteSpace(txtMaDA.Text))
            {
                MessageBox.Show("Nhập mã dự án", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaDA.Focus();
                return false;
            }
            else if (string.IsNullOrWhiteSpace(txtTenDA.Text))
            {
                MessageBox.Show("Nhập tên dự án", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenDA.Focus();
                return false;
            }
            else if (string.IsNullOrWhiteSpace(txtSoNV.Text))
            {
                MessageBox.Show("Nhập số nhân viên tham gia dự án", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSoNV.Focus();
                return false;
            }
            else if (string.IsNullOrWhiteSpace(txtMoTaDA.Text))
            {
                MessageBox.Show("Nhập mô tả dự án", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMoTaDA.Focus();
                return false;
            }
            else if (string.IsNullOrWhiteSpace(txtMaPB.Text))
            {
                MessageBox.Show("Nhập mã phòng ban", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaPB.Focus();
                return false;
            } else
            {
                return true;
            }
        }
        // Hàm clear textbox
        private void Clear_TextBox()
        {
            txtMaDA.Clear();
            txtTenDA.Clear();
            txtSoNV.Clear();
            txtMoTaDA.Clear();
            txtMaPB.Clear();
        }
        // Hàm tạo đối tượng DuAn
        private DuAn Create_DuAn()
        {
            DuAn DA = new DuAn(txtMaDA.Text, txtTenDA.Text, Convert.ToInt32(txtSoNV.Text), txtMoTaDA.Text, txtMaPB.Text);
            return DA;
        }

        // Thêm 
        private void btnThem_Click(object sender, EventArgs e)
        {
            if(Check_TextBox())
            {
                DuAn DA = Create_DuAn();

            }
        }
        // Sửa
        private void btnSua_Click(object sender, EventArgs e)
        {

        }
        // Xóa
        private void btnXoa_Click(object sender, EventArgs e)
        {

        }

        private void dtGridQLDA_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
