using EmpData.Models;
using Newtonsoft.Json;
using System.Text;

namespace EmpData.Dal
{
    public class Api
    {
        static string apiUrl = "https://localhost:7135/api/User/";
        public static async Task<dynamic> GetAsync(int? id = null)
        {
            var client = new HttpClient();
            string url = apiUrl + (id == null ? "Get" : ("GetById/" + id.ToString()));
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            string responseData = await response.Content.ReadAsStringAsync();
            if (id == null)
            {
                ApiResponse api = JsonConvert.DeserializeObject<ApiResponse>(responseData);
                return api.data;

            }
            else
            {
                Employee emp = JsonConvert.DeserializeObject<Employee>(responseData);
                return emp;
            }
        }
        public static async Task DeleteAsync(int id)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Delete, apiUrl + "Delete/" + id.ToString());
            var response = await client.SendAsync(request);
            Console.WriteLine(response.EnsureSuccessStatusCode());
        }
        public static async Task AddUpdateAsync(int? id = null, Employee emp = null)
        {
            var client = new HttpClient();


            string endpoint = id != null ? $"Update/{id}" : "Add";
            string url = apiUrl + endpoint;

            string jsonContent = JsonConvert.SerializeObject(emp);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(id != null ? HttpMethod.Put : HttpMethod.Post, url);
            request.Content = content;

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            Console.WriteLine(await response.Content.ReadAsStringAsync());
        }

    }
}
