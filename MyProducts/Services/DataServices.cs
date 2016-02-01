using MyProducts.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MyProducts.Services
{
    class DataServices
    {

        private string Base_Url = "http://localhost:7824/api/Sm/";

        public async Task<List<SmartPhone>> GetSmartPhones() {
            var httpclient = new HttpClient();
            var json_Response = await httpclient.GetStringAsync(Base_Url);
            var Smartphone = await JsonConvert.DeserializeObjectAsync<List<SmartPhone>>(json_Response);
            return Smartphone;
        }
        public async Task AddSmartphone(SmartPhone s)
        {

            var httpclient = new HttpClient();
            //var Smartphone_json = JsonConvert.SerializeObjectAsync(s);
            HttpContent httpcontent = new StringContent(JsonConvert.SerializeObject(s));
            httpcontent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await httpclient.PostAsync(new Uri(Base_Url), httpcontent);
        }

        public async Task EditSmartphone(SmartPhone s)
        {

            var httpclient = new HttpClient();
            //var Smartphone_json = JsonConvert.SerializeObjectAsync(s);
            HttpContent httpcontent = new StringContent(JsonConvert.SerializeObject(s));
            httpcontent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await httpclient.PutAsync(new Uri(Base_Url+s.Id), httpcontent);
        }

        public async Task DeleteSmartphone(SmartPhone s)
        {
            var httpclient = new HttpClient();
            var reponse = await httpclient.DeleteAsync(Base_Url + s.Id);
        }


    }
}
