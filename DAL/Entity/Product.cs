using DAL.Enum.Product;

namespace DAL.Entity.Product
{
    public class Product
    {
		public Product(string name, double price, int discount, int rating, User whoHasMade, long id, Brand brand)
		{
            Name = name;
            Discount = discount;
            Price = price;
            Rating = rating;
            WhoHasMade = whoHasMade;
            _id = id;
            _date_Of_Registration = DateTime.Now;
            Brand = brand;
		}

        private readonly long _id;
        public long Id { get { return _id; } }
        public string Name { get; set; }

        public Brand? Brand { get; set; }

        public double Price { get; set; }

        public int Discount { get; set; }

        public int Rating { get; set; }

        public string Image { get; set; }

        private readonly DateTime _date_Of_Registration;
        public DateTime Date_Of_Registration { get { return _date_Of_Registration; } }
        public User WhoHasMade { get; set; }

        public ViewStatus ViewStatus { get; set; } = ViewStatus.visible;
        public bool In_Stock { get; set; } = true;
    }
}
