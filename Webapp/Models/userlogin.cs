using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HSBCReward.Models
{
    public class userlogin
    {
        [Key]
        public int id { get; set; }
        public Nullable<Guid> Guid { get; set; }
        public string UID { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
    }
}