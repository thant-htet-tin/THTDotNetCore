using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace THTDotNetCore.ConsoleApp.AdoDotNetExamples
{
    public class AdoDotNetExample
    {
        public void Run()
        {
            Read();
            //Edit(1);
            //Edit(100);
            //Create("New Title","New Author","New Content");
            //Update(1,"New Title", "New Author", "New Content");
            //Delete(10);
        }

        private void Read()
        {
            SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = ".",
                InitialCatalog = "THARGYI",
                UserID = "sa",
                Password = "thargyi"
            };

            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);

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

            //DataSet can access multiple tables)
            //DataTable
            //DataRow
            //DataColumn

            //DataSet->DataTable->DataRow->DataColumn

            foreach (DataRow dr in dt.Rows)
            {
                Console.WriteLine($"Id => {dr["BlogId"]}");
                Console.WriteLine($"Title => {dr["BlogTitle"]}");
                Console.WriteLine($"Author => {dr["BlogAuthor"]}");
                Console.WriteLine($"Content => {dr["BlogContent"]}");

                Console.WriteLine("------------------");
            }

        }

        private void Edit(int id)
        {
            SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = ".",
                InitialCatalog = "THARGYI",
                UserID = "sa",
                Password = "thargyi"
            };

            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);

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
                Console.WriteLine("No data found");
                return;
            }

            DataRow dr = dt.Rows[0];


            Console.WriteLine($"Id => {dr["BlogId"]}");
            Console.WriteLine($"Title => {dr["BlogTitle"]}");
            Console.WriteLine($"Author => {dr["BlogAuthor"]}");
            Console.WriteLine($"Content => {dr["BlogContent"]}");

            Console.WriteLine("------------------");


        }

        private void Create(string title, string author, string content)
        {
            SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = ".",
                InitialCatalog = "THARGYI",
                UserID = "sa",
                Password = "thargyi"
            };

            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);

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

            command.Parameters.AddWithValue("@BlogTitle", title);
            command.Parameters.AddWithValue("@BlogAuthor", author);
            command.Parameters.AddWithValue("@BlogContent", content);

            int result = command.ExecuteNonQuery();

            connection.Close();

            string message = result > 0 ? "Saving Successful" : "Saving Failed";
            Console.WriteLine(message);
        }

        private void Update(int id, string title, string author, string content)
        {
            SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = ".",
                InitialCatalog = "THARGYI",
                UserID = "sa",
                Password = "thargyi"
            };

            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);

            connection.Open();

            string query = @"UPDATE [dbo].[Tbl_Blog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
 WHERE BlogId  = @BlogId";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@BlogId", id);
            command.Parameters.AddWithValue("@BlogTitle", title);
            command.Parameters.AddWithValue("@BlogAuthor", author);
            command.Parameters.AddWithValue("@BlogContent", content);

            int result = command.ExecuteNonQuery();

            connection.Close();

            string message = result > 0 ? "Updating Successful" : "Updating Failed";
            Console.WriteLine(message);
        }

        private void Delete(int id)
        {
            SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = ".",
                InitialCatalog = "THARGYI",
                UserID = "sa",
                Password = "thargyi"
            };

            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);

            connection.Open();

            string query = @"DELETE FROM [dbo].[Tbl_Blog]
                WHERE BlogId  = @BlogId";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@BlogId", id);


            int result = command.ExecuteNonQuery();

            connection.Close();

            string message = result > 0 ? "Delete Successful" : "Delete Failed";
            Console.WriteLine(message);
        }
    }
}
