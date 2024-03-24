using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HSBCReward.Models
{
    public class order
    {
        public int id { get; set; }
        public Nullable<int> txn_id { get; set; }
        public int points_value { get; set; }
        public string status { get; set; }
        public Nullable<System.DateTime> created_at { get; set; }
        public Nullable<int> user_id { get; set; }
        public string client_product_code { get; set; }
       
        [ForeignKey("user_id")]
        public virtual user user { get; set; }
    }
}