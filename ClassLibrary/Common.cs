using Consul;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    public class Common
    {
        public static IConfiguration Configuration { get; }
        static Common()
        {
            Configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", false, true).Build();
        }

        /// <summary>
        /// 需要注册的服务地址
        /// </summary>
        public static string ConsulServiceIP
        {
            get
            {
                return Configuration["Consul:ServiceIP"];
            }
        }

        /// <summary>
        /// 需要注册的服务端口
        /// </summary>
        public static int ConsulServicePort
        {
            get
            {
                string str = Configuration["Consul:ServicePort"];
                return int.Parse(str);
            }
        }

        /// <summary>
        /// 服务注册
        /// </summary>
        public static void ConsulRegister()
        {
            ConsulClient client = new ConsulClient(
                (ConsulClientConfiguration c) =>
                {
                    c.Address = new Uri(Configuration["Consul:Address"]); //Consul服务中心地址
                    c.Datacenter = Configuration["Consul:DataCenter"]; //指定数据中心，如果未提供，则默认为代理的数据中心。
                }
            );
            string checkUrl = Configuration["Consul:CheckUrl"];
            client.Agent.ServiceRegister(new AgentServiceRegistration()
            {
                ID = Guid.NewGuid().ToString(), //服务编号，不可重复
                Name = Configuration["Consul:ServiceName"], //服务名称
                Port = ConsulServicePort, //本程序的端口号
                Address = ConsulServiceIP, //本程序的IP地址
                Check = new AgentServiceCheck()
                {
                    DeregisterCriticalServiceAfter = TimeSpan.FromMilliseconds(1), //服务停止后多久注销
                    Interval = TimeSpan.FromSeconds(5), //服务健康检查间隔
                    Timeout = TimeSpan.FromSeconds(5), //检查超时的时间
                    HTTP = $"http://{ConsulServiceIP}:{ConsulServicePort}{checkUrl}" //检查的地址
                }
            });
        }

    }
}
