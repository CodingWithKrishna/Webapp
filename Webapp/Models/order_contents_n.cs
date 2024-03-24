using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HSBCReward.Models
{
    public class order_contents_n
    {
        public int id { get; set; }
        public Nullable<int> txn_id { get; set; }
        public Nullable<int> product_id { get; set; }
        public int quantity { get; set; }
        public string action_required { get; set; }
        public string age_group { get; set; }
        public string assembly_required { get; set; }
        public string basic_cost { get; set; }
        public string basic_courier_cost { get; set; }
        public string basic_insurance_amount { get; set; }
        public string basic_packaging_cost { get; set; }
        public string brand_name { get; set; }
        public string capacity { get; set; }
        public string category { get; set; }
        public string @class { get; set; }
        public string colour { get; set; }
        public string courier_cost_incl_gst { get; set; }
        public string date_time_of_entry { get; set; }
        public string date_of_import_stock_hitting_vendor_warehouse { get; set; }
        public string delivery_type { get; set; }
        public string descriptive { get; set; }
        public string details_of_UoM { get; set; }
        public string discount_on_FLP { get; set; }
        public string discount_on_MRP { get; set; }
        public string discount_on_MRP_Basic { get; set; }
        public string discount_on_offer_price { get; set; }
        public string discount_on_revised_buying { get; set; }
        public string final_landed_price { get; set; }
        public string gender { get; set; }
        public string global_code { get; set; }
        public string gst_percent { get; set; }
        public string gst_percent_on_services_rendered { get; set; }
        public string gst_amount { get; set; }
        public string gst_amount_on_basic_packaging_cost { get; set; }
        public string gst_amount_on_insurance { get; set; }
        public string gst_on_margin { get; set; }
        public string hsn_code { get; set; }
        public string import_frequency { get; set; }
        public string insurance_percent { get; set; }
        public string insurance_amount { get; set; }
        public string item_type { get; set; }
        public string make { get; set; }
        public string margin_percent { get; set; }
        public string margin_amount { get; set; }
        public string material { get; set; }
        public string merchandise_seasonality { get; set; }
        public string mfg_year { get; set; }
        public string model_no { get; set; }
        public string mrp { get; set; }
        public string name_of_alternate_vendor { get; set; }
        public string net_landed_cost { get; set; }
        public string offer_price { get; set; }
        public string orc_details { get; set; }
        public string packaging_cost_incl_gst { get; set; }
        public string processing_cost { get; set; }
        public string product_dimensions_actual { get; set; }
        public string product_dimensions_outerbox { get; set; }
        public string product_gross_weight { get; set; }
        public string product_name { get; set; }
        public string product_name_for_catalogue { get; set; }
        public string product_name_for_master_records { get; set; }
        public string product_net_weight { get; set; }
        public string quantity_in_import { get; set; }
        public string reserve_percent { get; set; }
        public string reserve_amount { get; set; }
        public string revised_buying_inc_gst { get; set; }
        public string size { get; set; }
        public string size_chart { get; set; }
        public string sku_avail_status { get; set; }
        public string sku_creation_dt { get; set; }
        public string sku_newness { get; set; }
        public string sku_short_description { get; set; }
        public string special_basic_cost { get; set; }
        public string stock_level { get; set; }
        public string stock_supply_validity { get; set; }
        public string style { get; set; }
        public string sub_category { get; set; }
        public string sub_class { get; set; }
        public string unit_of_measure { get; set; }
        public string validity_for_spl_basic_cost { get; set; }
        public string validity_in_days { get; set; }
        public string validity_left { get; set; }
        public string validity_left_for_spl_basic_cost { get; set; }
        public string vendor_code { get; set; }
        public string vendor_lead { get; set; }
        public string vendor_name { get; set; }
        public string vendor_sku_code { get; set; }
        public string status { get; set; }
        public string images { get; set; }
        public string terms { get; set; }
        public string client_product_category_code { get; set; }
        public string remarks { get; set; }

    }
}