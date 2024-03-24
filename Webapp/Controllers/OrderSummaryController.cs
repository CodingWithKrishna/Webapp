using HSBCReward.AppCode.BAL;
using HSBCReward.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HSBCReward.Controllers
{
    public class OrderSummaryController : Controller
    {

         private readonly CartService _cartService;
        private readonly LoginService _loginService;
        private readonly ProductService _productService;

        public OrderSummaryController()
        {
            _cartService = new CartService();
            _loginService = new LoginService();
            _productService = new ProductService();
        }
        // GET: OrderSummary
        public ActionResult Index()
        {
            string userGuid = Session["EncryptedUserGuid"] as string;
            string userUid = _loginService.GetUserUid(userGuid);
            int id = _loginService.GetUserId(userUid);
            List<product> productcart = _cartService.getCart(id);
            OrderComplete();
            return View(productcart);
        }


        public void OrderComplete()
        {
            string userGuid = Session["EncryptedUserGuid"] as string;
            string userUid = _loginService.GetUserUid(userGuid);
            _productService.CompleteOrder(userUid);
            // return View();
        }
    }
}