﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStack.Orm
{
	public class OrmConnectionFactory
	{
		private readonly IDialectProvider _dialectProvider;
		private readonly string _connectionString;

		public OrmConnectionFactory(IDialectProvider dialectProvider, string connectionString)
		{
			_dialectProvider = dialectProvider;
			_connectionString = connectionString;
		}
		
		/// <summary>
		/// Default command timeout (in seconds) set on OrmConnections created from the Factory
		/// </summary>
		public int DefaultCommandTimeout { get; set; }

		public IDialectProvider DialectProvider => _dialectProvider;

		public OrmConnection OpenConnection()
		{
			var conn = _dialectProvider.CreateConnection(_connectionString);
			conn.CommandTimeout = DefaultCommandTimeout;
			conn.Open();
			return conn;
		}

		public async Task<OrmConnection> OpenConnectionAsync()
		{
			var conn = _dialectProvider.CreateConnection(_connectionString);
			conn.CommandTimeout = DefaultCommandTimeout;
			await conn.OpenAsync();
			return conn;
		}

		public override string ToString()
		{
			return _dialectProvider.GetType().ToString();
		}
	}
}
