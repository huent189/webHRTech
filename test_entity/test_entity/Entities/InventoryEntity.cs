using System;
using System.Collections.Generic;
using System.Text;

namespace test_entity.Entities
{
    class InventoryEntity
    {
        public Guid CategoryId { get; set; }
        public Guid ProductId { get; set; }
        public Guid WarehouseId { get; set; }
        public int? Quantity { get; set; }

        public string Category { get; set; }
        public string Product { get; set; }
        public string Warehouse { get; set; }
    }
}
