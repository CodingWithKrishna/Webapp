using HSBCReward.AppCode.BAL;
using HSBCReward.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HSBCReward.Controllers
{
    public class OrderHistoryController : Controller
    {
        private readonly CartService _cartService;
        private readonly LoginService _loginService;

        public OrderHistoryController()
        {
            _cartService = new CartService();
            _loginService = new LoginService();
        }
        public ActionResult Index()
        {
            string userGuid = Session["EncryptedUserGuid"] as string;
            string userUid = _loginService.GetUserUid(userGuid);
            int id = _loginService.GetUserId(userUid);
            List<OrderHistoryModel> orderhistory = _cartService.orderHistory(id);
            return View(orderhistory);

        }

        public ActionResult GetOrderDetails(string orderId)
        {


            OrderHistoryModel orderdetails = _cartService.getorderdetailspopup(orderId);


            return Json(orderdetails, JsonRequestBehavior.AllowGet);
        }
    }
}