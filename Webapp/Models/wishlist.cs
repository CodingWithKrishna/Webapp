using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HSBCReward.Models
{
    public class wishlist
    {
        public int id { get; set; }
        public Nullable<int> product_id { get; set; }
        public Nullable<int> user_id { get; set; }

        public virtual product product { get; set; }
        public virtual user user { get; set; }
    }
}