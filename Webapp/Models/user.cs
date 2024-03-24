using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HSBCReward.Models
{
    public class user
    {
        public int id { get; set; }
        public string uid { get; set; }

        public virtual ICollection<cart> carts { get; set; }
        public virtual ICollection<order> orders { get; set; }
        public virtual ICollection<wishlist> wishlists { get; set; }
    }
}