using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ServiceStudent.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DefaultController : ControllerBase
    {
        static List<Student> list = new List<Student>() {
            new Student(){ Id = 1, StudentName = "学生1", StudentAge = 21 },
            new Student(){ Id = 2, StudentName = "学生2", StudentAge = 22 },
            new Student(){ Id = 3, StudentName = "学生3", StudentAge =23 }
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
        public List<Student> GetList()
        {
            Console.WriteLine(DateTime.Now.ToString());
            return list;
        }

        [HttpGet]
        public Student GetModel(int id)
        {
            Console.WriteLine(DateTime.Now.ToString());
            return list.Find(t => t.Id == id);
        }
    }
}
