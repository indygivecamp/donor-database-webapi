using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using DonorDatabase.Models;

namespace DonorDatabase.Controllers
{
    public class TodoController : ApiController
    {
        private donordatabaseEntities db = new donordatabaseEntities();

        // GET api/Todo
        [Authorize]
        public IEnumerable<Contact> GetOpenContact()
        {
            return db.Contacts.Where(p => !p.CompleteDate.HasValue).OrderByDescending(p => p.ScheduleDate).ThenBy(p => p.LOV_Fundraiser.Name).AsEnumerable();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}