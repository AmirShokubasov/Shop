
using AutoMapper;
using Layers.Models;
using LayersDAL.EF;
using LayersDLL.DTO;
using LayersDLL.Infrastructure;
using LayersDLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Layers.Custom;

namespace Layers.Controllers
{
    public class HomeController : Controller
    {
        IOrderService orderService;
        public StoreContext db;

        public HomeController(IOrderService serv)
        {
            orderService = serv;
        }

        [CustomFilter]
        public ActionResult Index(string search,string sortBy)

        {
           
            //IEnumerable<GoodDTO> goodDtos = orderService.GetGoods();
            //var mapper = new MapperConfiguration(cfg => cfg.CreateMap<GoodDTO, GoodVM>()).CreateMapper();
            //var goods = mapper.Map<IEnumerable<GoodDTO>, List<GoodVM>>(goodDtos);
            //return View(goods);

            IEnumerable<GoodDTO> goodDtos = orderService.Search(search);
           

            ViewBag.NameSort = String.IsNullOrEmpty(sortBy) ? "Name desc" : "";
            // var goodsSort = db.Goods.AsQueryable();
           // goodDtos.AsQueryable();
            switch (sortBy)
            {
                case "Name desc":
                    goodDtos = goodDtos.OrderByDescending(x => x.Company);
                    break;
                default:
                    goodDtos = goodDtos.OrderBy(x => x.Company);
                    break;

            }
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<GoodDTO, GoodVM>()).CreateMapper();
            var goods = mapper.Map<IEnumerable<GoodDTO>, List<GoodVM>>(goodDtos);

            return View(goods);

        }
        [Authorize(Roles ="admin")]
        public ActionResult ListUser()
        {
            IEnumerable<UserDTO> userDtos = orderService.GetUsers();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<UserDTO, UserVM>()).CreateMapper();
            var users = mapper.Map<IEnumerable<UserDTO>, List<UserVM>>(userDtos);
            return View(users);
        }

        [Authorize]

        public ActionResult Purchase()
        {
            IEnumerable<OrderDTO> orderDtos = orderService.GetOrders();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<OrderDTO, OrderVM>()).CreateMapper();
            var orders= mapper.Map<IEnumerable<OrderDTO>, List<OrderVM>>(orderDtos);
            if ((int)Session["id"] == 1)
            {
                return View(orders);
            }
            else {
                return View(orders.Where(u => u.UserId == (int)Session["id"]));
            }
        }

        [Authorize]
        public ActionResult Buy(string name, int price)
        {
            try
            {
                OrderVM orderNew = new OrderVM
                {
                    Name = name,
                    Price = price
                };
                return View(orderNew);
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }
        [Authorize]

        [HttpPost]

        public ActionResult Buy(OrderVM order)
        {
            try
            {
                var orderDto = new OrderDTO
                {
                    UserId = (int)Session["id"],
                    Name = order.Name,
                    Price = order.Price
                };
                orderService.MakeOrder(orderDto);
                return View("Confirm");
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return View(order);
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Delete(int id)
        {
            GoodDTO good = new GoodDTO
            {
                Id = id
            };
            orderService.DeleteGood(good);
            return View("Confirm");

        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(GoodVM good)
        {
            GoodDTO goodDto = new GoodDTO
            {
                Id = good.Id,
                Name = good.Name,
                Price = good.Price,
                Company = good.Company
                
            };
            orderService.AddGood(goodDto);
            return View("Confirm");
        }
        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult Edit(int id,string name,string company,int price)
        {
  
            GoodVM good = new GoodVM{
                Id = id,
                Name = name,
                Price = price,
                Company = company
                
            };
            return View(good);
        }


        [HttpPost]
        public ActionResult Edit(GoodVM goodVm)
        {
            if (ModelState.IsValid)
            {


                GoodDTO goodDto = new GoodDTO
                {
                    Id = goodVm.Id,
                    Name = goodVm.Name,
                    Price = goodVm.Price,
                    Company = goodVm.Company

                };
                orderService.UpdateGood(goodDto);
                return View("Confirm");
            }
            else
            {
                return View(goodVm);
            }
            

            
        }
        //public ActionResult Search(string search)
        //{
            
            
            
        //}

        protected override void Dispose(bool diposing)
        {
            orderService.Dispose();
            base.Dispose(diposing);

        }
    }

}