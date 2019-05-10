using System;
using System.Collections.Generic;

namespace test_entity.Models
{
    public partial class Role
    {
        public Role()
        {
            RoleOperations = new HashSet<RoleOperation>();
        }

        public string Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<RoleOperation> RoleOperations { get; set; }
    }
}
