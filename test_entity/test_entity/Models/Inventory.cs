using System;
using System.Collections.Generic;

namespace test_entity.Models
{
    public partial class Inventory
    {
        public Guid CategoryId { get; set; }
        public Guid ProductId { get; set; }
        public Guid WarehouseId { get; set; }
        public int? Quantity { get; set; }

        public virtual Category Category { get; set; }
        public virtual Product Product { get; set; }
        public virtual Warehouse Warehouse { get; set; }
    }
}
