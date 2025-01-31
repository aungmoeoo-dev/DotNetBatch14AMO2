﻿using Dapper;
using DotNetBatch14AMO2.ConsoleApp3.Dtos;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetBatch14AMO2.ConsoleApp3.DapperExamples;

internal class DapperExample
{
	public void Read()
	{
		using IDbConnection connection = new SqlConnection(AppSettings.SqlConnectionStringBuilder.ConnectionString);

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
		using IDbConnection connection = new SqlConnection(AppSettings.SqlConnectionStringBuilder.ConnectionString);

		string query = $"select * from TBL_Blog where BlogId = '{id}'";
		var blog = connection.Query<BlogDto>(query).FirstOrDefault();

		if(blog is null)
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
		using IDbConnection connection = new SqlConnection(AppSettings.SqlConnectionStringBuilder.ConnectionString);

		string query = $@"INSERT INTO [dbo].[TBL_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           ('{title}'
           ,'{author}'
           ,'{content}')";

		int result = connection.Execute(query);

		string message = result > 0 ? "Saving successful." : "Saving failed.";

		Console.WriteLine(message);
	}

	public void Update(string id, string title, string author, string content)
	{
		using IDbConnection connection = new SqlConnection(AppSettings.SqlConnectionStringBuilder.ConnectionString);

		string query = $@"UPDATE [dbo].[TBL_Blog]
   SET [BlogTitle] = '{title}'
      ,[BlogAuthor] = '{author}'
      ,[BlogContent] = '{content}'
 WHERE BlogId = '{id}'";

		int result = connection.Execute(query);

		string message = result > 0 ? "Updating successful." : "Updating failed.";

		Console.WriteLine(message);
	}

	public void Delete(string id)
	{
		using IDbConnection connection = new SqlConnection(AppSettings.SqlConnectionStringBuilder.ConnectionString);

		string query = $"delete from TBL_Blog where BlogId = '{id}'";
		int result = connection.Execute(query);

		string message = result > 0 ? "Deleting successful." : "Deleting failed.";

		Console.WriteLine(message);
	}
}
