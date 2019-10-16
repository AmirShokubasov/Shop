using AutoMapper;
using Layers.Models;
using LayersDLL.DTO;
using LayersDLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Layers.Controllers
{
    public class AccountController : Controller
    {
        IOrderService orderService;

        public AccountController(IOrderService serv)
        {
            orderService = serv;
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                UserVM user = null;
                var users = GetUser();
                user = users.FirstOrDefault(u => u.Login == model.Login && u.Password == model.Password);
                if (user != null)
                {
                    Session["Name"] = user.Name;
                    Session["Id"] = user.Id;
                    FormsAuthentication.SetAuthCookie(model.Login, true);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Пользователя с таким логином и паролем нет.");
                }
            }
            return View(model);
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                UserVM user = null;
                var users = GetUser();
                user = users.FirstOrDefault(u => u.Login == model.Login&& u.Password==model.Password);           
                if (user == null)
                {
                    var userdto = new UserDTO { Name=model.Name,Login=model.Login, Password=model.Password,RoleId=2};
                    orderService.AddUser(userdto);
                    users = GetUser();
                    user = users.Where(u => u.Login == model.Login && u.Password == model.Password).FirstOrDefault();              
                    if (user != null)
                    {
                        Session["Name"] = user.Name;
                        Session["id"] = user.Id;
                        FormsAuthentication.SetAuthCookie(model.Name, true);
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь с таким логином уже существует");
                }
            }
            return View(model);
        }

        public ActionResult LogOff()
        {
            Session["Name"] = null;
            Session["Id"] = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
        // GET: Account
        public List<UserVM> GetUser()
        {
            IEnumerable<UserDTO> userDtos = orderService.GetUsers();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<UserDTO, UserVM>()).CreateMapper();
           return mapper.Map<IEnumerable<UserDTO>, List<UserVM>>(userDtos);

        }


    }
}