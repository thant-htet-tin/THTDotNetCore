using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using THTDotNetCore.RestApi.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace THTDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogAdoDotNetController : ControllerBase
    {

        private readonly SqlConnectionStringBuilder _sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = ".",
            InitialCatalog = "THARGYI",
            UserID = "sa",
            Password = "thargyi"
        };

        [HttpGet]
        public IActionResult GetBlogs()
        {

            SqlConnection connection = new(_sqlConnectionStringBuilder.ConnectionString);
           
            connection.Open();

            string query = @"
        SELECT [BlogId]
              ,[BlogTitle]
              ,[BlogAuthor]
              ,[BlogContent]
          FROM [dbo].[Tbl_Blog]
        ";

            SqlCommand command = new SqlCommand(query, connection);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);

            connection.Close();

            List<BlogDataModel> lst = new List<BlogDataModel>();

            foreach (DataRow dr in dt.Rows)
            {
                lst.Add(new BlogDataModel
                {
                    BlogId = (int)dr["blogId"],
                    BlogAuthor = dr["BlogAuthor"].ToString(),
                    BlogTitle = dr["BlogTitle"].ToString(),
                    BlogContent = dr["BlogContent"].ToString()
                });
            }

            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlogById(int id)
        {

            SqlConnection connection = new(_sqlConnectionStringBuilder.ConnectionString);
           
            connection.Open();

            string query = $@"
    SELECT [BlogId]
          ,[BlogTitle]
          ,[BlogAuthor]
          ,[BlogContent]
      FROM [dbo].[Tbl_Blog] WHERE BlogId= @BlogId
    ";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@BlogId", id);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);

            connection.Close();



            if (dt.Rows.Count == 0)
            {
                return NotFound("No data found.");
            }

            DataRow dr = dt.Rows[0];
            var blog = new BlogDataModel
            {
                BlogId = (int)dr["blogId"],
                BlogAuthor = dr["BlogAuthor"].ToString(),
                BlogTitle = dr["BlogTitle"].ToString(),
                BlogContent = dr["BlogContent"].ToString()
            };

            return Ok(blog);
        }

        [HttpPost]
        public IActionResult CreateBlog(BlogDataModel blog)
        {

            SqlConnection connection = new(_sqlConnectionStringBuilder.ConnectionString);
            
            connection.Open();

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

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@BlogTitle", blog.BlogTitle);
            command.Parameters.AddWithValue("@BlogAuthor", blog.BlogAuthor);
            command.Parameters.AddWithValue("@BlogContent", blog.BlogContent);

            int result = command.ExecuteNonQuery();

            connection.Close();

            string message = result > 0 ? "Saving Successful" : "Saving Failed";

            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, BlogDataModel blog)
        {

            SqlConnection connection = new(_sqlConnectionStringBuilder.ConnectionString);
       
            connection.Open();

            string query = $@"
    SELECT [BlogId]
          ,[BlogTitle]
          ,[BlogAuthor]
          ,[BlogContent]
      FROM [dbo].[Tbl_Blog] WHERE BlogId= @BlogId
    
            ";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@BlogId", id);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);

            connection.Close();

            if (dt.Rows.Count == 0)
            {
                return NotFound("No data found.");
            }

            //DataRow dr = dt.Rows[0];
            //var item = new BlogDataModel
            //{
            //    BlogId = (int)dr["blogId"],
            //    BlogAuthor = dr["BlogAuthor"].ToString(),
            //    BlogTitle = dr["BlogTitle"].ToString(),
            //    BlogContent = dr["BlogContent"].ToString()
            //};

            //command.Parameters.AddWithValue("@BlogId", id);
            //command.Parameters.AddWithValue("@BlogTitle", blog.BlogTitle);
            //command.Parameters.AddWithValue("@BlogAuthor", blog.BlogAuthor);
            //command.Parameters.AddWithValue("@BlogContent", blog.BlogContent);

            //if (item is null)
            //{
            //    return NotFound("No data found");
            //}

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

            connection.Open();

            string Updatequery = @"UPDATE [dbo].[Tbl_Blog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
 WHERE BlogId  = @BlogId";

            SqlCommand updateCommand = new SqlCommand(Updatequery, connection);

            updateCommand.Parameters.AddWithValue("@BlogId", id);
            updateCommand.Parameters.AddWithValue("@BlogTitle", blog.BlogTitle);
            updateCommand.Parameters.AddWithValue("@BlogAuthor", blog.BlogAuthor);
            updateCommand.Parameters.AddWithValue("@BlogContent", blog.BlogContent);

            int result = updateCommand.ExecuteNonQuery();

            connection.Close();

            string message = result > 0 ? "Update Successful" : "Update Failed";

            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogDataModel blog)
        {
            SqlConnection connection = new(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            string query = $@"
    SELECT [BlogId]
          ,[BlogTitle]
          ,[BlogAuthor]
          ,[BlogContent]
      FROM [dbo].[Tbl_Blog] WHERE BlogId= @BlogId
    
            ";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@BlogId", id);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);

            connection.Close();

            if (dt.Rows.Count == 0)
            {
                return NotFound("No data found.");
            }

            //DataRow dr = dt.Rows[0];
            //var item = new BlogDataModel
            //{
            //    BlogId = (int)dr["blogId"],
            //    BlogAuthor = dr["BlogAuthor"].ToString(),
            //    BlogTitle = dr["BlogTitle"].ToString(),
            //    BlogContent = dr["BlogContent"].ToString()
            //};

            //command.Parameters.AddWithValue("@BlogId", id);
            //command.Parameters.AddWithValue("@BlogTitle", blog.BlogTitle);
            //command.Parameters.AddWithValue("@BlogAuthor", blog.BlogAuthor);
            //command.Parameters.AddWithValue("@BlogContent", blog.BlogContent);
            string conditions = string.Empty;

            if (!string.IsNullOrEmpty(blog.BlogTitle))
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

            if (conditions.Length == 0)
            {
                return BadRequest("Invalid Request.");
            }

            conditions = conditions.Substring(0, conditions.Length - 2);

            blog.BlogId = id;

            connection.Open();

            string queryUpdate = $@"UPDATE [dbo].[Tbl_Blog]
   SET {conditions}
 WHERE BlogId  = @BlogId";

            SqlCommand updateCommand = new SqlCommand(queryUpdate, connection);

            if (!string.IsNullOrEmpty(blog.BlogTitle))
            {
                updateCommand.Parameters.AddWithValue("@BlogTitle", blog.BlogTitle);
            }

            if (!string.IsNullOrEmpty(blog.BlogAuthor))
            {
                updateCommand.Parameters.AddWithValue("@BlogAuthor", blog.BlogAuthor);
            }

            if (!string.IsNullOrEmpty(blog.BlogContent))
            {
                updateCommand.Parameters.AddWithValue("@BlogContent", blog.BlogContent);
            }

            updateCommand.Parameters.AddWithValue("@BlogId", id);

            int result = updateCommand.ExecuteNonQuery();

            connection.Close();

            string message = result > 0 ? "Update Successful" : "Update Failed";

            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            SqlConnection connection = new(_sqlConnectionStringBuilder.ConnectionString);
            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            string query = $@"
    SELECT [BlogId]
          ,[BlogTitle]
          ,[BlogAuthor]
          ,[BlogContent]
      FROM [dbo].[Tbl_Blog] WHERE BlogId= @BlogId
    
            ";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@BlogId", id);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);


            if (dt.Rows.Count == 0)
            {
                return NotFound("No data found.");
            }

            string UpdateQuery = @"DELETE FROM [dbo].[Tbl_Blog]
                WHERE BlogId  = @BlogId";

            SqlCommand Updatecommand = new SqlCommand(UpdateQuery, connection);

            Updatecommand.Parameters.AddWithValue("@BlogId", id);

            int result = Updatecommand.ExecuteNonQuery();

            connection.Close();

            string message = result > 0 ? "Delete Successful" : "Delete Failed";

            return Ok(message);
        }

    }
}
