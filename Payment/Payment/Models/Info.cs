using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Payment.db;

namespace Payment.Models
{
    public class Info
    {
        public List<Donhang> ListDonhang { get; set; }
        public List<Sanpham> ListSanpham { get; set; }

            
    }
}