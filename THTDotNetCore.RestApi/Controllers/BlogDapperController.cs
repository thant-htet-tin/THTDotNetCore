using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using THTDotNetCore.RestApi.Models;

namespace THTDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogDapperController : ControllerBase
    {
        //private readonly SqlConnectionStringBuilder _sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        //{
        //    DataSource = ".",
        //    InitialCatalog = "THARGYI",
        //    UserID = "sa",
        //    Password = "thargyi"
        //};

        private readonly SqlConnectionStringBuilder _sqlConnectionStringBuilder;
        public BlogDapperController()
        {
            _sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = ".",
                InitialCatalog = "THARGYI",
                UserID = "sa",
                Password = "thargyi"
            };
        }

        [HttpGet]
        public IActionResult GetBlogs()
        {

            string query = @"
    SELECT [BlogId]
          ,[BlogTitle]
          ,[BlogAuthor]
          ,[BlogContent]
      FROM [dbo].[Tbl_Blog]
    ";
            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            List<BlogDataModel> lst = db.Query<BlogDataModel>(query).ToList();

            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlogById(int id)
        {
            string query = $@"
    SELECT [BlogId]
          ,[BlogTitle]
          ,[BlogAuthor]
          ,[BlogContent]
      FROM [dbo].[Tbl_Blog] WHERE BlogId= @BlogId
    ";
            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            BlogDataModel? item = db.Query<BlogDataModel>(query, new BlogDataModel { BlogId = id }).FirstOrDefault();
            //if(item == null)
            if (item is null)
            {
                return NotFound("No data found.");
            }
            return Ok(item);
        }

        [HttpPost]
        public IActionResult CreateBlog(BlogDataModel blog)
        {

            string query = @"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           (
		   @BlogTitle,
		   @BlogAuthor,
		   @BlogContent
		   )";

            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, blog);

            string message = result > 0 ? "Saving Successful" : "Saving Failed";

            return Ok(message);
        }


        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, BlogDataModel blog)
        {
            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);

            string query = $@"
    SELECT [BlogId]
          ,[BlogTitle]
          ,[BlogAuthor]
          ,[BlogContent]
      FROM [dbo].[Tbl_Blog] WHERE BlogId= @BlogId
    ";
           
            BlogDataModel? item = db.Query<BlogDataModel>(query, new BlogDataModel { BlogId = id }).FirstOrDefault();
            //if(item == null)
            if (item is null)
            {
                return NotFound("No data found.");
            }

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

            string queryUpdate = @"UPDATE [dbo].[Tbl_Blog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
 WHERE BlogId  = @BlogId";

            int result = db.Execute(queryUpdate, blog);

            string message = result > 0 ? "Update Successful" : "Update Failed";

            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogDataModel blog)
        {
            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);

            string query = $@"
    SELECT [BlogId]
          ,[BlogTitle]
          ,[BlogAuthor]
          ,[BlogContent]
      FROM [dbo].[Tbl_Blog] WHERE BlogId= @BlogId
    ";

            BlogDataModel? item = db.Query<BlogDataModel>(query, new BlogDataModel { BlogId = id }).FirstOrDefault();
            //if(item == null)
            if (item is null)
            {
                return NotFound("No data found.");
            }

            if (item is null)
            {
                return NotFound("No data found");
            }

            string conditions = string.Empty;

            if(!string.IsNullOrEmpty(blog.BlogTitle))
            {
                conditions += @"[BlogTitle] = @BlogTitle, ";
            }

            if (!string.IsNullOrEmpty(blog.BlogAuthor))
            {
                conditions += @"[BlogAuthor] = @BlogAuthor, ";
            }

            if (!string.IsNullOrEmpty(blog.BlogContent))
            {
                conditions += @"[BlogContent] = @BlogContent, ";
            }

            if(conditions.Length == 0)
            {
                return BadRequest("Invalid Request.");
            }

            conditions = conditions.Substring(0, conditions.Length - 2);

            blog.BlogId = id;

            string queryUpdate = $@"UPDATE [dbo].[Tbl_Blog]
   SET {conditions}
 WHERE BlogId  = @BlogId";

            int result = db.Execute(queryUpdate, blog);

            string message = result > 0 ? "Update Successful" : "Update Failed";

            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            string query = @"DELETE FROM [dbo].[Tbl_Blog]
                WHERE BlogId  = @BlogId";

            BlogDataModel blog = new BlogDataModel()
            {
                BlogId = id,
            };

            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, blog);

            string message = result > 0 ? "Delete Successful" : "Delete Failed";

            return Ok(message);
        }

    }
}
