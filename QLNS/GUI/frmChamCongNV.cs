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
    public partial class frmChamCongNV : Form
    {
        BUS_ChamCongNV bus_ChamCong = new BUS_ChamCongNV();
        private string maQL = "";
        public frmChamCongNV()
        {
            InitializeComponent();
        }
        public frmChamCongNV(string maQL)
        {
            InitializeComponent();
            this.maQL = maQL;
        }
        // Sự kiện Load form
        private void frmChamCongNV_Load(object sender, EventArgs e)
        {
            dateLamViec.Value = DateTime.Now;
            Load_DataGrid();
        }

        // Hàm load datagridView
        private void Load_DataGrid()
        {
            dtGridNV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DataTable dtNhanVien = bus_ChamCong.HienThiNV_ChamCong();
            dtGridNV.DataSource = dtNhanVien;

            // Bỏ thuộc tính sắp xếp của column 
            foreach (DataGridViewColumn column in dtGridNV.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            dtGridNV.Columns[0].HeaderText = "Mã NV";
            dtGridNV.Columns[1].HeaderText = "Họ tên";
            dtGridNV.Columns[2].HeaderText = "Giới tính";
            dtGridNV.Columns[3].HeaderText = "Chức vụ";
            dtGridNV.Columns[4].HeaderText = "Vị trí CV";
            dtGridNV.Columns[5].HeaderText = "Mã PB";

            if (dtGridNV.Rows.Count > 0)
            {
                // List mã nhân viên đã được chấm công trong ngày
                List<string> listMaNV = Get_ListMaNV();

                var ColumnName = dtNhanVien.Columns[0].ColumnName;

                foreach (string maNv in listMaNV)
                {
                    var value = maNv;
                    // Tìm kiếm mã nhân viên trong DataTable dtNhanVien
                    var findMaNV = dtNhanVien.Select($"{ColumnName} like '%" + value + "%'");
                    foreach (DataRow dr in findMaNV)
                    {
                        // Nếu mã nhân viên được tìm thấy thì thay đổi thuộc tính màu sắc của row DataGridView
                        if (dr != null)
                        {
                            // Lấy vị trí value tìm thấy trong DataTable
                            var index = dtNhanVien.Rows.IndexOf(dr);
                            dtGridNV.Rows[index].DefaultCellStyle.BackColor = Color.Red;
                            dtGridNV.Rows[index].DefaultCellStyle.ForeColor = Color.White;
                        }
                    }
                }
            }
        }
        // Hàm tạo đối tượng ChamCong
        private ChamCong Create_CC()
        {
            int day = dateLamViec.Value.Day;
            int month = dateLamViec.Value.Month;
            int year = dateLamViec.Value.Year;

            string TrangThai = radioLam.Text;
            if (radioNghi.Checked)
            {
                TrangThai = radioNghi.Text;
            }

            ChamCong CC = new ChamCong(day, month, year, TrangThai, txtGhiChu.Text, txtMaNV.Text);
            return CC;
        }
        // Hàm lấy ra list mã nv đã được chấm công trong ngày
        private List<string> Get_ListMaNV()
        {
            int day = dateLamViec.Value.Day;
            int month = dateLamViec.Value.Month;
            int year = dateLamViec.Value.Year;

            List<string> listMaNV = bus_ChamCong.Get_ListMaNV(day, month, year);
            return listMaNV;
        }
        // Hàm clear Text
        private void Clear_Text()
        {
            txtMaNV.Clear();
            txtTenNV.Clear();
            radioLam.Checked = true;
            txtGhiChu.Clear();
        }
        // Hàm check Text
        private bool Check_Text()
        {
            if(string.IsNullOrWhiteSpace(txtMaNV.Text)) {
                MessageBox.Show("Nhập mã nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMaNV.Focus();
                return false;
            } else if(string.IsNullOrWhiteSpace(txtTenNV.Text))
            {
                MessageBox.Show("Nhập tên nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTenNV.Focus();
                return false;
            }
            return true;
        }

        // Sự kiện cell click dataGrid View
        int index;
        private void dtGridNV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            index = e.RowIndex;
            if(index >= 0)
            {
                txtMaNV.Text = dtGridNV.Rows[index].Cells[0].Value.ToString();
                txtTenNV.Text = dtGridNV.Rows[index].Cells[1].Value.ToString();
            }
        }
        // Sự kiện chấm công
        private void btnChamCong_Click(object sender, EventArgs e)
        {
            if(Check_Text())
            {
                ChamCong CC = Create_CC();
                if (bus_ChamCong.ChamCongNV(CC))
                {
                    MessageBox.Show("Chấm công thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Nhân viên được chấm công thì có màu nền đỏ chữ trắng
                    dtGridNV.Rows[index].DefaultCellStyle.BackColor = Color.Red;
                    dtGridNV.Rows[index].DefaultCellStyle.ForeColor = Color.White;
                    Clear_Text();
                }
                else
                {
                    MessageBox.Show("Nhân viên này đã được chấm công hôm nay", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        // Sự kiện khi thay đổi giá trị DateTime thì load xem ngày đó những nhân viên nào đã được chấm công
        private void dateLamViec_ValueChanged(object sender, EventArgs e)
        {
            Load_DataGrid();
        }

        // Sự kiện khi click xem chi tiết chấm công
        private void btnCCTC_Click(object sender, EventArgs e)
        {
            Hide();
            frmChiTietChamCong frmCTCC = new frmChiTietChamCong(maQL);
            frmCTCC.Show();
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            FormTrang_Chu trangChu = new FormTrang_Chu(maQL);
            this.Hide();
            trangChu.ShowDialog();
        }
    } 
}
