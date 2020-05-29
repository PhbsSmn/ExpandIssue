using System.Linq;
using ExpandIssue.Models.EntityFramework;
using ExpandIssue.Models.OData;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;

namespace ExpandIssue.OData
{
    [Produces("application/json")]
    public class MasterTablesController : ODataController
    {
        #region Fields
        private readonly ExpandIssueDbContext _context;
        #endregion

        #region Constructors
        public MasterTablesController(ExpandIssueDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Requests
        [EnableQuery(EnableCorrelatedSubqueryBuffering = true, EnsureStableOrdering = false)]
        public IQueryable<MasterTableDetail> Get()
        {
            return _context.MasterTable.Select(m => new MasterTableDetail
            {
                Id = m.Id,
                Name = m.Name,
                Childs = m.ChildTable.Select(c => new ChildTableInfo
                {
                    Id = c.Id,
                    Created = c.Created,
                    Value = c.Value
                })
            }).OrderBy(m => m.Id);

        }
        #endregion
    }
}