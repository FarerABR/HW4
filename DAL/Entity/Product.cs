using DAL.Enum.Product;
using System;

namespace DAL.Entity.Product
{
    public class Product
    {
        public bool IsDeleted { get; set; } = false;

        private int _Id { get; set; }
        public int Id
        {
            get { return _Id; }
            set
            {
                _Id = Id_Seed++;
            }
        }
        public string Name { get; set; }

        public Brand Brand { get; set; }
        public decimal Price { get; set; }


        public decimal Discount { get; set; }

        public bool In_Stock { get; set; }

        public uint Score { get; set; }
        public DateTime Date_Of_Registration { get; set; }
        public ViewStatus ViewStatus { get; set; }

        public string[] Photos { get; set; }


        public static int Id_Seed { get; set; } = 0;
    }
}
