using System;
using System.Collections.Generic;

namespace test_entity.Models
{
    public partial class Warehouse
    {
        public Warehouse()
        {
            Inventories = new HashSet<Inventory>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Inventory> Inventories { get; set; }
    }
}
