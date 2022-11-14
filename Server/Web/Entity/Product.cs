﻿namespace Web.Entity
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }

        public bool IsAvailable { get; set; }

        public string PhotoUrl { get; set; }

        public long ProductStockId { get; set; }

        public virtual ICollection<ProductStock> ProductStocks { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<DailyProductDetail> DailyProductDetails { get; set; }

    }
}
