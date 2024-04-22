using BuyInBulk.DataAccess.Data;
using BuyInBulk.Models;
using BuyInBulk.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuyInBulk.DataAccess.DBInitializer
{
    public class DBInitializer : IDBInitializer
    {
        private readonly ApplicationDbContext _dbContext; 
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DBInitializer(ApplicationDbContext dbContext, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }


        
        
        
        public void Initialize()
        {
            try
            {
                //push migrations if not applied. 
                if (_dbContext.Database.GetPendingMigrations().Count() > 0)
                {
                    _dbContext.Database.Migrate();
                }

            }
            catch(Exception ex) { }

            //create roles if not created 
            if (!_roleManager.RoleExistsAsync(CommonConstants.Role_Customer).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(CommonConstants.Role_Customer)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(CommonConstants.Role_Employee)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(CommonConstants.Role_Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(CommonConstants.Role_Company)).GetAwaiter().GetResult();

                //If Role are not created then we will create admin user as well
                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "admin@buyinbulk.com",
                    Email = "admin@buyinbulk.com",
                    Name = "Deepak Shubham Bhardwaj",
                    PhoneNumber = "1112223333",
                    StreetAddress = "test 123 Ave",
                    State = "New Delhi",
                    Pincode = "110001",
                    City = "New Delhi"
                }, "Admin123*").GetAwaiter().GetResult();


                ApplicationUser user = _dbContext.ApplicationUsers.FirstOrDefault(u => u.Email == "admin@buyinbulk.com");
                _userManager.AddToRoleAsync(user, CommonConstants.Role_Admin).GetAwaiter().GetResult();
            }


        }
    }
}
