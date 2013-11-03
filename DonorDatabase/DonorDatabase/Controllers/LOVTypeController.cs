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
    public class LOVTypeController : ApiController
    {
        private donordatabaseEntities db = new donordatabaseEntities();

        // GET api/LOVType
        [Authorize]
        public IEnumerable<LOVType> GetLOVType()
        {
            return db.LOVTypes.AsEnumerable();
        }

        // GET api/LOVType/5
        public LOVType GetLOVType(int id)
        {
            var items = db.LOVTypes.Include(p => p.LOVs);
            LOVType item = items.Where(p => p.LOVTypeID == id).FirstOrDefault();
            if (item == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return item;
        }

        // PUT api/LOVType/5
        [Authorize]
        public HttpResponseMessage PutLOVType(int id, LOVType item)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != item.LOVTypeID)
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

        // POST api/LOVType
        [Authorize]
        public HttpResponseMessage PostLOVType(LOVType item)
        {
            if (ModelState.IsValid)
            {
                db.LOVTypes.Add(item);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, item);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = item.LOVTypeID }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/LOVType/5
        [Authorize]
        public HttpResponseMessage DeleteLOVType(int id)
        {
            LOVType item = db.LOVTypes.Find(id);
            if (item == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.LOVTypes.Remove(item);

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