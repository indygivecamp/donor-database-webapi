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
    public class InterestController : ApiController
    {
        private donordatabaseEntities db = new donordatabaseEntities();

        // GET api/Interest
        [Authorize]
        public IEnumerable<Interest> GetInterest()
        {
            return db.Interests.AsEnumerable();
        }

        // GET api/Interest/5
        [Authorize]
        public Interest GetInterest(int id)
        {
            var items = db.Interests.Include(p => p.LOV);
            Interest item = items.Where(p => p.InterestID == id).FirstOrDefault();
            if (item == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return item;
        }

        // PUT api/Interest/5
        [Authorize]
        public HttpResponseMessage PutInterest(int id, Interest item)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != item.InterestID)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(item).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // POST api/Interest
        [Authorize]
        public HttpResponseMessage PostInterest(Interest item)
        {
            if (ModelState.IsValid)
            {
                db.Interests.Add(item);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, item);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = item.InterestID }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Interest/5
        [Authorize]
        public HttpResponseMessage DeleteInterest(int id)
        {
            Interest item = db.Interests.Find(id);
            if (item == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Interests.Remove(item);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, item);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}