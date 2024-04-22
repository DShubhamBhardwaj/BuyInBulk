using BuyInBulk.DataAccess.Data;
using BuyInBulk.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuyInBulk.DataAccess.Repository
{   
    public class WorkContext : IWorkContext
    {
        private ApplicationDbContext _dbContext;
        public ICategoryRepository Category { get; private set; }
        public IProductRepository Product { get; private set; }
        public ICompanyRepository Company { get; private set; }
        public IShoppingCartRepository ShoppingCart { get; private set; }
        public IApplicationUserRepository ApplicationUser { get; private set; }
        public IOrderHeaderRepository OrderHeader { get; private set; }
        public IOrderDetailRepository OrderDetail { get; private set; }
        public WorkContext(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            ApplicationUser = new ApplicationUserRepository(_dbContext);
            Category = new CategoryRepository(_dbContext);
            Product = new ProductRepository(_dbContext);
            Company = new CompanyRepository(_dbContext);
            ShoppingCart = new ShoppingCartRepository(_dbContext);
            OrderDetail = new OrderDetailRepository(_dbContext);
            OrderHeader = new OrderHeaderRepository(_dbContext);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}