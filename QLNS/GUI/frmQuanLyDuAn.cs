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
            cbBox_TimKiem.Text = cbBox_TimKiem.Items[0].ToString();
            panel_TimKiem.Hide();
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
            if(index >= 0)
            {
                txtMaDA.Text = dtGridQLDA.Rows[index].Cells[0].Value.ToString();
                txtTenDA.Text = dtGridQLDA.Rows[index].Cells[1].Value.ToString();
                txtSoNV.Text = dtGridQLDA.Rows[index].Cells[2].Value.ToString();
                txtMoTaDA.Text = dtGridQLDA.Rows[index].Cells[3].Value.ToString();
                txtMaPB.Text = dtGridQLDA.Rows[index].Cells[4].Value.ToString();
            }
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
                if(BUS_QLDA.ThemDA(DA))
                {
                    MessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Load_dtGridQLDA(BUS_QLDA.HienThiDuLieu());
                    Clear_TextBox();
                } else
                {
                    MessageBox.Show("Thêm không thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        // Sửa
        private void btnSua_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn sửa", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if(result == DialogResult.OK)
            {
                if (Check_TextBox())
                {
                    DuAn DA = Create_DuAn();
                    if (BUS_QLDA.SuaDA(DA))
                    {
                        MessageBox.Show("Sửa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Load_dtGridQLDA(BUS_QLDA.HienThiDuLieu());
                        Clear_TextBox();
                    }
                    else
                    {
                        MessageBox.Show("Sửa không thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        // Xóa
        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if(result == DialogResult.OK)
            {
                if (Check_TextBox())
                {
                    DuAn DA = Create_DuAn();
                    if (BUS_QLDA.XoaDA(DA))
                    {
                        MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Load_dtGridQLDA(BUS_QLDA.HienThiDuLieu());
                        Clear_TextBox();
                    }
                    else
                    {
                        MessageBox.Show("Xóa không thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        int i = 0;
        // Tìm kiếm
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            panel_TimKiem.Show();
            dtGridQLDA.DataSource = null;
            if(i >= 1)
            {
                if(string.IsNullOrWhiteSpace(txtTimKiem.Text))
                {
                    MessageBox.Show("Chưa nhập thông tin cần tìm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtTimKiem.Focus();
                } else
                {
                    if (cbBox_TimKiem.Text == "Tìm kiếm theo Mã DA")
                    {
                        DataTable dt = BUS_QLDA.TimKiemDA_Ma(txtTimKiem.Text);
                        Load_dtGridQLDA(dt);
                        MessageBox.Show($"Tìm thấy {dt.Rows.Count} kết quả");
                    }
                    else if (cbBox_TimKiem.Text == "Tìm kiếm theo Tên DA")
                    {
                        DataTable dt = BUS_QLDA.TimKiemDA_Ten(txtTimKiem.Text);
                        Load_dtGridQLDA(dt);
                        MessageBox.Show($"Tìm thấy {dt.Rows.Count} kết quả");
                    }
                }
            }
            i++;
        }
        // Sự kiện thoát panel tìm kiếm
        private void btnThoat_TimKiem_Click(object sender, EventArgs e)
        {
            panel_TimKiem.Hide();
            i = 0;
        }
        // Hiển thị
        private void btnHienThi_Click(object sender, EventArgs e)
        {
            Load_dtGridQLDA(BUS_QLDA.HienThiDuLieu());
        }
    }
}
