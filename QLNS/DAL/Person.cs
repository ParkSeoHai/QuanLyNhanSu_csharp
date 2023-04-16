using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Person
    {
        public string HoTen { get; set; }
        public string GioiTinh { get; set; }
        public string NgaySinh { get; set; }
        public string DiaChi { get; set; }
        public string SDT { get; set; }
        public string Email { get; set; }

        public Person(string HoTen, string GioiTinh, string NgaySinh, string DiaChi, string SDT, string Email)
        {
            this.HoTen = HoTen;
            this.GioiTinh = GioiTinh;
            this.NgaySinh = NgaySinh;
            this.DiaChi = DiaChi;
            this.SDT = SDT;
            this.Email = Email;
        }
        public Person() { }
    }
}
