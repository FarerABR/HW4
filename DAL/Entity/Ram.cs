using DAL.Enum.Product;

namespace DAL.Entity.Product
{
    public class Ram : Product
    {
		public Ram(string name, double price, int off, int rating, string image, User whoHasMade) : base(name, price, off, rating, whoHasMade)
        {
            Image = image;
		}
        public int ModuleCount { get; set; }
        public RamMemType MemoryType { get; set; }
        public int ModuleCapacity { get; set; }
    }
}