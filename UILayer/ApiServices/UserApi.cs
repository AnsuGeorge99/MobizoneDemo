using APILayer.Models;
using DomainLayer;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace UILayer.Datas.Apiservices
{
    public class UserApi
    {
        public UserApi(IConfiguration configuration)
        {
        }
        public bool UserRegister(Registration user)
        //public bool UserRegister(Registration user)
        {
            using (HttpClient httpclient = new HttpClient())
            {
                string data = Newtonsoft.Json.JsonConvert.SerializeObject(user);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                string url = "https://localhost:44380/api/User/SignUp";
                Uri uri = new Uri(url);
                System.Threading.Tasks.Task<HttpResponseMessage> result = httpclient.PostAsync(uri, content);
                if (result.Result.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
        }

        public bool UserLogin(Login userLogin)
        //public bool UserLogin(Login userLogin)
        {
            using (HttpClient httpclient = new HttpClient())
            {
                string data = Newtonsoft.Json.JsonConvert.SerializeObject(userLogin);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                string url = "https://localhost:44380/api/User/SignIn";
                Uri uri = new Uri(url);
                System.Threading.Tasks.Task<HttpResponseMessage> result = httpclient.PostAsync(uri, content);
                if (result.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return true;
                }
                return false;
            }
        }
        //getuser data
        public IEnumerable<Registration> GetUserInfo()
        {

            UserResponse<IEnumerable<Registration>> _responseModel = new UserResponse<IEnumerable<Registration>>();
            using (HttpClient httpclient = new HttpClient())
            {
                
                string url = "https://localhost:44380/api/User/GetUser";
                Uri uri = new Uri(url);
                System.Threading.Tasks.Task<HttpResponseMessage> result = httpclient.GetAsync(uri);
                if (result.Result.IsSuccessStatusCode)
                {
                    System.Threading.Tasks.Task<string> response = result.Result.Content.ReadAsStringAsync();
                    _responseModel.result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<Registration>>(response.Result);
                }

                return _responseModel.result;
            }
        }

        public bool EditUserInfo(Registration userInfo)
        {
            using (HttpClient httpclient = new HttpClient())
{
                string data = Newtonsoft.Json.JsonConvert.SerializeObject(userInfo);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                string url = "https://localhost:44380/api/User/SignUp";
                Uri uri = new Uri(url);
                System.Threading.Tasks.Task<HttpResponseMessage> result = httpclient.PutAsync(uri, content);
                if (result.Result.IsSuccessStatusCode)
                {
                    return true;
                }

                return false;
            }
        }
    }
}
