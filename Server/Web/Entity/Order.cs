namespace Web.Entity
{
    public class Order : BaseEntity
    {
        public int OrderNo { get; set; }
        public byte Status { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaymentType { get; set; } // cash, card
        public byte OrderType { get; set; } // Dine In Home delivery
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public long DineInTableId { get; set; }
        public virtual DineInTable DineInTable { get; set; }
    }
}
