namespace Web.Entity
{
    public class DailyProductDetail : BaseEntity
    {
        public int ProductId { get; set; }
        public int DailyMin { get; set; }
        public int Balance { get; set; }
        public int Opening { get; set; }
        public bool IsMinRequired { get; set; }
    }
}
