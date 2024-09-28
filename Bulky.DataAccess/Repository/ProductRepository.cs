using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProduct
    {
        private readonly ApplicationDbContext _Db;

        public ProductRepository(ApplicationDbContext DB) : base(DB)
        {
            _Db = DB;
        }

        public void save()
        {
            _Db.SaveChanges();
        }

        public void update(Product obj)
        {
            _Db.Products.Update(obj);
        }
    }
}
