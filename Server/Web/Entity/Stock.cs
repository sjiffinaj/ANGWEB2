namespace Web.Entity
{
    public class Stock : BaseEntity
    {
        public string Name { get; set; }
        public string UOM { get; set; }
        public int StockMin { get; set; }
        public bool IsMinRequired { get; set; }
        public virtual ICollection<StockDetail> StockDetails { get; set; }
        public virtual ICollection<ProductStock> ProductStocks { get; set; }
    }
}
