using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using THTDotNetCore.ConsoleApp.Models;

namespace THTDotNetCore.ConsoleApp.EFCoreExamples
{
    public class EFCoreExample
    {
        private readonly AppDbContext _dbContext = new AppDbContext();
        public void Run()
        {
            //Read();
            //Edit(1);
            //Edit(120);
            //Create("New next Title", "New next Author", "New next Content");
            Update(5, "New Title", "New Author", "New Content");
            Delete(3);
        }

        private void Read()
        {
            //AsNoTracking better performance/ just copy cached datas similar "with (nolock)" in sql
            List<Models.BlogDataModel> lst = _dbContext.Blogs.AsNoTracking().ToList();
            foreach (BlogDataModel item in lst)
            {
                Console.WriteLine(item.BlogId);
                Console.WriteLine(item.BlogTitle);
                Console.WriteLine(item.BlogAuthor);
                Console.WriteLine(item.BlogContent);

                Console.WriteLine("=================");
            }
        }

        private void Edit(int id)
        {
            BlogDataModel? item = _dbContext.Blogs.FirstOrDefault(x=>x.BlogId==id);
            if (item is null)
            {
                Console.WriteLine("No data found");
                return;
            }

            Console.WriteLine(item.BlogId);
            Console.WriteLine(item.BlogTitle);
            Console.WriteLine(item.BlogAuthor);
            Console.WriteLine(item.BlogContent);

            Console.WriteLine("------------------");

        }

        private void Create(string title, string author, string content)
        {                              
            BlogDataModel blog = new BlogDataModel()
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content,
            };

            _dbContext.Blogs.Add(blog);
            int result = _dbContext.SaveChanges();


            string message = result > 0 ? "Saving Successful" : "Saving Failed";
            Console.WriteLine(message);
        }

        private void Update(int id,string title, string author, string content)
        {   
            
            BlogDataModel? item = _dbContext.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                Console.WriteLine("No data found");
                return;
            }

            item.BlogContent = content;
            item.BlogTitle = title;
            item.BlogAuthor = author;

            int result = _dbContext.SaveChanges(); 

            string message = result >0 ? "Updating Successful" : "Update Failed";
            Console.WriteLine(message);          
        }

        private void Delete(int id)
        {
            BlogDataModel? item = _dbContext.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                Console.WriteLine("No data found");
                return;
            }

            _dbContext.Blogs.Remove(item);
            int result = _dbContext.SaveChanges();

            string message = result > 0 ? "Deleting Successful" : "Deleting Failed";
            Console.WriteLine(message);
        }

    }
}
