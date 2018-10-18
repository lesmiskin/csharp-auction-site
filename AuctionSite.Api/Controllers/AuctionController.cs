
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using System.Data.SQLite;
using System.Data;
using System.Net;

namespace AuctionSite.Api.Controllers {
	[ApiController]
	public class AuctionController : ControllerBase {
		#region Privates

		private IDbConnection EstablishDatabaseConnection() {
			return new SQLiteConnection(@"data source=Database\sqlite.db");
		}

		#endregion

		/// <summary>
		/// One-time setup to initialise the database structure, and establish fresh data.
		/// </summary>
		[HttpGet("setup")]
		public async Task<ActionResult> Setup() {
			// Read setup script from disk.
			var setupScript = System.IO.File.ReadAllText(
				@"Database\create-database-structure.sql"
			);

			// Run the script against the database.
			using(var db = EstablishDatabaseConnection()) {
				await db.ExecuteAsync(setupScript);
			}

			// Indicate success with a CREATED http response.
			return new StatusCodeResult(201);
		}

		[HttpGet("auctions")]
		public async Task<ActionResult<IEnumerable<dynamic>>> Auctions() {
			// return list of auctions *straight* from the database.
			using(var db = EstablishDatabaseConnection()) {
				return (await db.QueryAsync(
					"select * from auctions"
				)).ToList();
			}	
		}

		[HttpGet("auctions/{id}")]
		public async Task<ActionResult<dynamic>> GetAuction(
			int id
		) {
			using(var db = EstablishDatabaseConnection()) {
				return await db.QuerySingleAsync(
					"select * from auctions " + 
						"where id = @id", 
					new {
						id
					} 
				);
			}	
		}

		[HttpPost("auctions/{id}")]
		public async Task<ActionResult> UpdateAuction(
			int id, 
			[FromBody] dynamic body
		) {
			using(var db = EstablishDatabaseConnection()) {
				await db.ExecuteAsync(
					"update auctions " +
						"set title = @title " +
						"where id = @id", 
					new {
						id, 
						body.title
					} 
				);
			}	

			return new StatusCodeResult((int)HttpStatusCode.OK);
		}

		[HttpPost("auctions")]
		public async Task<ActionResult> CreateAuction([FromBody] dynamic body) {
			using(var db = EstablishDatabaseConnection()) {
				await db.ExecuteAsync(
					"insert into auctions (id, title) " +
					"values(@id, @title)", 
					new {
						body.id, 
						body.title
					} 
				);
			}	

			return new StatusCodeResult((int)HttpStatusCode.Created);
		}
	}
}
