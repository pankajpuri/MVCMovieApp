using System.ComponentModel.DataAnnotations;

namespace MVCMovieApp.Models
{
    public class ProductSold
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public int StoreId { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateSold { get; set; }

        public Customer Customers { get; set; }
        public Product Products { get; set; }  
        public  Store Stores { get; set; }

    }
}
