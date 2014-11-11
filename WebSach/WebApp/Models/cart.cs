using System.Collections.Generic;
using System.Linq;

namespace WebApp.Models
{
    public class Cart
    {
        private List<CartLine> dssanpham = new List<CartLine>();
        public void AddItem(string Masp, string Tensp, float Giasanpham, int quantity, string tendn)
        {
            CartLine line = dssanpham.Where(p => p.masp == Masp).FirstOrDefault();
            if (line == null)
            {
                dssanpham.Add(new CartLine { masp = Masp, tensp = Tensp, giasanpham = Giasanpham, Quantity = quantity, tendn = tendn });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void Remove(string masp)
        {
            CartLine line = dssanpham.Where(p => p.masp.Contains(masp)).FirstOrDefault();
            dssanpham.Remove(line);
        }
        public float tongtien()
        {
            return dssanpham.Sum(e => e.giasanpham * e.Quantity);
        }
        public void capnhatsoluong(string masp, int soluong)
        {
            CartLine line = dssanpham.Where(p => p.masp == masp).FirstOrDefault();
           line.Quantity = soluong;
        }
        public void Clear()
        {
            dssanpham.Clear();
        }
        public decimal tongsanpham()
        {
            return dssanpham.Sum(e => e.Quantity);
        }
        public IEnumerable<CartLine> Lines
        {
            get { return dssanpham; }
        }
    }
    public class CartLine
    {
        public string masp { get; set; }
        public string tensp { get; set; }
        public float giasanpham { get; set; }
        public int Quantity { get; set; }
        public string tendn { get; set; }
    }

}
