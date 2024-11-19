using Dapper;
using DotNetBatch14AMO2.ConsoleApp.Dtos;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;

namespace DotNetBatch14AMO2.ConsoleApp.DapperExamples;

internal class DapperExample
{
	private readonly string _connectionString = AppSettings._sqlConnectionStringBuilder.ConnectionString;

	public void Read()
	{
		using IDbConnection connection = new SqlConnection(_connectionString);

		string query = "select * from TBL_Blog";
		var blogs = connection.Query<BlogDto>(query).ToList();

		foreach (var blog in blogs)
		{
			Console.WriteLine(blog.BlogId);
			Console.WriteLine(blog.BlogTitle);
			Console.WriteLine(blog.BlogAuthor);
			Console.WriteLine(blog.BlogContent);
		}
	}

	public void Edit(string id)
	{
		using IDbConnection connection = new SqlConnection(_connectionString);

		string query = $"select * from TBL_Blog where BlogId = '{id}'";
		var blog = connection.Query<BlogDto>(query).FirstOrDefault();

		if (blog is null)
		{
			Console.WriteLine("No data found.");
			return;
		}

		Console.WriteLine(blog.BlogId);
		Console.WriteLine(blog.BlogTitle);
		Console.WriteLine(blog.BlogAuthor);
		Console.WriteLine(blog.BlogContent);
	}

	public void Create(string title, string author, string content)
	{
		using IDbConnection connection = new SqlConnection(_connectionString);

		string query = $@"INSERT INTO [dbo].[TBL_Blog]
           (
           [BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           (
           '{title}'
           ,'{author}'
           ,'{content}')";

		var result = connection.Execute(query);

		string message = result < 0 ? "Saving Failed." : "Saving Successful.";

		Console.WriteLine(message);
	}

	public void Update(string id, string title, string author, string content)
	{
		using IDbConnection connection = new SqlConnection(_connectionString);

		string query = $@"UPDATE [dbo].[TBL_Blog]
   SET
      [BlogTitle] = '{title}'
      ,[BlogAuthor] = '{author}'
      ,[BlogContent] = '{content}'
 WHERE BlogId = '{id}'";

		int result = connection.Execute(query);

		string message = result < 0 ? "Updating failed." : "Updating successful.";

		Console.WriteLine(message);
	}

	public void Delete(String id)
	{
		using IDbConnection connection = new SqlConnection(_connectionString);

		string query = $"delete from TBL_Blog where BlogId = '{id}'";

		int result = connection.Execute(query);

		string message = result < 0 ? "Deleting failed." : "Deleting successful.";

		Console.WriteLine(message);
	}
}
