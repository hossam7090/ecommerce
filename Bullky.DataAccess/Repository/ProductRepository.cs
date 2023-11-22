using bulky.DataAccess.Data;
using bulky.Models.Models;
using Bullky.DataAccess.Repository.IRepository;
using Bullky.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bullky.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        void IProductRepository.Update(Product product)
        {
            _db.products.Update(product);
        }

        
    }
}
