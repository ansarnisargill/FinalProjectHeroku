using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class Notification
    {
        public int ID { get; set; }
        public string Link { get; set; }
        public string Text { get; set; }
        public int UserID { get; set; }
        public bool Status { get; set; }
        public string Type { get; set; }

    }
}