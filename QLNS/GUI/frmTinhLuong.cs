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
    public partial class frmTinhLuong : Form
    {
        BUS_TinhLuong bus_TinhLuong = new BUS_TinhLuong();
        private string maQL = "";
        public frmTinhLuong()
        {
            InitializeComponent();
        }
        public frmTinhLuong(string maQL)
        {
            this.maQL = maQL;
            InitializeComponent();
        }
        List<LuongNV> ListLuong;
        private void frmTinhLuong_Load(object sender, EventArgs e)
        {
            int month = DateTime.Now.Month;
            txtThang.Text = month.ToString();
            int year = DateTime.Now.Year;
            txtNam.Text = year.ToString();

            int Thang = Convert.ToInt32(txtThang.Text);
            int Nam = Convert.ToInt32(txtNam.Text);
            Load_List(Thang, Nam);
        }
        // Load list
        private void Load_List(int Thang, int Nam)
        {
            ListLuong = bus_TinhLuong.HienThi_Luong(Thang, Nam);
            Load_DataLuong(ListLuong);
        }

        // Load Dữ liệu bảng lương
        private void Load_DataLuong(List<LuongNV> source)
        {
            dtGridLuong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dtGridLuong.ColumnHeadersDefaultCellStyle.Font = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold);
            dtGridLuong.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dtGridLuong.DataSource = source;
            dtGridLuong.Columns[0].HeaderText = "Mã NV";
            dtGridLuong.Columns[1].HeaderText = "Họ tên";
            dtGridLuong.Columns[2].HeaderText = "Chức vụ";
            dtGridLuong.Columns[3].HeaderText = "Tổng công";
            dtGridLuong.Columns[4].HeaderText = "Lương/Công";
            dtGridLuong.Columns[5].HeaderText = "Lương thực nhận";
        }

        // Tính Lương
        private void btnTinhLuong_Click(object sender, EventArgs e)
        {
            int Thang = Convert.ToInt32(txtThang.Text);
            int Nam = Convert.ToInt32(txtNam.Text);

            var source = bus_TinhLuong.HienThi_Luong(Thang, Nam);
            ListLuong = source;

            Load_DataLuong(source);
        }
        // Tạo báo cáo
        private void btnReport_Click(object sender, EventArgs e)
        {
            Hide();
            frmThongKeLuong frmReport = new frmThongKeLuong(ListLuong);
            frmReport.Show();
        }
        // Quay lại form Chi tiết chấm công
        private void btnBack_Click(object sender, EventArgs e)
        {
            Hide();
            frmChiTietChamCong frmCTCC = new frmChiTietChamCong(maQL);
            frmCTCC.Show();
        }
    }
}
