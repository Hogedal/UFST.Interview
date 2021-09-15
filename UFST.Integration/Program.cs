using RestSharp;
using System;

namespace UFST.Integration
{
    class Program
    {
        static void Main(string[] args)
        {
            //This bit of code is intended to run either as a shceduled task on a server or as something like a scheduled function in azure executing once pr day.

            //pretend i fetched these from config, im a bit pressed for time.
            var webshopApiUrl = "webshop.contoso.com/api";
            var qeueApiUrl = "mq.contoso.com/api";

            var WebshopClient = new RestClient(webshopApiUrl);
            var MqClient = new RestClient(qeueApiUrl);

            var getOrdersRequest = new RestRequest($"/orders?from={DateTime.Today.AddDays(-1).ToString("yyyyMMdd")}&till={DateTime.Today.AddDays(-1).ToString("yyyyMMdd")}");
            var getOrdersResponse = WebshopClient.Get(getOrdersRequest);

            var postOrderRequest = new RestRequest("/enqueue");
            postOrderRequest.AddParameter("text/json", getOrdersResponse.Content, ParameterType.RequestBody);

            MqClient.Post(postOrderRequest);
        }
    }
}
