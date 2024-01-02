using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using THTDotNetCore.ConsoleApp.Models;
using static System.Net.Mime.MediaTypeNames;

namespace THTDotNetCore.ConsoleApp.RestClientExamples
{
    public class RestClientExample
    {
        private string _blogEndpoint = "https://localhost:7216/api/blog";
        public async Task Run()
        {

            //await Read();
            //await Edit(1);
            //await Edit(5000);
            //await Create("New next tht Title", "New next tht Author", "New next tht Content");
            //await Update(6, "New tg Title", "New tg Author", "New tg Content");
            await Delete(7);

        }

        public async Task Read()
        {
            RestClient client = new RestClient();
            RestRequest request = new RestRequest(_blogEndpoint, Method.Get);
            //await client.GetAsync(request);
            RestResponse response = await client.ExecuteAsync(request);


            if (response.IsSuccessStatusCode)
            {
                string jsonStr = response.Content!;
                List<BlogDataModel> lst = JsonConvert.DeserializeObject<List<BlogDataModel>>(jsonStr)!;
                foreach (BlogDataModel item in lst)
                {
                    Console.WriteLine(item.BlogId);
                    Console.WriteLine(item.BlogTitle);
                    Console.WriteLine(item.BlogAuthor);
                    Console.WriteLine(item.BlogContent);

                    Console.WriteLine("=================");
                }

            }

        }

        public async Task Edit(int id)
        {
            RestClient client = new RestClient();
            RestRequest request = new RestRequest($"{_blogEndpoint}/{id}", Method.Get);
            RestResponse response = await client.ExecuteAsync(request);

            if (response.IsSuccessStatusCode)
            {
                string jsonStr = response.Content!;
                BlogDataModel item = JsonConvert.DeserializeObject<BlogDataModel>(jsonStr)!;

                Console.WriteLine(item.BlogId);
                Console.WriteLine(item.BlogTitle);
                Console.WriteLine(item.BlogAuthor);
                Console.WriteLine(item.BlogContent);

                Console.WriteLine("=================");


            }
            else
            {
                Console.WriteLine(response.Content!);
            }
        }


        public async Task Create(string title, string author, string content)
        {
            BlogDataModel blog = new BlogDataModel
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };

            RestClient client = new RestClient();
            RestRequest request = new RestRequest(_blogEndpoint, Method.Post);
            request.AddJsonBody(blog);

            RestResponse response = await client.ExecuteAsync(request);
            Console.WriteLine(response.Content!);
        }

        public async Task Update(int id, string title, string author, string content)
        {
            BlogDataModel blog = new BlogDataModel
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };

            RestClient client = new RestClient();
            RestRequest request = new RestRequest($"{_blogEndpoint}/{id}", Method.Put);
            request.AddJsonBody(blog);

            RestResponse response = await client.ExecuteAsync(request);

            Console.WriteLine(response.Content!);
        }

        public async Task Delete(int id)
        {
            RestClient client = new RestClient();
            RestRequest request = new RestRequest($"{_blogEndpoint}/{id}", Method.Delete);
            RestResponse response = await client.ExecuteAsync(request);
            Console.WriteLine(response.Content!);
        }
    }
}
