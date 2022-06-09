using DAL.Enum.Product;

namespace DAL.Entity.Product
{
    public class Motherboard : Product
    {
        public Motherboard(string name, double price, int off, int rating, string image, User whoHasMade) : base(name, price, off, rating, whoHasMade)
        {
            Image = image;
        }
        public MotherBased BasedOn { get; set; }
        public RAID RAIDSupport { get; set; }
        public int RAMCount { get; set; }
        public int PCICount { get; set; }
    }
}
