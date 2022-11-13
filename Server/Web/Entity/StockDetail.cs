namespace Web.Entity
{
    public class StockDetail : BaseEntity
    {
        public long StockId { get; set; }
        public long Current { get; set; }
        public long Needed { get; set; }

    }
}
