using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JobPortal.Models
{
    public class ForgotPasswordMV
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}