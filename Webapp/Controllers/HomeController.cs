using HSBCReward.AppCode.BAL;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Services.Description;

namespace HSBCReward.Controllers
{
    public class HomeController : Controller
    {
        private readonly CartService _cartService;
        private readonly LoginService _loginService;

        public HomeController()
        {
            _loginService = new LoginService();
            _cartService = new CartService();
        }

        public ActionResult Index()
        {

           Guid? encryptedUserGuid = _loginService.GetUserGuid("7c3f36dd5363d94e96c5ebb1f9992891b1e30e08e6abd0ade8663d9ab3f4e2d1");
            if(encryptedUserGuid != null)
            {
                Session["EncryptedUserGuid"] = encryptedUserGuid.ToString();
                Session["CartItemCount"] = _cartService.getCartCount("7c3f36dd5363d94e96c5ebb1f9992891b1e30e08e6abd0ade8663d9ab3f4e2d1");
                return View();
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Internal server error");
            }


        }

        public ActionResult FAQ()
        {

            return View();
        }

        public ActionResult TnC()
        {

            return View();
        }

      
    }
}