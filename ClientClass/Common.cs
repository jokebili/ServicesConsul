using Consul;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ClientClass
{
    public class Common
    {
        public static IConfiguration Configuration { get; }
        static Common()
        {
            Configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", false, true).Build();
        }

        public static string ConsulAddress
        {
            get { return Configuration["ConsulAddress"]; }
        }

        /// <summary>
        /// 获取服务
        /// </summary>
        public static string GetService(string serviceName)
        {
            ConsulClient client = new ConsulClient(c => c.Address = new Uri(ConsulAddress));

            var response = client.Agent.Services().Result.Response;

            //服务名称区分大小写，若要不区分：Equals(serviceName, StringComparison.OrdinalIgnoreCase)
            var services = response.Where(s => s.Value.Service.Equals(serviceName)).Select(s => s.Value);

            //进行取模，随机取得一个服务器，或者使用其它负载均衡策略
            var service = services.ElementAt(Environment.TickCount % services.Count());

            return service.Address + ":" + service.Port;
        }

        /// <summary>
        /// 获取服务（异步方法）
        /// </summary>
        public async Task<string> GetService2(string serviceName)
        {
            ConsulClient client = new ConsulClient(c => c.Address = new Uri(ConsulAddress));

            var response = (await client.Agent.Services()).Response;

            //服务名称区分大小写，若要不区分：Equals(serviceName, StringComparison.OrdinalIgnoreCase)
            var services = response.Where(s => s.Value.Service.Equals(serviceName)).Select(s => s.Value);

            //进行取模，随机取得一个服务器，或者使用其它负载均衡策略
            var service = services.ElementAt(Environment.TickCount % services.Count());

            return service.Address + ":" + service.Port;
        }

        public static string HttpGetString(string url)
        {
            HttpClient httpClient = new HttpClient();
            string result = httpClient.GetAsync(url)
            .Result.Content.ReadAsStringAsync().Result;
            httpClient.Dispose();
            return result;
        }

        public static T HttpGetObject<T>(string url)
        {
            string result = HttpGetString(url);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(result);
        }

    }
}
