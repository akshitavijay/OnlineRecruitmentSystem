using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Net.Http.Headers;


namespace OnlineRecruitmentSystemMVC
{
    public class GlobalVariables
    {
        public HttpClient WebApiClient = new HttpClient();

        public GlobalVariables()
        {
            
            WebApiClient.BaseAddress = new Uri("https://localhost:44379/api/");
            WebApiClient.DefaultRequestHeaders.Clear();
            WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }


    }
}