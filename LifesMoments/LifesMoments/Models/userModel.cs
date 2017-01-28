using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifesMoments.Models
{
    public class userModel
    {
        public int userID { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        public string emailAddress { get; set; }

        public string userPassword { get; set; }
    }
}