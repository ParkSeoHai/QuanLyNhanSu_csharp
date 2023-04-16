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
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace GUI
{
    public partial class frmChiTietChamCong : Form
    {
        BUS_ChiTietChamCong BUS_CTCC = new BUS_ChiTietChamCong();
        private string maQL = "";
        public frmChiTietChamCong()
        {
            InitializeComponent();
        }
        public frmChiTietChamCong(string maQL)
        {
            InitializeComponent();
            this.maQL = maQL;
        }
        // Hàm load dataGrid
        private void Load_DataGrid(DataTable dt)
        {
            dtGridCTCC.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtGridCTCC.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dtGridCTCC.DataSource = dt;
            // Bỏ thuộc tính sắp xếp của column 
            foreach (DataGridViewColumn column in dtGridCTCC.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            dtGridCTCC.Columns[0].HeaderText = "Mã CC";
            dtGridCTCC.Columns[1].HeaderText = "Ngày làm";
            dtGridCTCC.Columns[2].HeaderText = "Tháng làm";
            dtGridCTCC.Columns[3].HeaderText = "Năm làm";
            dtGridCTCC.Columns[4].HeaderText = "Trạng thái";
            dtGridCTCC.Columns[5].HeaderText = "Ghi chú";
            dtGridCTCC.Columns[6].HeaderText = "Mã NV";
        }
        // Hàm tạo đối tượng chấm công
        public ChamCong Creat_CC()
        {
            int day = dateThoiGianLam.Value.Day;
            int month = dateThoiGianLam.Value.Month;
            int year = dateThoiGianLam.Value.Year;
            string TrangThai = radioLam.Text;
            if(radioNghi.Checked)
            {
                TrangThai = radioNghi.Text;
            }
            ChamCong CC = new ChamCong(day, month, year, TrangThai, txtGhiChu.Text, txtMaNV.Text);
            return CC;
        }
        // Hàm check textBox
        private bool Check_TextBox()
        {
            if(string.IsNullOrWhiteSpace(txtMaNV.Text))
            {
                MessageBox.Show("Nhập mã nhân viên");
                txtMaNV.Focus();
                return false;
            }
            return true;
        }

        // Sự kiện load form
        private void frmChiTietChamCong_Load(object sender, EventArgs e)
        {
            panelTimKiem.Hide();
            dateThoiGianLam.Value = DateTime.Now;
            Load_DataGrid(BUS_CTCC.HienThi_CTCC());
        }
        // Sự kiện khi click quay lại 
        private void btnBack_Click(object sender, EventArgs e)
        {
            Hide();
            frmChamCongNV frmCC = new frmChamCongNV(maQL);
            frmCC.Show();
        }
        // Sự kiện hiển thị dữ liệu
        private void btnHienThi_Click(object sender, EventArgs e)
        {
            Load_DataGrid(BUS_CTCC.HienThi_CTCC());
        }
        // Sự kiện thêm
        private void btnThem_Click(object sender, EventArgs e)
        {
            if(Check_TextBox())
            {
                ChamCong CC = Creat_CC();  
                if(BUS_CTCC.Them_CCTC(CC))
                {
                    MessageBox.Show("Thêm thành công");
                }
                else
                {
                    MessageBox.Show("Thêm không thành công");
                }
            }
        }
        // Sự kiện sửa
        private void btnSua_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn sửa?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if(result == DialogResult.OK)
            {
                if(Check_TextBox())
                {
                    ChamCong CC = Creat_CC();
                    if(BUS_CTCC.Sua_CTCC(CC, txtMaCC.Text))
                    {
                        MessageBox.Show("Sửa thành công");
                    } else
                    {
                        MessageBox.Show("Sửa không thành công");
                    }
                }
            }
        }
        // Sự kiện xóa
        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                if (BUS_CTCC.Xoa_CTCC(txtMaCC.Text))
                {
                    MessageBox.Show("Xóa thành công");
                }
                else
                {
                    MessageBox.Show("Xóa không thành công");
                }
            }
        }
        // Sự kiện bấm tính lương cho nhân viên
        private void btnTinhLuong_Click(object sender, EventArgs e)
        {
            Hide();
            frmTinhLuong frmTL = new frmTinhLuong();
            frmTL.Show();
        }

        // Sự kiện click cell datagridView
        int index;
        private void dtGridCTCC_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            index = e.RowIndex;
            if(index >= 0)
            {
                txtMaCC.Text = dtGridCTCC.Rows[index].Cells[0].Value.ToString();
                string day = dtGridCTCC.Rows[index].Cells[1].Value.ToString();
                string month = dtGridCTCC.Rows[index].Cells[2].Value.ToString();
                string year = dtGridCTCC.Rows[index].Cells[3].Value.ToString();
                string date = $"{day}/{month}/{year}"; 
                if(date.Length < 4)
                {
                    dateThoiGianLam.Value = DateTime.Now;
                } else
                {
                    dateThoiGianLam.Value = Convert.ToDateTime(date);
                }
                string TrangThai = dtGridCTCC.Rows[index].Cells[4].Value.ToString();
                if (TrangThai == "Làm" || TrangThai == "LÀM" || TrangThai == "làm")
                {
                    radioLam.Checked = true;
                } else
                {
                    radioNghi.Checked = true;
                }
                txtGhiChu.Text = dtGridCTCC.Rows[index].Cells[5].Value.ToString();
                txtMaNV.Text = dtGridCTCC.Rows[index].Cells[6].Value.ToString();
            }
        }
        // Sự kiện tìm kiếm
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            panelTimKiem.Show();
        }
        // Thoát panel tìm kiếm
        private void btnThoatPanelTimKiem_Click(object sender, EventArgs e)
        {
            panelTimKiem.Hide();
        }
        private void btnThoatPanelTimKiem_MouseHover(object sender, EventArgs e)
        {
            btnThoatPanelTimKiem.BackColor = Color.Red;
            btnThoatPanelTimKiem.ForeColor = Color.White;
        }
        private void btnThoatPanelTimKiem_MouseLeave(object sender, EventArgs e)
        {
            btnThoatPanelTimKiem.BackColor = Color.White;
            btnThoatPanelTimKiem.ForeColor = Color.Black;
        }
        // Xem thông tin tìm kiếm
        private void btnXem_Click(object sender, EventArgs e)
        {
            DataTable dt = BUS_CTCC.HienThi_CTCC_ThangNam(txtMonth.Text, txtYear.Text);
            if (dt.Rows.Count > 0)
            {
                Load_DataGrid(dt);
            }
            else
            {
                MessageBox.Show("Không có kết quả để hiển thị");
            }
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
