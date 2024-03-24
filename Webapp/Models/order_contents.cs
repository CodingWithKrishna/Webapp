using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HSBCReward.Models
{
    public class order_contents
    {
        public int id { get; set; }
        public int txn_id { get; set; }
        public int product_id { get; set; }
        public int quantity { get; set; }
    }
}