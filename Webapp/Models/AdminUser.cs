using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HSBCReward.Models
{
    public class AdminUser
    {
        [Key]
        public int UserId { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }

    }
}