using HSBCReward.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HSBCReward.AppCode.BAL
{
    public class ProductService
    {
        private HSBCRewardDbContext _dbContext = new HSBCRewardDbContext();
        private readonly CartService _cartService = new CartService();
        private readonly LoginService _loginService = new LoginService();

        public List<ProductListViewModel> GetProductList(string clientProductCategoryCode)
        {
            try
            {
                if (string.IsNullOrEmpty(clientProductCategoryCode))
                {
                    throw new ArgumentException("clientProductCategoryCode cannot be null or empty.");

                }

                var productInfo = new ProductListViewModel();

                // Fetch distinct values for category, subcategory, and brand
                productInfo.Categories = _dbContext.Database.SqlQuery<string>(
                       "EXEC GetProductList @action",
                      new SqlParameter("@action", "Category")
                       ).ToList();

                // Fetch subcategories
                productInfo.Subcategories = _dbContext.Database.SqlQuery<string>(
                    "EXEC GetProductList @action",
                    new SqlParameter("@action", "Subcategory")
                ).ToList();

                // Fetch brands
                productInfo.Brands = _dbContext.Database.SqlQuery<string>(
                    "EXEC GetProductList @action",
                    new SqlParameter("@action", "Brand")
                ).ToList();

                // Fetch maximum prize range
                productInfo.MaximumPrizeRange = _dbContext.Database.SqlQuery<string>(
                    "EXEC GetProductList @action",
                    new SqlParameter("@action", "MaximumPrizeRange")
                ).FirstOrDefault();

                // Fetch products
                productInfo.Products = _dbContext.Database.SqlQuery<product>(
                    "EXEC GetProductList @action, @client_product_category_code",
                    new SqlParameter("@action", "Products"),
                    new SqlParameter("@client_product_category_code", clientProductCategoryCode)
                ).ToList();

             



                return new List<ProductListViewModel> { productInfo };
            }
            catch (Exception ex)
            {
                // Handle exceptions
                // For simplicity, rethrowing the exception
                throw ex;
            }
        }

        public ProductFilterViewModel GetFilterProductList(string clientProductCategoryCode, string minPoint, string maxPoint, string category, List<string> subCategory, List<string> brand, string searchValue)
        {
            try
            {

                if (string.IsNullOrEmpty(clientProductCategoryCode))
                {
                    throw new ArgumentException("clientProductCategoryCode cannot be null or empty.");

                }
                var productInfo = new ProductFilterViewModel();



                // Fetch products
                var productsFromDatabase = _dbContext.Database.SqlQuery<product>(
                    "EXEC GetProductList @action, @client_product_category_code",
                    new SqlParameter("@action", "Products"),
                    new SqlParameter("@client_product_category_code", clientProductCategoryCode)
                ).ToList();

                if (string.IsNullOrEmpty(minPoint))
                {
                    minPoint = "0";
                }

                if (string.IsNullOrEmpty(minPoint) && string.IsNullOrEmpty(maxPoint))
                {
                    throw new ArgumentException("");


                }
                var productsInRange = productsFromDatabase
                      .Where(x => Convert.ToDecimal(x.final_landed_price) >= Convert.ToDecimal(minPoint)
                               && Convert.ToDecimal(x.final_landed_price) <= Convert.ToDecimal(maxPoint))
                      .ToList();
                
                // Filter products based on the provided category if it's not null
                if (!string.IsNullOrEmpty(category))
                {
                    productsInRange = productsInRange
                        .Where(x => x.category.Equals(category))
                        .ToList();
                }

                // Filter products based on the provided subcategories if they are not null
                if (subCategory != null && subCategory.Any())
                {
                    productsInRange = productsInRange
                        .Where(x => subCategory.Contains(x.sub_category))
                        .ToList();
                }

                // Filter products based on the provided brands if they are not null
                if (brand != null && brand.Any())
                {
                    productsInRange = productsInRange
                        .Where(x => brand.Contains(x.brand_name))
                        .ToList();
                }
                if (!string.IsNullOrEmpty(searchValue))
                {
                    productsInRange = productsInRange
                                      .Where(p => p.vendor_name.ToUpper().Contains(searchValue.ToUpper()) || p.final_landed_price.ToUpper().Contains(searchValue.ToUpper())).ToList();
                }

                productInfo.Products = productsInRange;


                return  productInfo ;




            }
            catch (Exception ex)
            {
                // Handle exceptions
                // For simplicity, rethrowing the exception
                throw ex;
            }
        }

        public product product(string Id)
        {
            int id=0;
            if (!string.IsNullOrEmpty(Id))
            {
                id = Convert.ToInt32(Id);
            }
            var product = _dbContext.Products.FirstOrDefault(x=> x.id == id);
            return product;
        }

        public bool AddToCart(int productId, int quantity, string userId)
        {
            int uid = _dbContext.users.FirstOrDefault(x => x.uid.Equals(userId)).id;
            cart ccart = new cart();
            ccart.product_id = productId;
            ccart.quantity = quantity;
            ccart.user_id = uid;
            _dbContext.cart.Add(ccart);
            _dbContext.SaveChanges();
            return true;
        }


        public void CompleteOrder(string userUid)
        {
            try
            {

                int id = _loginService.GetUserId(userUid);
                List<product> productcart = _cartService.getCart(id);

                foreach (var product in productcart)
                {
                    // Call the stored procedure to save the product
                    _dbContext.Database.ExecuteSqlCommand(
                        "EXEC SaveOrder @product_id, @quantity, @user_id,@points_value, @client_product_code,@status",
                        new SqlParameter("@product_id", product.id),
                        new SqlParameter("@quantity", product.quantity),
                        new SqlParameter("@user_id", id),
                        new SqlParameter("@points_value", "2000"),
                        new SqlParameter("@client_product_code", "HPR"),
                        new SqlParameter("@status", "CONFIRMED")

                    );
                }




            }
            catch (Exception ex)
            {
                // Handle exceptions
                // For simplicity, rethrowing the exception
                throw ex;
            }
        }
    }
}