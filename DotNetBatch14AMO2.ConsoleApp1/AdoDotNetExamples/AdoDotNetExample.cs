using Microsoft.Data.SqlClient;
using System.Data;

namespace DotNetBatch14AMO2.ConsoleApp.AdoDotNetExamples;

internal class AdoDotNetExample
{
	private readonly string _connectionString = AppSettings._sqlConnectionStringBuilder.ConnectionString;
	public void Read()
	{
		SqlConnection connection = new(_connectionString);

		connection.Open();

		string query = "select * from TBL_Blog";
		SqlCommand cmd = new(query, connection);
		SqlDataAdapter adapter = new(cmd);

		DataTable dt = new();
		adapter.Fill(dt);

		connection.Close();

		foreach (DataRow row in dt.Rows)
		{
			Console.WriteLine(row["BlogId"]);
			Console.WriteLine(row["BlogTitle"]);
			Console.WriteLine(row["BlogAuthor"]);
			Console.WriteLine(row["BlogContent"]);
		}

	}

	public void Edit(string id)
	{
		SqlConnection connection = new(_connectionString);

		connection.Open();

		string query = $"select * from TBL_Blog where BlogId = '{id}'";
		SqlCommand cmd = new(query, connection);
		SqlDataAdapter adapter = new(cmd);
		DataTable dt = new();
		adapter.Fill(dt);

		connection.Close();

		if (dt.Rows.Count <= 0)
		{
			Console.WriteLine("No data found.");
			return;
		}

		var row = dt.Rows[0];
		Console.WriteLine(row["BlogId"]);
		Console.WriteLine(row["BlogTitle"]);
		Console.WriteLine(row["BlogAuthor"]);
		Console.WriteLine(row["BlogContent"]);
		
	}

	public void Create(string title, string author, string content)
	{
		SqlConnection connection = new(_connectionString);

		connection.Open();

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

		SqlCommand cmd = new(query, connection);
		int result = cmd.ExecuteNonQuery();

		connection.Close();

		string message = result < 0 ? "Saving failed." : "Saving Successful.";

		Console.WriteLine(message);

	}

	public void Update(string id, string title, string author, string content)
	{
		SqlConnection connection = new(_connectionString);

		connection.Open();

		string query = $@"UPDATE [dbo].[TBL_Blog]
   SET
      [BlogTitle] = '{title}'
      ,[BlogAuthor] = '{author}'
      ,[BlogContent] = '{content}'
 WHERE BlogId = '{id}'";

		SqlCommand cmd = new(query, connection);

		int result = cmd.ExecuteNonQuery();

		connection.Close();

		string message = result < 0 ? "Updating failed." : "Updating Successful.";

		Console.WriteLine(message);

	}

	public void Delete(string id)
	{
		SqlConnection connection = new(_connectionString);

		connection.Open();

		string query = $"DELETE FROM [dbo].[TBL_Blog] WHERE BlogId = '{id}'";
		SqlCommand cmd = new(query, connection);

		int result = cmd.ExecuteNonQuery();

		connection.Close();

		string message = result < 0 ? "Deleting failed." : "Deleting Successful.";

		Console.WriteLine(message);
	}
}
