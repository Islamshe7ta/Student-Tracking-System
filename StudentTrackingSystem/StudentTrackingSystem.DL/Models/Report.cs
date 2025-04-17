using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace StudentTrackingSystem.DAL.Models
{
    class Report
    {
        
        public int ReportId { get; set; }
        public string ReportName { get; set; }
        public string ReportDescription { get; set; }
        public DateTime ReportDate { get; set; }
        public DateTime ReportDateEnd { get; set; }


    }
}
