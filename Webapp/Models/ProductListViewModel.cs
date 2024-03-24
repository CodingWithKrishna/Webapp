using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HSBCReward.Models
{
    public class ProductListViewModel
    {

        public List<string> Categories { get; set; }
        public List<string> Subcategories { get; set; }
        public List<string> Brands { get; set; }
        public List<product> Products { get; set; }
        public string MaximumPrizeRange {  get; set; }

    }
}