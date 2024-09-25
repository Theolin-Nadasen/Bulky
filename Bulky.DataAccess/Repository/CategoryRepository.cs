using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
	public class CategoryRepository : Repository<Category>, ICategory
	{
		private readonly ApplicationDbContext _Db;

        public CategoryRepository(ApplicationDbContext DB) : base(DB)
        {
			_Db = DB;
        }

        public void save()
		{
			_Db.SaveChanges();
		}

		public void Update(Category obj)
		{
			_Db.Categories.Update(obj);
		}
	}
}
