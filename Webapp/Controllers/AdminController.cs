using HSBCReward.AppCode.BAL;
using HSBCReward.AppCode.Helpers;
using HSBCReward.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;

namespace HSBCReward.Controllers
{
    public class AdminController : Controller
    {
        private readonly LoginService _loginService;
        private readonly AdminService _adminLoginService;

      
        public AdminController()
        {
            _loginService = new LoginService();
            _adminLoginService = new AdminService();

        }
        // GET: AdminLogin
        public ActionResult Index()
        {
            return View();
        }
      

        [HttpPost]
        public ActionResult Index(AdminLogin loginDTO)
        {
            if (ModelState.IsValid)
            {
                bool IsUserValid = _loginService.ValidateAdminUser(loginDTO.Email, loginDTO.Password);
                if (IsUserValid == true)
                {
                    return RedirectToAction("Order", "Admin");                   
                }
                else
                {
                    // User not found or inactive
                    ViewBag.ErrorMessage = "Invalid email or password";
                    return View("Index");
                }
               
            }
            else
            {
                return View(loginDTO);
            }


        }


        [HttpGet]
        public ActionResult GetOrderById(string id)
        {
            try
            {
                var productDetail = _adminLoginService.getOrderById(id);
                var AdminOrderViewModel = new
                {
                    productImage = productDetail.images,
                    productName = productDetail.product_name,
                    productId = productDetail.id,
                    productQty=productDetail.quantity_in_import,
                    productStatus = productDetail.status,
                    productRemark =productDetail.remarks
                };

                // Return product details as JSON
                return Json(productDetail, JsonRequestBehavior.AllowGet);
            }
            catch (ArgumentException)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public ActionResult updateProduct(string productId, string points,string productName, string availability)
        {

            bool Sucess = _adminLoginService.updateProduct(productId, points, productName, availability);
            return RedirectToAction("productCatalogue");
        }

        public ActionResult updateOrder(string productId, string status, string remark)
        {
            int id = Convert.ToInt32(productId);    
            bool Sucess = _adminLoginService.updateOrder(id, status, remark);
            return RedirectToAction("Order");
        }


        public ActionResult Order(string fromDate, string toDate)
        {
            List<AdminOrderViewModel> orderList = _adminLoginService.getorder(fromDate, toDate);
            return View(orderList);
        }

        [HttpPost]
        public ActionResult uploadorder(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                _adminLoginService.uploadorderdata(file);
            }

            return Json(new { success = true });
        }


        [HttpPost]
        public ActionResult uploadproduct(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                _adminLoginService.uploadproductdata(file);
            }

            return Json(new { success = true });
        }


        [HttpGet]
        public ActionResult getOrderDetails(string productId)
        {

            order_contents_n product = _adminLoginService.getOrderPopup(productId);
            if (product != null)
            {
                return Json(product, JsonRequestBehavior.AllowGet);
            }
            else
            {
                // Return an appropriate response if the product is not found
                return HttpNotFound("Product not found");
            }
        }

        [HttpGet]
        public ActionResult getProductDetails(string productId)
        {

            product product = _adminLoginService.getProductPopup(productId);
            if (product != null)
            {
                return Json(product, JsonRequestBehavior.AllowGet);
            }
            else
            {
                // Return an appropriate response if the product is not found
                return HttpNotFound("Product not found");
            }
        }

        public ActionResult productCatalogue(string filter)
        {

            List<product> productList = _adminLoginService.getproducts(filter);
            return View(productList);
        }
       

        public ActionResult ordercsv()
        {

            MemoryStream memoryStream = _adminLoginService.GetOrderCsv();
            return File(memoryStream, "application/octet-stream", "orders.csv");
        }


        public ActionResult productcsv()
        {

            MemoryStream memoryStream = _adminLoginService.getProductCsv();
            return File(memoryStream, "application/octet-stream", "product.csv");
        }
        public ActionResult Logout()
        {
            Session.Clear();
            Response.Cookies[FormsAuthentication.FormsCookieName].Expires = DateTime.Now.AddDays(-1);
            return RedirectToAction("Index");
        }

    }
}