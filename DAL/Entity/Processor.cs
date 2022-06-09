using DAL.Enum.Product;

namespace DAL.Entity.Product
{
    public class Processor : Product
    {
        public Processor(string name, double price, int off, int rating, string image, User whoHasMade) : base(name, price, off, rating, whoHasMade)
        {
            Image = image;
        }
        public int CoreCount { get; set; }
        public ProcessorType Series { get; set; }
    }
}
