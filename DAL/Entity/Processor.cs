using System;
using DAL.Enum.Product;
namespace DAL.Entity.Product
{
    public class Processor : Product
    {
        public Processor(string name, string off, string price, int rating, string image) : base(name, off, price, rating)
        {
            Image = image;
        }
        public int CoreCount { get; set; }
        public ProcessorType Series { get; set; }
    }
}
