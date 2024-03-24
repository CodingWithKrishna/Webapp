
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HSBCReward.AppCode.BAL;
using HSBCReward.Models;



namespace HSBCReward.Controllers
{
    public class CatalogueController : Controller
    {

        private readonly ProductService _productService;
        private readonly CartService _cartService;
        private readonly LoginService _loginService;


        public CatalogueController()
        {
            _cartService = new CartService();
            _productService = new ProductService(); // Instantiate the ProductService
            _loginService = new LoginService();
        }



        public ActionResult Index(string customerType)
        {
            try
            {

                string userGuid = Session["EncryptedUserGuid"] as string;
                string userUid = _loginService.GetUserUid(userGuid);
                Session["CartItemCount"] = _cartService.getCartCount(userUid);
                Session["CustomerType"] = customerType;
                if (string.IsNullOrEmpty(customerType))
                {
                    customerType = Session["CustomerType"] as string;
                    // Your code here if customerType is null or empty
                }
                List<ProductListViewModel> productList = _productService.GetProductList(customerType);

                if (productList != null)
                {
                    
                    return View(productList);

                }
                else
                {
                    return Json(new { message = "No products found." });
                }
            }
            catch (ArgumentException )
            {

                return RedirectToAction("Error", "Home");
            }

            
        }

        [HttpPost]
        public ActionResult FilterProduct(string minPoint, string maxPoint, string category, List<string> subCategory, List<string> brand, string search)
        {
            try
            {
                string customerType = Session["CustomerType"] as string;

             var  model = _productService.GetFilterProductList(customerType, minPoint, maxPoint, category, subCategory, brand, search);
                return PartialView(model);
            }
            catch (ArgumentException ) 
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        public ActionResult GetProductDetails(string id)
        {
            try
            {
                var productDetail = _productService.product(id);
                var productDetails = new
                {
                    productName = productDetail.product_name,
                    productImage = productDetail.images,
                    productDescription = productDetail.descriptive,
                    productPrice = productDetail.final_landed_price,
                    productTerms = productDetail.terms,
                    productBrand =productDetail.brand_name,
                    productId=productDetail.id
                };

                // Return product details as JSON
                return Json(productDetails, JsonRequestBehavior.AllowGet);
            }
            catch (ArgumentException)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public ActionResult AddToCart(string pId, int quantity)
        {
            try
            {
                string userGuid = Session["EncryptedUserGuid"] as string;
                string userUid = _loginService.GetUserUid(userGuid);
                int productId = Convert.ToInt32(pId);
                bool result = _productService.AddToCart(productId, quantity, userUid);
                return Json(new { success = true, message = "Product added to cart successfully" });
            }
            catch (ArgumentException)
            {
                return RedirectToAction("Error", "Home");
            }
        }


    }
}