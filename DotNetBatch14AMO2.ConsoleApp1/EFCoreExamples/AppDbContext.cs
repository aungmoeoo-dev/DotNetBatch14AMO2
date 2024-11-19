using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetBatch14AMO2.ConsoleApp.EFCoreExamples;

internal class AppDbContext : DbContext
{
	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{

		if (!optionsBuilder.IsConfigured)
		{
			optionsBuilder.UseSqlServer(AppSettings._sqlConnectionStringBuilder.ConnectionString);
		}
	}

	public DbSet<TBLBlog> Blogs { get; set; }
}

[Table("TBL_Blog")]
public class TBLBlog
{
	[Key]
	[Column("BlogId")]
	public string Id { get; set; }
	[Column("BlogTitle")]
	public string Title { get; set; }
	[Column("BlogAuthor")]
	public string Author { get; set; }
	[Column("BlogContent")]
	public string Content { get; set; }
}