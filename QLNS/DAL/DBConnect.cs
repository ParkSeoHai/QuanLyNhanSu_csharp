using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DAL
{
    public class DBConnect
    {
        public SqlConnection ChuoiKetNoi_Hai()
        {
            string connString = @"Data Source=DESKTOP-LNJ99RH\SQLEXPRESS;Initial Catalog=QuanLyNhanSu;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connString);
            return conn;
        }
        public SqlConnection chuoiKetNoi_Mot()
        {
            string strCon = @"Data Source=DESKTOP-LNJ99RH\SQLEXPRESS;Initial Catalog=QuanLyNhanSu;Integrated Security=True";
            SqlConnection sqlCon = new SqlConnection(strCon);
            sqlCon.Open();
            return sqlCon;
        }
        
    }
}
