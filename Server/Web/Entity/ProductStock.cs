namespace Web.Entity
{
    public class ProductStock : BaseEntity
    {
        public long ProductId { get; set; }
        public long StockId { get; set; }
        public decimal RequiredQty { get; set; }

    }
}
