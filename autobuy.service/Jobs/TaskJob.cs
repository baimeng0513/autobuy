using Common.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace autobuy.service.Jobs
{
    class TaskJob : IJob
    {
        private ILog log = LogManager.GetLogger(typeof(TaskService));
        public void Execute(IJobExecutionContext context)
        {
            var baseAddress = new Uri("http://ccb.veip.cn");
            var cookieContainer = new CookieContainer();
            var handler = new HttpClientHandler() { CookieContainer = cookieContainer };
            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = baseAddress;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Linux; Android 4.4.2; PE-TL20 Build/HuaweiPE-TL20) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/37.0.0.0 Mobile MQQBrowser/6.2 TBS/036215 Safari/537.36 MicroMessenger/6.3.16.49_r03ae324.780 NetType/WIFI Language/zh_CN");
                client.DefaultRequestHeaders.Add("Cookie", "");
                HttpContent content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("", "")

                });
                cookieContainer.Add(baseAddress, new Cookie("Hm_lvt_145c87f690d79b172e5c81fc1b045676", "1462323535%2C1462355204%2C1462365796%2C1462419478"));
                cookieContainer.Add(baseAddress, new Cookie("Hm_lpvt_145c87f690d79b172e5c81fc1b045676", "1462419718"));
                cookieContainer.Add(baseAddress, new Cookie("Hm_lvt_b6daeabbeab83052a986d1d59eb3af8a", "1461634008%2C1461757036%2C1461823777%2C1462110228"));
                cookieContainer.Add(baseAddress, new Cookie("Hm_lvt_4ac0f3e8689efc4e11c7e52f0bcfde2f", "1461757054%2C1461823781%2C1461981740%2C1462110234"));
                cookieContainer.Add(baseAddress, new Cookie("remember-me", "cGxPSER0eXR1V0RTSmx3bVd5NEJrQT09Olg5aVhTM2ZnZDk0TG4xMmFQTUFETmc9PTp0cnVl"));
                cookieContainer.Add(baseAddress, new Cookie("JSESSIONID", "76A40AF0C3576128FE841CFD7615DCD0"));
                HttpResponseMessage response = client.PostAsync("/1/daily/sign/ops/", content).Result;
                var datetime = DateTime.Now.ToString();
                if (response.IsSuccessStatusCode)
                {
                    log.Info(datetime + "post请求成功,");
                    log.Info("返回结果：" + response.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    log.Info(datetime + "post请求失败:" + response.StatusCode);
                }
            }
        }
    }
}
