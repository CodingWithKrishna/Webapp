using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HSBCReward.Models
{
    public class AdminOrderViewModel
    {
        public int id { get; set; }
        public Nullable<int> txn_id { get; set; }
        public string status { get; set; }
        public string global_code { get; set; }
        public string remarks { get; set; }

    }
}