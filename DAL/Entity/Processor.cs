using System;
using DAL.Enum.Product;
namespace DAL.Entity.Product
{
    public class Processor : Product
    {
        public int CoreCount { get; set; }
        public ProcessorType Series { get; set; }
    }
}
