using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using THTDotNetCore.ConsoleApp.Models;

namespace THTDotNetCore.ConsoleApp.DapperExamples
{
    public class DapperExample
    {


        //private readonly SqlConnectionStringBuilder _sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        //{
        //    DataSource = ".",
        //    InitialCatalog = "THARGYI",
        //    UserID = "sa",
        //    Password = "thargyi"
        //};

        private readonly SqlConnectionStringBuilder _sqlConnectionStringBuilder;
        public DapperExample()
        {
            _sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = ".",
                InitialCatalog = "THARGYI",
                UserID = "sa",
                Password = "thargyi"
            };
        }

        public void Run()
        {
            //Read();
            //Edit(1);
            //Edit(14);
            //Create("New next Title", "New next Author", "New next Content");
            Update(4, "New Title", "New Author", "New Content");
            Delete(2);
        }

        private void Read()
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
            foreach (BlogDataModel item in lst)
            {
                Console.WriteLine(item.BlogId);
                Console.WriteLine(item.BlogTitle);
                Console.WriteLine(item.BlogAuthor);
                Console.WriteLine(item.BlogContent);

                Console.WriteLine("------------------");
            }


        }

        private void Edit(int id)
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

            BlogDataModel blog = new BlogDataModel()
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content,
            };

            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, blog);

            string message = result > 0 ? "Saving Successful" : "Saving Failed";
            Console.WriteLine(message);
        }

        private void Update(int id, string title, string author, string content)
        {
            string query = @"UPDATE [dbo].[Tbl_Blog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
 WHERE BlogId  = @BlogId";

            BlogDataModel blog = new BlogDataModel()
            {
                BlogId = id,
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content,
            };

            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, blog);

            string message = result > 0 ? "Update Successful" : "Update Failed";
            Console.WriteLine(message);
        }

        private void Delete(int id)
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
            Console.WriteLine(message);
        }
    }
}
