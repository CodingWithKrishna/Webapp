using HSBCReward.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace HSBCReward.AppCode.BAL
{
    public class CartService
    {

        private HSBCRewardDbContext _dbContext = new HSBCRewardDbContext();

        public List<product> getCart(int userId)
        {
          

            var cartItems = _dbContext.cart
                                .Where(x => x.user_id == userId)
                                .Select(x => new { ProductId = x.product_id, Quantity = x.quantity, Id = x.id })
                                .ToList();

            var productsInCart = (from c in cartItems
                                  join p in _dbContext.Products on c.ProductId equals p.id
                                  select new product
                                  {
                                      id = p.id,
                                      images = p.images,
                                      global_code = p.global_code,
                                      brand_name = p.brand_name,
                                      quantity = c.Quantity,
                                      final_landed_price = p.final_landed_price,
                                      terms = c.Id.ToString()

                                  }).ToList();


            return productsInCart;

        }

        public bool deleteProduct(int productId)
        {
            try
            {
                if (productId != 0)
                {
                    var product = _dbContext.cart.FirstOrDefault(x => x.id == productId);
                    _dbContext.cart.Remove(product);
                    _dbContext.SaveChanges();
                    return true;
                }
                else { return false; }
            }
            catch (Exception ex)
            {
                throw;
               
            }


        }

        public bool updateProductQty(int productId,int quantity)
        {
            try
            {
                var product = _dbContext.cart.FirstOrDefault(x => x.product_id == productId);
                if (product != null)
                {
                    // Update the quantity column
                    product.quantity = quantity; // Assuming newQuantity is the new quantity value you want to set

                    // Save changes to the database
                    _dbContext.SaveChanges();
                    return true; // Indicates that the update was successful
                }
                else
                {
                    // Handle the case where no product with the given productId was found in the cart
                    return false; // Indicates that the update was unsuccessful
                }

            }
            catch (Exception ex) {
                throw;
            
            }

            
        }

        public int getCartCount(string Uid)
        {

          
            var id = _dbContext.users.FirstOrDefault(x => x.uid.Equals(Uid)).id;

            var count = _dbContext.cart.Count(x => x.user_id == id);

            return count;
        }

        public List<OrderHistoryModel> orderHistory(int Uid)
        {
            List<OrderHistoryModel> orderHistoryModels = new List<OrderHistoryModel>();

            List<order_contents> ore = _dbContext.order_contents.ToList();
            List<order> orderhistory = _dbContext.orders.Where(x => x.user_id == Uid).ToList();
            foreach (var order in orderhistory)
            {
                OrderHistoryModel orderHistoryModel = new OrderHistoryModel();
                order_contents order_content = _dbContext.order_contents.FirstOrDefault(x => x.txn_id == order.txn_id);

                orderHistoryModel.txn_id = order.txn_id;
                orderHistoryModel.points_value = order.points_value;
                orderHistoryModel.status = order.status;
                orderHistoryModel.created_at = order.created_at;
                orderHistoryModel.quantity = order_content?.quantity ?? 0;
                if (order_content != null)
                {
                    orderHistoryModel.brand_name = _dbContext.Products.FirstOrDefault(x => x.id == order_content.product_id)?.brand_name;
                    orderHistoryModel.final_landed_price = Convert.ToInt32(_dbContext.Products.FirstOrDefault(x => x.id == order_content.product_id)?.final_landed_price);
                }

                orderHistoryModels.Add(orderHistoryModel);
            }



            return orderHistoryModels;
        }



        public OrderHistoryModel getorderdetailspopup(string txn)
        {

            int itxn = Convert.ToInt32(txn);

            OrderHistoryModel orderHistoryModels = new OrderHistoryModel();

            order order = _dbContext.orders.FirstOrDefault(x => x.txn_id == itxn);
            order_contents ordercontent = _dbContext.order_contents.FirstOrDefault(x => x.txn_id == itxn);

            orderHistoryModels.txn_id = itxn;
            orderHistoryModels.status = order.status;
            orderHistoryModels.created_at = order.created_at;
            orderHistoryModels.quantity = ordercontent.quantity;
            orderHistoryModels.client_product_code = Convert.ToString(ordercontent.product_id);
            orderHistoryModels.final_landed_price = Convert.ToInt32(_dbContext.Products.FirstOrDefault(x => x.id == ordercontent.product_id)?.final_landed_price);



            return orderHistoryModels;
        }

    }
}