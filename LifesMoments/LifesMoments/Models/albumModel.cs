using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifesMoments.Models
{
    public class albumModel
    {
        public int albumID { get; set; }

        public int userIDFK { get; set; }

        public string albumDescription { get; set; }

        public DateTime albumDate { get; set; }
        
    }
}