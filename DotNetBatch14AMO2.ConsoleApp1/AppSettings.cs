using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetBatch14AMO2.ConsoleApp;

internal class AppSettings
{
	
	public static readonly SqlConnectionStringBuilder _sqlConnectionStringBuilder = new()
	{
		DataSource = ".",
		InitialCatalog = "TestDB",
		UserID = "sa",
		Password = "Aa145156167!",
		TrustServerCertificate = true
	};
}
