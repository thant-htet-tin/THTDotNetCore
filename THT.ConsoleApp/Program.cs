// See https://aka.ms/new-console-template for more information
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using THT.ConsoleApp.AdoDotNetExamples;

Console.WriteLine("Hello, World!");

//SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder();
//sqlConnectionStringBuilder.DataSource = "."; //server name
//sqlConnectionStringBuilder.InitialCatalog = "THARGYI"; //dbname
//sqlConnectionStringBuilder.UserID = "sa";
//sqlConnectionStringBuilder.Password = "thargyi";

//SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
//{
//    DataSource = ".",
//    InitialCatalog = "THARGYI",
//    UserID = "sa",
//    Password = "thargyi"
//};

//SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);

//connection.Open();

//string query = @"
//    SELECT [BlogId]
//          ,[BlogTitle]
//          ,[BlogAuthor]
//          ,[BlogContent]
//      FROM [dbo].[Tbl_Blog]
//    ";

//SqlCommand command = new SqlCommand(query, connection);
//SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
//DataTable dt = new DataTable();
//sqlDataAdapter.Fill(dt);

//connection.Close();

////DataSet can access multiple tables)
////DataTable
////DataRow
////DataColumn

////DataSet->DataTable->DataRow->DataColumn

//foreach(DataRow dr in dt.Rows)
//{
//    Console.WriteLine($"Id => {dr["BlogId"]}");
//    Console.WriteLine($"Title => {dr["BlogTitle"]}");
//    Console.WriteLine($"Author => {dr["BlogAuthor"]}");
//    Console.WriteLine($"Content => {dr["BlogContent"]}");

//    Console.WriteLine("------------------");
//}

AdoDotNetExample adoDotNetExample = new AdoDotNetExample();
adoDotNetExample.Run();

Console.ReadKey();

