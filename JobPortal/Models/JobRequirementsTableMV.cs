using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobPortal.Models
{
    public class JobRequirementsTableMV
    {
        public JobRequirementsTableMV()
        {
            Details=new List<JobRequirementDetailMV> ();
        }
        public int JobRequirementID { get; set; }
        public string JobRequirementTitle { get; set; }

        public List<JobRequirementDetailMV> Details {get; set; }
    }
}