using System;
using System.Collections.Generic;

namespace ExpandIssue.Models.EntityFramework
{
    public partial class ChildTable
    {
        public int Id { get; set; }
        public int MasterTableId { get; set; }
        public DateTime Created { get; set; }
        public string Value { get; set; }

        public virtual MasterTable MasterTable { get; set; }
    }
}
