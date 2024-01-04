using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using THTDotNetCore.ConsoleApp.Models;

namespace THTDotNetCore.ConsoleApp.RefitExamples
{
    public class RefitExample
    {
        private readonly IBlogApi _blogApi = RestService.For<IBlogApi>("https://localhost:7216");
        public async Task Run()
        {
            //await Read();
            //await Edit(1);
            //await Edit(5555);
            //await Create("New next tht Title", "New next tht Author", "New next tht Content");
            //await Update(6, "New Title", "New Author", "New Content");
            await Delete(11);
        }

        public async Task Read()
        {
            List<BlogDataModel> lst = await _blogApi.GetBlogs();
            foreach (BlogDataModel item in lst)
            {
                Console.WriteLine(item.BlogId);
                Console.WriteLine(item.BlogTitle);
                Console.WriteLine(item.BlogAuthor);
                Console.WriteLine(item.BlogContent);

                Console.WriteLine("=================");
            }
        }

        public async Task Edit(int id)
        {
            try
            {
                BlogDataModel item = await _blogApi.GetBlog(id);

                Console.WriteLine(item.BlogId);
                Console.WriteLine(item.BlogTitle);
                Console.WriteLine(item.BlogAuthor);
                Console.WriteLine(item.BlogContent);

                Console.WriteLine("=================");
            }
            catch (Refit.ApiException ex)
            {

                Console.WriteLine(ex.ReasonPhrase!.ToString());
                Console.WriteLine(ex.Content!.ToString());
            }
        }

        public async Task Create(string title, string author, string content)
        {
            var message = await _blogApi.CreateBlog(new BlogDataModel
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            });

            await Console.Out.WriteAsync(message.ToString());
        }

        public async Task Update(int id, string title, string author, string content)
        {
            try
            {
                string message = await _blogApi.UpdateBlog(id, blog: new BlogDataModel
                {
                    BlogTitle = title,
                    BlogAuthor = author,
                    BlogContent = content
                });

                await Console.Out.WriteAsync(message.ToString());

            }
            catch (Refit.ApiException ex)
            {

                Console.WriteLine(ex.ReasonPhrase!.ToString());
                Console.WriteLine(ex.Content!.ToString());
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                string message = await _blogApi.DeleteBlog(id);
                await Console.Out.WriteAsync(message.ToString());
            }
            catch (Refit.ApiException ex)
            {
                Console.WriteLine(ex.ReasonPhrase!.ToString());
            }
        }
    }
}
