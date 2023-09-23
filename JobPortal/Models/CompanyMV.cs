using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobPortal.Models
{
    public class CompanyMV
    {
        public int CompanyID { get; set; }
        public int UserID { get; set; }
        public string CompanyName { get; set; }
        public string ContactNo { get; set; }
        public string PhoneNo { get; set; }
        public string EmailAddress { get; set; }
        public string Logo { get; set; }
        public string Description { get; set; }

    }
}