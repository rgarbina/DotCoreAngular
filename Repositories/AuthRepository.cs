using Angular_ASPNETCore.Data;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using System.Text;
using Angular_ASPNETCore.DTO;

namespace Angular_ASPNETCore.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        public async Task<bool> Login(string username, string password)
        {
            string apiURL = "https://dev.sitemercado.com.br/api/login";
            bool retorno = false;
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var byteArray = Encoding.UTF8.GetBytes(string.Format("{0}:{1}", username, password));
                    httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                    var jObject = JsonConvert.SerializeObject(new { username, password });
                    StringContent content = new StringContent("", Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PostAsync(apiURL, content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        var receivedResponse = JsonConvert.DeserializeObject<ApiLoginReponseDto>(apiResponse);

                        retorno = receivedResponse.success;
                    }
                }
            }
            catch (Exception)
            {
            }

            return retorno;
        }
    }
}
