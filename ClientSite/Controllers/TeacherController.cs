using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClientClass;
using Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClientSite.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        [HttpGet]
        public object GetList()
        {
            string ip = Common.GetService("Teacher");
            List<Teacher> list = Common.HttpGetObject<List<Teacher>>($"http://{ip}/api/Default/GetList");
            return new
            {
                address = ip,
                data = list
            };
        }

        [HttpGet]
        public object GetModel(string id)
        {
            string ip = Common.GetService("Teacher");
            Teacher model = Common.HttpGetObject<Teacher>($"http://{ip}/api/Default/GetModel?id={id}");
            return new
            {
                address = ip,
                data = model
            };
        }
    }
}