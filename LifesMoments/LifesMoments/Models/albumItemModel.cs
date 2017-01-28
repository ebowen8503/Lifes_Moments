using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifesMoments.Models
{
    public class albumItemModel
    {
        public int albumItemID { get; set; }

        public int albumIDFK { get; set; }

        public albumModel album { get; set; }

        public string albumItemDescription { get; set; }

        public string albumItemLocation { get; set; }

        public string mediaTypeDescription { get; set; }
    }
}