using DAL.Enum.Product;

namespace DAL.Entity.Product
{
    public class Processor : Product
    {
        public Processor(string name, double price, int off, int rating, string image, User whoHasMade, long id, int coreCount, ProcessorType series, Brand brand)
            : base(name, price, off, rating, whoHasMade, id, brand)
        {
            Image = image;
            CoreCount = coreCount;
            Series = series;
        }
        public int CoreCount { get; set; }
        public ProcessorType Series { get; set; }
    }
}
