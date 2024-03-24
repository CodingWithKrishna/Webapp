using CsvHelper;
using CsvHelper.Configuration;
using HSBCReward.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace HSBCReward.AppCode.BAL
{
    public class AdminService
    {
        private HSBCRewardDbContext _dbContext = new HSBCRewardDbContext();

        public bool updateProduct(string productId, string points,string productName, string availability)
        {
            int Id = Convert.ToInt32(productId);
            product dbproduct = _dbContext.Products.FirstOrDefault(x => x.id == Id);
            dbproduct.final_landed_price = points;
            dbproduct.product_name = productName;
            if(availability =="0")
            {
                dbproduct.status = null;

            }
            else
            {
                dbproduct.status = "NA";
            }
          

            _dbContext.Products.AddOrUpdate(dbproduct);
            _dbContext.SaveChanges();

            return true;
        }
        public List<product> getproducts(string filter)
        {

           

            List<product> products = _dbContext.Products.ToList();

            if (!string.IsNullOrEmpty(filter))
            {

                if (filter == "Available")
                {
                    products = products
             .Where(x => x.status == null)
             .ToList();
                }
                else
                {
                    products = products
 .Where(x => x.status == "NA").ToList();
                }

            }

            return products;
        }


       

        public List<AdminOrderViewModel> getorder(string fromDate, string toDate)
        {


            if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
            {
                DateTime fromDt = Convert.ToDateTime(fromDate);
                DateTime toDt = Convert.ToDateTime(toDate);

                // LINQ query to select specific columns from both tables and apply a condition
                var query = from order in _dbContext.orders
                            join orderContent in _dbContext.order_contents_n
                            on order.txn_id equals orderContent.txn_id
                            where order.created_at >= fromDt && order.created_at <= toDt
                            select new AdminOrderViewModel
                            {
                                // Selecting specific columns from both tables
                                id = order.id,
                                global_code = orderContent.global_code,
                                status = orderContent.status,
                                remarks = orderContent.remarks
                                // Add more properties as needed
                            };

                // Execute the query and materialize the results into a list
                List<AdminOrderViewModel> orders = query.ToList();

                
                return orders;
            }
            else if (string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))

            {

                DateTime toDt = Convert.ToDateTime(toDate);

                    // LINQ query to select specific columns from both tables and apply a condition
                    var query = from order in _dbContext.orders
                                join orderContent in _dbContext.order_contents_n
                                on order.txn_id equals orderContent.txn_id
                                where order.created_at <= toDt
                                select new AdminOrderViewModel
                                {
                                    // Selecting specific columns from both tables
                                    id = order.id,
                                    global_code = orderContent.global_code,
                                    status = orderContent.status,
                                    remarks = orderContent.remarks
                                    // Add more properties as needed
                                };

                    // Execute the query and materialize the results into a list
                    List<AdminOrderViewModel> orders = query.ToList();


                    return orders;
                
            }

            else if (!string.IsNullOrEmpty(fromDate) && string.IsNullOrEmpty(toDate))
            {
                DateTime fromDt = Convert.ToDateTime(fromDate);

                // LINQ query to select specific columns from both tables and apply a condition
                var query = from order in _dbContext.orders
                            join orderContent in _dbContext.order_contents_n
                            on order.txn_id equals orderContent.txn_id
                            where order.created_at >= fromDt
                            select new AdminOrderViewModel
                            {
                                // Selecting specific columns from both tables
                                id = order.id,
                                global_code = orderContent.global_code,
                                status = orderContent.status,
                                remarks = orderContent.remarks
                                // Add more properties as needed
                            };

                // Execute the query and materialize the results into a list
                List<AdminOrderViewModel> orders = query.ToList();


                return orders;
            }
            else 
            {
                var query = from order in _dbContext.orders
                            join orderContent in _dbContext.order_contents_n
                            on order.txn_id equals orderContent.txn_id
                            
                            select new AdminOrderViewModel
                            {
                                // Selecting specific columns from both tables
                                id = order.id,
                                global_code = orderContent.global_code,
                                status = orderContent.status,
                                remarks = orderContent.remarks
                                // Add more properties as needed
                            };
                List<AdminOrderViewModel> orders = query.ToList();
                return orders;

            }

        }

        public bool AuthenticateUser(string email, string password)
        {
            FormsAuthentication.SetAuthCookie(email, true);
            return true;
        }
        


             public MemoryStream getProductCsv()
        {
            MemoryStream memorystream = new MemoryStream();
            StreamWriter streamwriter = new StreamWriter(memorystream);

            CsvConfiguration csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture);
            CsvWriter csvWriter = new CsvWriter(streamwriter, csvConfiguration);

            csvWriter.WriteField("Id");
            csvWriter.WriteField("Product Name");
            csvWriter.WriteField("Client Product");
            csvWriter.WriteField("Global Code");
            csvWriter.WriteField("Status");

            csvWriter.NextRecord();

            int batchSize = 1000; // Adjust the batch size as needed
            int skip = 0;
            List<product> productRecords;

            do
            {
                // Retrieve a batch of orders from the database
                productRecords = _dbContext.Products.OrderBy(o => o.id).Skip(skip).Take(batchSize).ToList();

                foreach (var product in productRecords)
                {
                    csvWriter.WriteField(product.id);
                    csvWriter.WriteField(product.product_name);
                    csvWriter.WriteField(product.client_product_category_code);
                    csvWriter.WriteField(product.global_code);
                    csvWriter.WriteField(product.status);

                    csvWriter.NextRecord();
                }

                csvWriter.Flush();
                skip += batchSize;
            }
            while (productRecords.Count == batchSize);

            memorystream.Position = 0;
            return memorystream;
        }

        public MemoryStream GetOrderCsv()
        {
            MemoryStream memorystream = new MemoryStream();
            StreamWriter streamwriter = new StreamWriter(memorystream);

            CsvConfiguration csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture);
            CsvWriter csvWriter = new CsvWriter(streamwriter, csvConfiguration);

            csvWriter.WriteField("Deal");
            csvWriter.WriteField("Product Code");
            csvWriter.WriteField("Status");
            csvWriter.WriteField("Remarks");
            csvWriter.NextRecord();

            int batchSize = 1000; // Adjust the batch size as needed
            int skip = 0;
            List<order_contents_n> orderRecords;

            do
            {
                // Retrieve a batch of orders from the database
                orderRecords = _dbContext.order_contents_n.OrderBy(o => o.id).Skip(skip).Take(batchSize).ToList();

                foreach (var order in orderRecords)
                {
                    csvWriter.WriteField(order.id);
                    csvWriter.WriteField(order.global_code);
                    csvWriter.WriteField(order.status);
                    csvWriter.WriteField(order.remarks);
                    csvWriter.NextRecord();
                }

                csvWriter.Flush();
                skip += batchSize;
            }
            while (orderRecords.Count == batchSize);

            memorystream.Position = 0;
            return memorystream;
        }




        public bool uploadorderdata(HttpPostedFileBase file)
        {
            try
            {
                bool isFirstLine = true; // Flag to skip the header row

                using (var reader = new StreamReader(file.InputStream))
                {


                    while (!reader.EndOfStream)
                    {


                        var line = reader.ReadLine();


                        if (isFirstLine)
                        {
                            isFirstLine = false;
                            continue;
                        }
                        var values = line.Split(',');

                        int id = int.Parse(values[0]);
                        string globalcode = values[1];
                      string status = values[2];
                        string remark = values[3];
                        //Map other properties as needed


                        // Assuming your CSV structure matches the model

                        var orderContentToUpdate = (from orderContent in _dbContext.order_contents_n
                                                    join order in _dbContext.orders
                                                    on orderContent.txn_id equals order.txn_id
                                                    where order.id == id && orderContent.global_code == globalcode
                                                    select orderContent).FirstOrDefault();

                        if (orderContentToUpdate != null)
                        {
                            // Update the status and remark properties of the order_contents_n record
                            orderContentToUpdate.status = status.Trim();
                            orderContentToUpdate.remarks = remark.Trim();
                            _dbContext.SaveChanges();
                        }

                    }

                    }

                    return true;

                
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool uploadproductdata(HttpPostedFileBase file)
        {
            try
            {
                using (var reader = new StreamReader(file.InputStream))
                {

                    bool isFirstLine = true; // Flag to skip the header row

                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        if (isFirstLine)
                        {
                            isFirstLine = false;
                            continue;
                        }
                        var values = line.Split(',');

                        int id = int.Parse(values[0]);
                        string product_name = values[1];
                        string client_product_code = values[2];
                        string globalCode = values[3];
                        string status = values[4];
                                             product ProductContentToUpdate = _dbContext.Products.FirstOrDefault(x => x.id == id);

                        if (ProductContentToUpdate != null)
                        {
                            // Update the status and remark properties of the order_contents_n record
                            ProductContentToUpdate.global_code = globalCode.Trim();
                            if (status.ToUpper() == "UNMASK")
                            {
                                ProductContentToUpdate.status = null;

                            }
                            else if(status.ToUpper()=="MASK")
                            {
                                ProductContentToUpdate.status = "NA";

                            }
                            _dbContext.SaveChanges();
                        }

                    }

                }

                return true;


            }
            catch (Exception)
            {
                return false;
            }

        }

        public product getProductPopup(string productId)
        {
            int id = Convert.ToInt32(productId);
            product product = _dbContext.Products.FirstOrDefault(x => x.id == id);

            return product;
        }

        public order_contents_n getOrderPopup(string productId)
        {
            int id = Convert.ToInt32(productId);
            order_contents_n product = _dbContext.order_contents_n.FirstOrDefault(x => x.id == id);

            return product;
        }
        public order_contents_n getOrderById(string Id)
        {
            int id = 0;
            if (!string.IsNullOrEmpty(Id))
            {
                id = Convert.ToInt32(Id);
            }
            var product = _dbContext.order_contents_n.FirstOrDefault(x => x.id == id);
            return product;
        }
        public bool updateOrder(int Id,string status,string remark)
        {
            if(Id != 0)
            {
                var product = _dbContext.order_contents_n.FirstOrDefault(x => x.id == Id);
                if (product != null)
                {
                    product.status = status;
                    product.remarks = remark;
                    _dbContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }


            }

            return true;
        }
    }
}