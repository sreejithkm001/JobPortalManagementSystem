using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace JobPortal.Models
{
    public class UserMV
    {
        public UserMV()
        {
            Company = new CompanyMV();
        }
        public int UserID { get; set; }
        public int UserTypeID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        [NotMapped]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        public string EmailAddress { get; set; }
        public string ContactNo { get; set; }

        [NotMapped]
        public HttpPostedFileBase imageFile { get; set; }
        [NotMapped]
        public HttpPostedFileBase FileUpload { get; set; }

        public bool AreYouProvider { get; set; }
        public CompanyMV Company { get; set; }
    }
}