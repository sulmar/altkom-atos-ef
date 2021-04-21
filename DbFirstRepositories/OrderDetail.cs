namespace DbFirstRepositories
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OrderDetail
    {
        public int Id { get; set; }

        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }

        public int? Item_Id { get; set; }

        public int? Order_Id { get; set; }

        public virtual Item Item { get; set; }

        public virtual Order Order { get; set; }
    }
}
