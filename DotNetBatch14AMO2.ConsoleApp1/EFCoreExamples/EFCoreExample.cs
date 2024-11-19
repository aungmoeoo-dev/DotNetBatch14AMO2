using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DotNetBatch14AMO2.ConsoleApp.EFCoreExamples;

internal class EFCoreExample
{
	private readonly AppDbContext _db = new();
	public void Read()
	{
		var list = _db.Blogs.ToList();

		foreach (var item in list)
		{
			Console.WriteLine(item.Id);
			Console.WriteLine(item.Title);
			Console.WriteLine(item.Author);
			Console.WriteLine(item.Content);
		}
	}

	public void Edit(string id)
	{
		var item = _db.Blogs.FirstOrDefault(x => x.Id == id);

		if (item is null)
		{
			Console.WriteLine("No data found.");
			return;
		}

		Console.WriteLine(item.Id);
		Console.WriteLine(item.Title);
		Console.WriteLine(item.Author);
		Console.WriteLine(item.Content);
	}

	public void Create(string title, string author, string content)
	{
		var blog = new TBLBlog
		{
			Id = Guid.NewGuid().ToString(),
			Title = title,
			Author = author,
			Content = content
		};

		_db.Blogs.Add(blog);
		int result = _db.SaveChanges();

		string message = result < 0 ? "Saving failed." : "Saving Successful.";

		Console.WriteLine(message);
	}

	public void Update(string id, string title, string author, string content)
	{
		var item = _db.Blogs.AsNoTracking().FirstOrDefault(x => x.Id == id);

		if(item is null)
		{
			Console.WriteLine("No data found.");
			return;
		}

		item.Title = title;
		item.Author = author;
		item.Content = content;

		_db.Entry(item).State = EntityState.Modified;
		int result = _db.SaveChanges();

		string message = result < 0 ? "Updating failed." : "Updating Successful.";

		Console.WriteLine(message);
	}

	public void Delete(string id)
	{
		var item = _db.Blogs.AsNoTracking().FirstOrDefault(x => x.Id == id);

		if (item is null)
		{
			Console.WriteLine("No data found.");
		}

		_db.Entry(item).State = EntityState.Deleted;
		int result = _db.SaveChanges();

		string message = result < 0 ? "Deleting failed." : "Deleting Successful.";

		Console.WriteLine(message);
	}
}
