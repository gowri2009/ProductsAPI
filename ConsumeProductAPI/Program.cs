using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;

namespace ConsumeProductAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpClient client = CreateClient();
            // client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImN3aGFkbWluIiwicm9sZSI6ImFkbWluIiwibmJmIjoxNTk1MjAxMzgyLCJleHAiOjE5MTA3MzQxODIsImlhdCI6MTU5NTIwMTM4Mn0.pGvwYPs3WGsag2uzaojoy2-mwO07OkFXsNxelq3CKaM");
            var productObject = new
            {
                Name = "test prod 3",
                Description = "test description for prod 2",
                Price = "10.12"
            };

            //Converting the object to a json string. NOTE: Make sure the object doesn't contain circular references.
            string json = JsonConvert.SerializeObject(productObject);

            //Needed to setup the body of the request
            StringContent data = new StringContent(json, Encoding.UTF8, "application/json");

            var t = client.PostAsync("/api/createproduct", data);
            t.Wait();
            var response = t.Result;
            Console.WriteLine(response.StatusCode);

            if (client == null)
                 client = CreateClient();

            t = client.GetAsync("api/products");
            t.Wait();
            response = t.Result;
            var result = response.Content.ReadAsStringAsync().Result;

            var resp = JsonConvert.DeserializeObject(result);
            Console.WriteLine(resp);

            Console.ReadKey();

        }

        static HttpClient CreateClient()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:5001");
            return client;
        }
    }
}
