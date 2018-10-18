
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
		public ActionResult Setup() {
			// Read setup script from disk.
			var setupScript = System.IO.File.ReadAllText(@"Database\create-database-structure.sql");

			// Run the script against the database.
			using(var db = EstablishDatabaseConnection()) {
				db.Execute(setupScript);
			}

			// Indicate success with a CREATED http response.
			return new StatusCodeResult(201);
		}

		[HttpGet("auctions")]
		public ActionResult<IEnumerable<dynamic>> Auctions() {
			// return list of auctions *straight* from the database.
			using(var db = EstablishDatabaseConnection()) {
				return db.Query<dynamic>("select * from auctions").ToList();
			}	
		}

		[HttpGet("auction/{id}")]
		public ActionResult<string> GetAuction(int id) {
			return "value";
		}

		[HttpPost("auction/{id}")]
		public ActionResult<string> UpdateAuction(int id) {
			return "value";
		}

		[HttpPost("auction")]
		public void CreateAuction([FromBody] string value) {
		}
	}
}
