using DAL.Enum.Product;
using System;

namespace DAL.Entity.Product
{
    public class Product
    {
		public Product(string name, string discount, string price, int rating)
		{
            Name = name;
            Discount = discount;
            Price = price;
            Rating = rating;
		}

        private int _Id { get; set; }
        public int Id
        {
            get { return _Id; }
            set
            {
                _Id = ++Id_Seed;
            }
        }
        public string Name { get; set; }

        public Brand Brand { get; set; }
        public string Price { get; set; }

        public string Discount { get; set; }

        public bool In_Stock { get; set; }

        public int Rating { get; set; }
        public DateTime Date_Of_Registration { get; set; }
        public ViewStatus ViewStatus { get; set; }

        public string Image { get; set; }


        private static int Id_Seed { get; set; } = 0;
    }
}
