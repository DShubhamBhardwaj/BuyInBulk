using BuyInBulk.DataAccess.Repository;
using BuyInBulk.Models;
using BuyInBulk.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BuyInBulk.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = CommonConstants.Role_Admin)]
    public class CompanyController : Controller
    {
        private readonly IWorkContext _workContext;
        public CompanyController(IWorkContext workContext)
        {
            _workContext = workContext;
        }
    
        public IActionResult Index()
        {
            List<Company> objCompanyList = _workContext.Company.GetAll().ToList();
    
            return View(objCompanyList);
        }
    
        public IActionResult Upsert(int? id)
        {
    
            if (id == null || id == 0)
            {
                //create
                return View(new Company());
            }
            else
            {
                //update
                Company companyObj = _workContext.Company.Get(u => u.Id == id);
                return View(companyObj);
            }
    
        }
        [HttpPost]
        public IActionResult Upsert(Company CompanyObj)
        {
            if (ModelState.IsValid)
            {
    
                if (CompanyObj.Id == 0)
                {
                    _workContext.Company.Add(CompanyObj);
                }
                else
                {
                    _workContext.Company.Update(CompanyObj);
                }
    
                _workContext.Save();
                TempData["success"] = "Company created successfully";
                return RedirectToAction("Index");
            }
            else
            {
    
                return View(CompanyObj);
            }
        }
    
    
        #region API CALLS
    
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Company> objCompanyList = _workContext.Company.GetAll().ToList();
            return Json(new { data = objCompanyList });
        }
    
    
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var CompanyToBeDeleted = _workContext.Company.Get(u => u.Id == id);
            if (CompanyToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
    
            _workContext.Company.Remove(CompanyToBeDeleted);
            _workContext.Save();
    
            return Json(new { success = true, message = "Delete Successful" });
        }
        #endregion
    }
}

