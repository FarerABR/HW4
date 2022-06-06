using DAL.Enum.Product;
using System;

namespace DAL.Entity.Product
{
    public class Motherboard : Product
    {
        public Motherboard(string name, string off, string price, int rating, string image) : base(name, off, price, rating)
        {
            Image = image;
        }
        public MotherBased BasedOn { get; set; }
        public RAID RAIDSupport { get; set; }
        public int RAMCount { get; set; }
        public int PCICount { get; set; }
    }
}
