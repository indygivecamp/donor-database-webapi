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
    public class LOVController : ApiController
    {
        private donordatabaseEntities db = new donordatabaseEntities();

        // GET api/LOV
        [Authorize]
        public IEnumerable<LOV> GetLOV()
        {
            return db.LOVs.AsEnumerable();
        }

        // GET api/LOV/5
        [Authorize]
        public LOV GetLOV(int id)
        {
            var items = db.LOVs;
            LOV item = items.Where(p => p.LOVID == id).FirstOrDefault();
            if (item == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return item;
        }

        // PUT api/LOV/5
        [Authorize]
        public HttpResponseMessage PutLOV(int id, LOV item)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != item.LOVID)
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

        // POST api/LOV
        [Authorize]
        public HttpResponseMessage PostLOV(LOV item)
        {
            if (ModelState.IsValid)
            {
                db.LOVs.Add(item);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, item);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = item.LOVID }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/LOV/5
        [Authorize]
        public HttpResponseMessage DeleteLOV(int id)
        {
            LOV item = db.LOVs.Find(id);
            if (item == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.LOVs.Remove(item);

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