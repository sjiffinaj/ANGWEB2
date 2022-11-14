namespace Web.Entity
{
    public class StockDetail : BaseEntity
    {
        public long Current { get; set; }
        public long Needed { get; set; }
        public long StockId { get; set; }
        public virtual Stock Stock { get; set; }
    }
}
