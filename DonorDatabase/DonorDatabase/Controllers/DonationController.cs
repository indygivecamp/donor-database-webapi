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
    public class DonationController : ApiController
    {
        private donordatabaseEntities db = new donordatabaseEntities();

        // GET api/Donation
        [Authorize]
        public IEnumerable<Donation> GetDonation()
        {
            return db.Donations.AsEnumerable();
        }

        // GET api/Donation/5
        [Authorize]
        public Donation GetDonation(int id)
        {
            var items = db.Donations.Include(p => p.LOV_Source);
            Donation item = items.Where(p => p.DonationID == id).FirstOrDefault();
            if (item == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return item;
        }

        // PUT api/Donation/5
        [Authorize]
        public HttpResponseMessage PutDonation(int id, Donation item)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != item.DonationID)
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

        // POST api/Donation
        [Authorize]
        public HttpResponseMessage PostDonation(Donation item)
        {
            if (ModelState.IsValid)
            {
                db.Donations.Add(item);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, item);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = item.DonationID }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Donation/5
        [Authorize]
        public HttpResponseMessage DeleteDonation(int id)
        {
            Donation item = db.Donations.Find(id);
            if (item == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Donations.Remove(item);

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