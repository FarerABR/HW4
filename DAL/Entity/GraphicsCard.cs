using DAL.Enum.Product;

namespace DAL.Entity.Product
{
    public class GraphicsCard : Product
    {
        public GraphicsCard(string name, double price, int off, int rating, string image, User whoHasMade) : base(name, price, off, rating, whoHasMade)
        {
            Image = image;
        }
        public string HDMICount { get; set; }
        public GraphMemType MemoryType { get; set; }
    }
}
