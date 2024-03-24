using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HSBCReward.Models
{
    [Table("cart")]
    public class cart
    {
        public int id { get; set; }
        public Nullable<int> product_id { get; set; }
        public int quantity { get; set; }
        public Nullable<int> user_id { get; set; }

        [ForeignKey("user_id")]
        public virtual user user { get; set; }
    }
}