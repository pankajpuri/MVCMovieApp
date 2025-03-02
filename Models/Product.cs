namespace MVCMovieApp.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public ICollection<ProductSold> ProductSold { get; set; } = new List<ProductSold>();
    }
}
