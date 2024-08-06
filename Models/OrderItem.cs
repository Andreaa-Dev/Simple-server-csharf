namespace ECommerceAPI.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }

        // way 1:
        // public OrderItem(int id, int productId, Product product, int quantity)
        // {
        //     Id = id;
        //     ProductId = productId;
        //     Product = product ?? throw new ArgumentNullException(nameof(product)); // Ensure non-null value
        //     Quantity = quantity;
        // }

        // way 2: public Product? Product { get; set; }

        // way 3: public Product Product { get; set; } = new Product(); 

    }
}


