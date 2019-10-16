using AutoMapper;
using Layers.Models;
using LayersDAL.EF;
using LayersDAL.Entities;
using LayersDLL.DTO;
using LayersDLL.Interfaces;
using LayersDLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Layers.Controllers
{
    public class WebController : ApiController
    {
        
        private static List<string> fructs = new List<string> {"apple","orange"};

        IOrderService orderService;
        

        public WebController(IOrderService serv)
        {
            orderService = serv;
        }

        //[HttpGet]
        //public IEnumerable<string> ListUser()
        //{
        //    return fructs;
        //    //IEnumerable<UserDTO> userDtos = orderService.GetUsers();
        //    //var mapper = new MapperConfiguration(cfg => cfg.CreateMap<UserDTO, UserVM>()).CreateMapper();
        //    //var users = mapper.Map<IEnumerable<UserDTO>, List<UserVM>>(userDtos);
        //    //List<UserVM> a = 


        //}

        [HttpGet]
        public IEnumerable<UserVM> getUsers()
        {
            IEnumerable<UserDTO> userDtos = orderService.GetUsers();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<UserDTO, UserVM>()).CreateMapper();
            var users = mapper.Map<IEnumerable<UserDTO>, List<UserVM>>(userDtos);
            return users;
        }

        //protected override void Dispose(bool diposing)
        //{
        //    orderService.Dispose();
        //    base.Dispose(diposing);

        //}


        //[HttpGet]
        //public IEnumerable<string> GetNames()
        //{
        //    return studentnames;
        //}


    }
}
