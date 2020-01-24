using OrderingSystem.Data.Business.Abstract;
using OrderingSystem.Data.Data.Abstract;
using OrderingSystem.Data.Data.Entities;
using OrderingSystem.Data.Data.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrderingSystem.WebApp.Controllers
{
    public class AuditLogController : Controller
    {
        private IAuditLogService _auditLogService = null;
    
        public AuditLogController(IAuditLogService auditLogService)
        {
            _auditLogService = auditLogService;
        }

        // GET: AuditLog
        public ActionResult Index(int page)
        {
            IEnumerable<AuditLogViewModel> result = _auditLogService.GetAll().ToPagedList((page == 0) ? 1 : page, 10);
            return View(result);
        }
    }
}