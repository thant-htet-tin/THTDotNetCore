using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using THTDotNetCore.ConsoleApp.Models;

namespace THTDotNetCore.ConsoleApp.RefitExamples
{
    public interface IBlogApi
    {
        [Get("/api/blog")]
        Task<List<BlogDataModel>> GetBlogs();

        [Get("/api/blog/{id}")]
        Task<BlogDataModel> GetBlog(int id);

        [Post("/api/blog")]
        Task<string> CreateBlog(BlogDataModel blog);

        [Put("/api/blog/{id}")]
        Task<string> UpdateBlog(int id, BlogDataModel blog);

        [Delete("/api/blog/{id}")]
        Task<BlogDataModel> DeleteBlog(int id);

    }
}
