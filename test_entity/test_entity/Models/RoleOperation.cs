using System;
using System.Collections.Generic;

namespace test_entity.Models
{
    public partial class RoleOperation
    {
        public string RoleId { get; set; }
        public string OperationId { get; set; }

        public virtual Operation Operation { get; set; }
        public virtual Role Role { get; set; }
    }
}
