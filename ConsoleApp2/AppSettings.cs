﻿using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetBatch14AMO2.ConsoleApp2
{
	internal class AppSettings
	{
		public readonly static SqlConnectionStringBuilder SqlConnectionStringBuilder = new()
		{
			DataSource = ".",
			InitialCatalog = "TestDB",
			UserID = "sa",
			Password = "Aa145156167!",
			TrustServerCertificate = true
		};
	}
}
