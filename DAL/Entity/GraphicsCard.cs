using DAL.Enum.Product;

namespace DAL.Entity.Product
{
    public class GraphicsCard : Product
    {
        public GraphicsCard(string name, double price, int off, int rating, string image, User whoHasMade, long id, int hdmiCount, GraphMemType memoryType, Brand brand)
            : base(name, price, off, rating, whoHasMade, id, brand)
        {
            Image = image;
        }
        public int HDMICount { get; set; }
        public GraphMemType MemoryType { get; set; }
    }
}
