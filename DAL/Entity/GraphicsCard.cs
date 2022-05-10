using System;
using DAL.Enum.Product;

namespace DAL.Entity.Product
{
    public class GraphicsCard : Product
    {
        public string HTMICount { get; set; }
        public GraphMemType MemoryType { get; set; }
    }
}
