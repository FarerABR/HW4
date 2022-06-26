using DAL.Enum.Product;

namespace DAL.Entity.Product
{
    public class Product
    {
		public Product(string name, decimal price, int discount, int rating, ushort whoHasMadeId, ushort id, Brand brand)
		{
            Name = name;
            Discount = discount;
            Price = price;
            Rating = rating;
            WhoHasMadeId = whoHasMadeId;
            _id = id;
            Date_Of_Registration = DateTime.Now;
            Brand = brand;
		}

        private readonly ushort _id;
        public ushort Id { get { return _id; } }
        public string Name { get; set; }

        public Brand? Brand { get; set; }

        public decimal Price { get; set; }

        public int Discount { get; set; }

        public int Rating { get; set; }

        public string Image { get; set; }

        public DateTime Date_Of_Registration { get; set; }

        public ushort WhoHasMadeId;

        public ViewStatus ViewStatus { get; set; } = ViewStatus.visible;

        public List<ushort> AddedToCartIds_List = new();
    }
}
