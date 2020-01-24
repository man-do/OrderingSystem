using OrderingSystem.Data.Business.Abstract;
using OrderingSystem.Data.Data.Abstract;
using OrderingSystem.Data.Data.Entities;
using OrderingSystem.Data.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Data.Business.Concrete
{
    public class AuditLogService : IAuditLogService
    {
        private readonly IRepository<AuditLog> _logRepository;

        public AuditLogService(IRepository<AuditLog> logRepository)
        {
            _logRepository = logRepository;
        }

        public IEnumerable<AuditLogViewModel> GetAll()
        {
            List<AuditLog> logs = _logRepository.GetAll().ToList();
            List<AuditLogViewModel> auditLogs = new List<AuditLogViewModel>();
            foreach(AuditLog log in logs)
            {
                AuditLogViewModel auditLog = new AuditLogViewModel
                {
                    Date = log.Date,
                    Level = log.Level,
                    Message = log.Message,
                    Exception = log.Exception
                };
                auditLogs.Add(auditLog);
            }
            return auditLogs;
        }
    }
}
