using Angular_ASPNETCore.Data;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using System.Text;

namespace Angular_ASPNETCore.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        public async Task<bool> Login(string username, string password)
        {
            string apiURL = "https://dev.sitemercado.com.br/api/login";
            bool returno = false;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://dev.sitemercado.com.br/api");

                //HTTP POST
                var postTask = client.PostAsJsonAsync("login", new { username, password });
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    returno = true;
                }
            }

            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(new { username, password }), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync(apiURL, content))
                {
                    //string apiResponse = await response.Content.ReadAsStringAsync();
                    //receivedReservation = JsonConvert.DeserializeObject<Reservation>(apiResponse);

                    string apiResponse = await response.Content.ReadAsStringAsync();
                    dynamic resultado = JsonConvert.DeserializeObject(apiResponse);
                }
            }

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(apiURL))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    dynamic resultado = JsonConvert.DeserializeObject(apiResponse);
                }
            }

            return returno;
        }
    }
}
