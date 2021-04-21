using System;
using System.Collections.Generic;

namespace Models
{
    public class Order : BaseEntity
    {
        public DateTime OrderDate { get; set; }
        public string OrderNumber { get; set; }
        public virtual Customer Customer { get; set; } // Navigation Property
        public OrderStatus Status { get; set; }
        public  ICollection<OrderDetail> Details { get; set; } // Navigation Property

        public Order()
        {
            Details = new HashSet<OrderDetail>();

            Status = OrderStatus.Ordered;
            OrderDate = DateTime.Now;
        }
    }
}
