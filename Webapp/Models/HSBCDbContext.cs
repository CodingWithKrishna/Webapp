using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace HSBCReward.Models
{
    public class HSBCRewardDbContext : DbContext
    {
       
        public HSBCRewardDbContext() : base("name=HSBCConn")
        {

        }
     
        public virtual DbSet<product> Products { get; set; }

        public virtual DbSet<cart> cart { get; set; }

        public virtual DbSet<user> users { get; set; }

        public virtual DbSet<userlogin> userlogins { get; set; }

        public virtual DbSet<order> orders { get; set; }
        public virtual DbSet<order_contents> order_contents { get; set; }
        public virtual DbSet<order_contents_n> order_contents_n { get; set; }

        public virtual DbSet<AdminUser> AdminUsers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<order>()
                .Property(o => o.txn_id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
        }
    }
}