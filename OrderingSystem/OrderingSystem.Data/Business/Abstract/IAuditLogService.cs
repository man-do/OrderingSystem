using OrderingSystem.Data.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Data.Business.Abstract
{
    public interface IAuditLogService
    {
        IEnumerable<AuditLogViewModel> GetAll();
    }
}
