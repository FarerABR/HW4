using DAL.Enum.Product;

namespace DAL.Entity.Product
{
    public class Ram : Product
    {
		public Ram(string name, decimal price, int off, int rating, string image, ushort whoHasMadeId, ushort id, int moduleCount, RamMemType memoryType, int moduleCapacity, Brand brand)
            : base(name, price, off, rating, whoHasMadeId, id, brand)
        {
            Image = image;
            ModuleCount = moduleCount;
            MemoryType = memoryType;
            ModuleCapacity = moduleCapacity;
        }
        public int ModuleCount { get; set; }
        public RamMemType MemoryType { get; set; }
        public int ModuleCapacity { get; set; }
    }
}