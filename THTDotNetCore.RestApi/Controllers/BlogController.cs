using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using THTDotNetCore.RestApi.Models;

namespace THTDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly AppDbContext _appDbContext = new AppDbContext();

        [HttpGet]
        public IActionResult GetBlogs()
        {

            List<BlogDataModel> lst = _appDbContext.Blogs.ToList();
            return Ok(lst);
        }

        [HttpGet("{pageNo}/{pageSize}")]
        public IActionResult GetBlogsByPagination(int pageNo, int pageSize)
        {
            //pageNo = 1[1-10]
            //pageNo = 2[11-20]
            //start row no = (end row no - pageSize) + 1
            //end row no = pageNo * pageSize
            List<BlogDataModel> lst =
                _appDbContext.Blogs
                .Skip((pageNo - 1) * pageSize) //if(pageNo==5,PageSize==10) ((5-1) * 10) = 40 skip
                .Take(pageSize)
                .ToList();

            int rowCount = _appDbContext.Blogs.Count();
            int pageCount = rowCount / pageSize;

            if (rowCount % pageSize > 0)
            {
                pageCount++;
            }

            return Ok(
                new
                {
                    IsEndOfPage = pageNo >= pageCount,
                    PageCount = pageCount,
                    pageNo = pageNo,
                    pageSize = pageSize,
                    data = lst
                });
        }

        [HttpGet("{id}")]
        public IActionResult GetBlogById(int id)
        {
            BlogDataModel? item = _appDbContext.Blogs.FirstOrDefault(x => x.BlogId == id);

            if (item is null)
            {
                return NotFound("No data found");
            }

            return Ok(item);
        }

        [HttpPost]
        public IActionResult CreateBlog(BlogDataModel blog)
        {
            _appDbContext.Blogs.Add(blog);
            int result = _appDbContext.SaveChanges();

            string message = result > 0 ? "Saving Successful" : "Saving Failed";

            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, BlogDataModel blog)
        {
            BlogDataModel? item = _appDbContext.Blogs.FirstOrDefault(x => x.BlogId == id);

            if (item is null)
            {
                return NotFound("No data found");
            }
                        
            if (string.IsNullOrEmpty(blog.BlogTitle))
            {
                return BadRequest("Blog Title is Required");
            }

            if (string.IsNullOrEmpty(blog.BlogAuthor))
            {
                return BadRequest("Blog Author is Required");
            }

            if (string.IsNullOrEmpty(blog.BlogContent))
            {
                return BadRequest("Blog Content is Required");
            }


            item.BlogTitle = blog.BlogTitle;
            item.BlogAuthor = blog.BlogAuthor;
            item.BlogContent = blog.BlogContent;

            int result = _appDbContext.SaveChanges();

            string message = result > 0 ? "Update Successful" : "Update Failed";

            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogDataModel blog)
        {
            BlogDataModel? item = _appDbContext.Blogs.FirstOrDefault(x => x.BlogId == id);

            if (item is null)
            {
                return NotFound("No data found");
            }

            if (!string.IsNullOrEmpty(blog.BlogTitle))
            {
                item.BlogTitle = blog.BlogTitle;
            }

            if(!string.IsNullOrEmpty(blog.BlogAuthor))
            {
                item.BlogAuthor = blog.BlogAuthor;
            }

            if (!string.IsNullOrEmpty(blog.BlogContent))
            {
                item.BlogContent = blog.BlogContent;
            }
            
            

            int result = _appDbContext.SaveChanges();

            string message = result > 0 ? "Update Successful" : "Update Failed";

            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            BlogDataModel? item = _appDbContext.Blogs.FirstOrDefault(x => x.BlogId == id);

            if (item is null)
            {
                return NotFound("No data found");
            }

            _appDbContext.Blogs.Remove(item);

            int result = _appDbContext.SaveChanges();

            string message = result > 0 ? "Delete Successful" : "Delete Failed";

            return Ok(message);
        }
    }
}
