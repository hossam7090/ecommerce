using bulky.DataAccess.Data;
using Bullky.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bullky.DataAccess.Repository
{
	public class UserRepository : Repository<Users>
	{
		private readonly ApplicationDbContext _db;
		public UserRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}
	}
}
