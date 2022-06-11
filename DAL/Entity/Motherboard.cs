using DAL.Enum.Product;

namespace DAL.Entity.Product
{
    public class Motherboard : Product
    {
        public Motherboard(string name, double price, int off, int rating, string image, User whoHasMade, long id, MotherBased basedOn, RAID raidSupport, int ramCount, int pciCount, Brand brand)
            : base(name, price, off, rating, whoHasMade, id, brand)
        {
            Image = image;
            BasedOn = basedOn;
            RAIDSupport = raidSupport;
            RAMCount = ramCount;
            PCICount = pciCount;
        }
        public MotherBased BasedOn { get; set; }
        public RAID RAIDSupport { get; set; }
        public int RAMCount { get; set; }
        public int PCICount { get; set; }
    }
}
