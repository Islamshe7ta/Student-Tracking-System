using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentTrackingSystem.DAL.Models;

namespace StudentTrackingSystem.BLL.Interfaces
{
    public interface IAuditLogRepository
    {
        Task<IEnumerable<AuditLog>> GetPagedAsync(int pageNumber, int pageSize);
    }
}
