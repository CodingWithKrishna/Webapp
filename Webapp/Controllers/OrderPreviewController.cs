using HSBCReward.AppCode.BAL;
using HSBCReward.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HSBCReward.Controllers
{
    public class OrderPreviewController : Controller
    {
        // GET: Preview
        private readonly CartService _cartService;
        private readonly LoginService _loginService;
        public OrderPreviewController()
        {
            _cartService = new CartService();
            _loginService = new LoginService();// Instantiate the ProductService
        }
        public ActionResult Index()
        {
            string userGuid = Session["EncryptedUserGuid"] as string;
            string userUid = _loginService.GetUserUid(userGuid);
            int id = _loginService.GetUserId(userUid);
            List<product> productcart = _cartService.getCart(id);
            return View(productcart);
            
        }
    }
}