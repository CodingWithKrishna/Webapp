using HSBCReward.AppCode.BAL;
using HSBCReward.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HSBCReward.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart

        private readonly CartService _cartService;
        private readonly LoginService _loginService;

        public CartController()
        {
            _cartService = new CartService();
            _loginService = new LoginService();// Instantiate the ProductService
        }
      
        public ActionResult Index()
        {
            string userGuid = Session["EncryptedUserGuid"] as string;
           string userUid =_loginService.GetUserUid(userGuid);
            Session["CartItemCount"] = _cartService.getCartCount(userUid);

            int id = _loginService.GetUserId(userUid);
            List<product> productcart = _cartService.getCart(id);
            return View(productcart);

        }

        public ActionResult DeleteItem(string productCode)
        {
            int cartCode = Convert.ToInt32(productCode);
            bool succeeded = _cartService.deleteProduct(cartCode);
            if (succeeded)
            {
                return Json(new { success = true });
            }
            else
            {
                // Return a JSON object with error status
                return Json(new { success = false });
            }

        }

        public ActionResult UpdateQuantity(int productId, int quantity)
        {
            if (productId != 0 && quantity !=0)
            {
                bool succeeded = _cartService.updateProductQty(productId, quantity);
                if (succeeded)
                {
                    return Json(new { success = true });
                }
                else
                {
                    // Return a JSON object with error status
                    return Json(new { success = false });
                }
            }
            return Json(new { success = false });

        }

    }
}