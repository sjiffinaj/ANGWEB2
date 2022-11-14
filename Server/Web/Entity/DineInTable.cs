namespace Web.Entity
{
    public class DineInTable : BaseEntity
    {
        public string TableNo { get; set; }
        public byte SeatCapacity { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
