using DAL.Enum.Product;
using System;

namespace DAL.Entity.Product
{
    public class Motherboard : Product
    {
        public MotherBased BasedOn { get; set; }
        public RAID RAIDSupport { get; set; }
        public int RAMCount { get; set; }
        public int PCICount { get; set; }
    }
}
