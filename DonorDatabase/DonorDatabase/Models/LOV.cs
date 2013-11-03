//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DonorDatabase.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class LOV
    {
        public LOV()
        {
            this.Contacts = new HashSet<Contact>();
            this.Contacts1 = new HashSet<Contact>();
            this.Contacts2 = new HashSet<Contact>();
            this.Donations = new HashSet<Donation>();
            this.Interests = new HashSet<Interest>();
            this.People = new HashSet<Person>();
            this.People1 = new HashSet<Person>();
            this.People2 = new HashSet<Person>();
            this.People3 = new HashSet<Person>();
            this.People4 = new HashSet<Person>();
            this.People5 = new HashSet<Person>();
        }
    
        public int LOVID { get; set; }
        public int LOVTypeID { get; set; }
        public string Name { get; set; }
        public int DisplayOrder { get; set; }
        public bool Active { get; set; }
    
        private ICollection<Contact> Contacts { get; set; }
        private ICollection<Contact> Contacts1 { get; set; }
        private ICollection<Contact> Contacts2 { get; set; }
        private ICollection<Donation> Donations { get; set; }
        private ICollection<Interest> Interests { get; set; }
        private LOVType LOVType { get; set; }
        private ICollection<Person> People { get; set; }
        private ICollection<Person> People1 { get; set; }
        private ICollection<Person> People2 { get; set; }
        private ICollection<Person> People3 { get; set; }
        private ICollection<Person> People4 { get; set; }
        private ICollection<Person> People5 { get; set; }
    }
}