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
    public class StudentController : ControllerBase
    {
        [HttpGet]
        public object GetList()
        {
            string ip = Common.GetService("Student");
            List<Student> list = Common.HttpGetObject<List<Student>>($"http://{ip}/api/Default/GetList");
            return new
            {
                address = ip,
                data = list
            };
        }

        [HttpGet]
        public object GetModel(string id)
        {
            string ip = Common.GetService("Student");
            Student model = Common.HttpGetObject<Student>($"http://{ip}/api/Default/GetModel?id={id}");
            return new
            {
                address = ip,
                data = model
            };
        }
    }
}