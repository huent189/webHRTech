using System;
using System.Collections.Generic;

namespace test_entity.Models
{
    public partial class Category
    {
        public Category()
        {
            Inventories = new HashSet<Inventory>();
            InverseParent = new HashSet<Category>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? ParentId { get; set; }

        public virtual Category Parent { get; set; }
        public virtual ICollection<Inventory> Inventories { get; set; }
        public virtual ICollection<Category> InverseParent { get; set; }
    }
}
