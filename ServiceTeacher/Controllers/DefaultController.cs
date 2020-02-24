using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ServiceTeacher.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DefaultController : ControllerBase
    {
        static List<Teacher> list = new List<Teacher>() {
            new Teacher(){ Id = 1, TeacherName = "张三", TeacherAge = 36 },
            new Teacher(){ Id =2, TeacherName = "李四", TeacherAge = 37 },
            new Teacher(){ Id = 3, TeacherName = "王二", TeacherAge = 38 }
        };

        /// <summary>
        /// 健康检查接口
        /// </summary>
        [HttpGet]
        public string Check()
        {
            return "1";
        }

        [HttpGet]
        public List<Teacher> GetList()
        {
            Console.WriteLine(DateTime.Now.ToString());
            return list;
        }

        [HttpGet]
        public Teacher GetModel(int id)
        {
            Console.WriteLine(DateTime.Now.ToString());
            return list.Find(t => t.Id == id);
        }
    }
}