
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using System.Data.SQLite;
using System.Data;

namespace AuctionSite.Api.Controllers {
	[ApiController]
	public class AuctionController : ControllerBase {
		private IDbConnection EstablishDatabaseConnection() {
			return new SQLiteConnection(@"data source=Database\sqlite.db");
		}

		[HttpGet("setup")]
		public ActionResult<string> Setup() {
			// Read setup script from disk.
			var setupScript = System.IO.File.ReadAllText(@"Database\create-database-structure.sql");

			// Run the script against the database.
			using(var db = EstablishDatabaseConnection()) {
				db.Execute(setupScript);
			}

			// Tell the client everything went ok (otherwise, details of the unhandled exception will 
			// be shown to them).
			return "Succeeded";
		}

		[HttpGet("auctions")]
		public ActionResult<string> Auctions() {
			return "value";
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
