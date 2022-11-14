namespace Web.Entity
{
    public class OrderDetail : BaseEntity
    {
        public int Count { get; set; }
        public decimal Price { get; set; }
        public long OrderId { get; set; }
        public virtual Order Order { get; set; }
        public long ProductId { get; set; }
        public virtual Product Product { get; set; }

    }
}
