using BlazorApp.Data;
using System.Text.Json;
using System.Text;
using System.Net.Http.Headers;

namespace BlazorApp.Services
{
    public class RecordService
    {
        private readonly HttpClient _httpClient;

        public RecordService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Record>> GetUsersAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Record>>("Records/Users");
        }
        public async Task<bool> InsertUserAsync(Record user)
        {
            try
            {
                if (user == null)
                {
                    Console.WriteLine("Invalid user object.");
                    return false;
                }
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // Ensures camelCase naming
                    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.Never
                };
                var json = JsonSerializer.Serialize(user, options);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                string url = $"Records/InsertUser";

                // Send POST request
                HttpResponseMessage response = await _httpClient.PostAsync(url, content);

                string responseBody = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Update Success: " + responseBody);
                    return true;
                }
                else
                {
                    Console.WriteLine($"Error {response.StatusCode}: {responseBody}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return false;
            }
            //HttpClient client = new HttpClient();

            //HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7170/api/Records/InsertUser");

            //request.Headers.Add("accept", "*/*");
         
            //request.Content = new StringContent("{\n  \"id\": 0,\n  \"createdBy\": \"asdfds\",\n  \"updatedBy\": \"asdf\",\n  \"firstName\": \"asdf\",\n  \"lastName\": \"asdf\",\n  \"email\": \"asdf\"\n}");
            //request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            //HttpResponseMessage response = await client.SendAsync(request);
            //response.EnsureSuccessStatusCode();
            //string responseBody = await response.Content.ReadAsStringAsync();
            //return response.IsSuccessStatusCode;
            //try
            //{
            //    var response1 = await _httpClient.PostAsJsonAsync("Records/InsertUser", user);                
            //}
            //catch(Exception ex) {
            //    var a = 1;
            //}
            //var response = await _httpClient.PostAsJsonAsync("Records/InsertUser", user);
            //return response.IsSuccessStatusCode;
        }
        public async Task<bool> UpdateUserAsync(Record user)
        {
            try
            {
                if (user == null || user.Id == 0)
                {
                    Console.WriteLine("Invalid user object.");
                    return false;
                }
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // Ensures camelCase naming
                    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.Never
                };
                var json = JsonSerializer.Serialize(user, options);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                string url = $"Records/UpdateUser/{user.Id}";

                // Send PUT request
                HttpResponseMessage response = await _httpClient.PutAsync(url, content);

                string responseBody = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Update Success: " + responseBody);
                    return true;
                }
                else
                {
                    Console.WriteLine($"Error {response.StatusCode}: {responseBody}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return false;
            }
            //HttpClient client = new HttpClient();

            //HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, "https://localhost:7170/api/Records/UpdateUser/116");

            //var options = new JsonSerializerOptions
            //{
            //    WriteIndented = true,
            //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // Ensures camelCase naming
            //    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.Never
            //};
            //var json = JsonSerializer.Serialize(user,options);
            //Console.WriteLine(json);

            //request.Headers.Add("accept", "*/*");
            //request.Content = new StringContent(json);
            //request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            //HttpResponseMessage response = await client.SendAsync(request);
            //response.EnsureSuccessStatusCode();

            //// Check response
            //if (response.IsSuccessStatusCode)
            //{
            //    string responseBody = await response.Content.ReadAsStringAsync();
            //    Console.WriteLine("Update Success: " + responseBody);
            //    return true;
            //}
            //else
            //{
            //    string error = await response.Content.ReadAsStringAsync();
            //    Console.WriteLine($"Error {response.StatusCode}: {error}");
            //    return false;
            //}
        }

        public async Task<Record?> GetUserByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Record>($"Records/GetUser/{id}");
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"Records/DeleteUser/{id}");
            return response.IsSuccessStatusCode;
        }


    }
}
