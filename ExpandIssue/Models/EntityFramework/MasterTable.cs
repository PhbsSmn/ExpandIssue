using System;
using System.Collections.Generic;

namespace ExpandIssue.Models.EntityFramework
{
    public partial class MasterTable
    {
        public MasterTable()
        {
            ChildTable = new HashSet<ChildTable>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ChildTable> ChildTable { get; set; }
    }
}
