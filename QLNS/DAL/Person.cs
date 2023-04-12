using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Person
    {
        protected string HoTen { get; set; }
        protected string GioiTinh { get; set; }
        protected string NgaySinh { get; set; }
        protected string DiaChi { get; set; }
        protected string SDT { get; set; }
        protected string Email { get; set; }

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
