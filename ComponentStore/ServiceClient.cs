using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ComponentStore
{
    public static class ServiceClient
    {
        internal async static Task<List<string>> GetCategoryNameAsync()
        {
            using (HttpClient lcHttpClient = new HttpClient())
                return JsonConvert.DeserializeObject<List<string>>
            (await lcHttpClient.GetStringAsync("http://localhost:8555/api/Store/GetCategoryName/"));
        }

        internal async static Task<clsComponent> GetComponentAsync(string prComponentName)
        {
            using (HttpClient lcHttpClient = new HttpClient())
                return JsonConvert.DeserializeObject<clsComponent>
            (await lcHttpClient.GetStringAsync
        ("http://localhost:8555/api/store/GetComponent?Name=" + prComponentName));

        }

        internal async static Task<List<string>> GetOrdersAsync()
        {
            using (HttpClient lcHttpClient = new HttpClient())
                return JsonConvert.DeserializeObject<List<string>>
            (await lcHttpClient.GetStringAsync("http://localhost:8555/api/Store/GetOrders/"));
        }
        
        internal async static Task<clsOrder> GetOrderDetailsAsync(int prID)
        {
            using (HttpClient lcHttpClient = new HttpClient())
                return JsonConvert.DeserializeObject<clsOrder>
            (await lcHttpClient.GetStringAsync("http://localhost:8555/api/Store/GetOrderDetails?Id=" + prID));

        }

        internal async static Task<clsAllComponents> GetComponentDetails(string prID)
        {
            using (HttpClient lcHttpClient = new HttpClient())
                return JsonConvert.DeserializeObject<clsAllComponents>
            (await lcHttpClient.GetStringAsync("http://localhost:8555/api/Store/GetComponentDetails?Id=" + prID));
        }

        private async static Task<string> InsertOrUpdateAsync<TItem>(TItem prItem, string prUrl, string prRequest)
        {
            using (HttpRequestMessage lcReqMessage = new HttpRequestMessage(new HttpMethod(prRequest), prUrl))
            using (lcReqMessage.Content =
            new StringContent(JsonConvert.SerializeObject(prItem), Encoding.UTF8, "application/json"))
            using (HttpClient lcHttpClient = new HttpClient())
            {
                HttpResponseMessage lcRespMessage = await lcHttpClient.SendAsync(lcReqMessage);
                return await lcRespMessage.Content.ReadAsStringAsync();
            }
 }

        internal async static Task<string> UpdateComponentAsnc(clsAllComponents component)
        {
            return await InsertOrUpdateAsync(component, "http://localhost:8555/api/store/PutComponent", "PUT");
        }

        internal async static Task<string> InsertComponentAsync(clsAllComponents component)
        {
            return await InsertOrUpdateAsync(component, "http://localhost:8555/api/store/PostComponent", "POST");
        }

        internal async static Task<string> DeleteComponentAsync(clsAllComponents clsAllComponents)
        {
            using (HttpClient lcHttpClient = new HttpClient())
            {
                HttpResponseMessage lcRespMessage = await lcHttpClient.DeleteAsync
            ($"http://localhost:8555/api/store/DeleteComponent?Name={clsAllComponents.Name}&ProductName={clsAllComponents.tblCategoryName}");
                return await lcRespMessage.Content.ReadAsStringAsync();
            }
        }
    }
}
