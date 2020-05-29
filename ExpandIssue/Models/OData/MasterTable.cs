using System;
using System.Collections.Generic;

namespace ExpandIssue.Models.OData
{
    public class MasterTableInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class MasterTableDetail : MasterTableInfo
    {
        public virtual IEnumerable<ChildTableInfo> Childs { get; set; }
    }
}
