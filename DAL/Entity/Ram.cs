using DAL.Enum.Product;
using System;

namespace DAL.Entity.Product
{
    public class Ram : Product
    {
        public int ModuleCount { get; set; }
        public RamMemType MemoryType { get; set; }
        public int ModuleCapacity { get; set; }
    }
}
