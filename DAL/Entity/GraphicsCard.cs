using System;
using DAL.Enum.Product;

namespace DAL.Entity.Product
{
    public class GraphicsCard : Product
    {
        public GraphicsCard(string name, string off, string price, int rating, string image) : base(name, off, price, rating)
        {
            Image = image;
        }
        public string HDMICount { get; set; }
        public GraphMemType MemoryType { get; set; }
    }
}
