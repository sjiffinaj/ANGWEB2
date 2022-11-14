namespace Web.Entity
{
    public class ProductStock : BaseEntity
    {
        public decimal RequiredQty { get; set; }
        public long ProductId { get; set; }
        public virtual Product Product { get; set; }
        public long StockId { get; set; }
        public virtual Stock Stock { get; set; }
    }
}
