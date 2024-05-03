namespace Catalog.API.Models
{
    public class Product
    {
        public Guid Id { get; set; }    
        public string Nombre { get; set; }
        public List<string> Category { get; set; }
        public string Description { get; set; }
        public string ImageFile { get; set; }
        public decimal Price { get; set; }    
    }
}