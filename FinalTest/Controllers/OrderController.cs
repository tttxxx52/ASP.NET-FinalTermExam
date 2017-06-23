using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalTest.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            Models.OrderService orderService = new Models.OrderService();
            List<Models.OrderDetails> dataList = orderService.GetCodeValName();
            List<SelectListItem> CodeValList = new List<SelectListItem>();

            foreach (var item in dataList)
            {
                CodeValList.Add(new SelectListItem()
                {
                    Text = item.CodeValName,
                    Value = item.CodeId.ToString(),
                });
            }

            ViewBag.CodeValName = CodeValList;
            return View();
        }


        /// <summary>
        /// 回傳訂單資料
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetData(Models.Order order)
        {

            Models.OrderService orderService = new Models.OrderService();
            List<Models.Order> list = orderService.SearchOrder(order);
            return this.Json(list);
        }



        [HttpGet]
        public ActionResult DoDelete(string CustomerID)
        {
            Models.OrderService orderService = new Models.OrderService();
            orderService.DeleteOrderById(CustomerID);
            return RedirectToAction("Index");
        }



        public ActionResult InsertPage()
        {

            return View();
        }



        [HttpPost]
        public ActionResult DoInsert(Models.Order order)
        {

            Models.OrderService orderService = new Models.OrderService();
            orderService.InsertOrder(order);
            return RedirectToAction("Index");
        }


    }
}