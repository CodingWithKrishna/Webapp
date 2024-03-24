using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HSBCReward.Models
{
    public class OrderHistoryModel
    {
        public int id { get; set; }
        public Nullable<int> txn_id { get; set; }
        public int points_value { get; set; }
        public string status { get; set; }
        public Nullable<System.DateTime> created_at { get; set; }
        public Nullable<int> user_id { get; set; }
        public string client_product_code { get; set; }

        public int? quantity { get; set; }

        public string brand_name { get; set; }

        public int final_landed_price { get; set; }

    }
}