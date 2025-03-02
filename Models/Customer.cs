namespace MVCMovieApp.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public ICollection<ProductSold> ProductSold { get; set; } = new List<ProductSold>();

    }
}
