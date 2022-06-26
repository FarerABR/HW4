using DAL.Enum.Product;

namespace DAL.Entity.Product
{
    public class GraphicsCard : Product
    {
        public GraphicsCard(string name, decimal price, int off, int rating, string image, ushort whoHasMadeId, ushort id, int hdmiCount, GraphMemType memoryType, Brand brand)
            : base(name, price, off, rating, whoHasMadeId, id, brand)
        {
            Image = image;
        }
        public int HDMICount { get; set; }
        public GraphMemType MemoryType { get; set; }
    }
}
