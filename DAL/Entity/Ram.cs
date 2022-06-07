using DAL.Enum.Product;
using System;

namespace DAL.Entity.Product
{
    public class Ram : Product
    {
		public Ram(string name, string off, string price, int rating, string image) : base(name, off, price, rating)
		{
            Image = image;
		}
        public int ModuleCount { get; set; }
        public RamMemType MemoryType { get; set; }
        public int ModuleCapacity { get; set; }
    }
}