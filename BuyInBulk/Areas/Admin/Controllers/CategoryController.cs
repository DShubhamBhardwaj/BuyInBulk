using BuyInBulk.DataAccess.Data;
using BuyInBulk.DataAccess.Repository;
using BuyInBulk.Models;
using BuyInBulk.Utility;
using BuyInBulk.DataAccess.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace BuyInBulk.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = CommonConstants.Role_Admin)]
    public class CategoryController : Controller
    {
        private readonly IWorkContext _workContext;

        public CategoryController(IWorkContext workContext)
        {
            _workContext = workContext;
        }
        public IActionResult Index()
        {
            List<Category> categoryList = _workContext.Category.GetAll().ToList();
            return View(categoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "the name and display order cannot be the same");
            }
            if (ModelState.IsValid)
            {
                _workContext.Category.Add(category);
                _workContext.Save();
                TempData["success"] = "Category Created successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _workContext.Category.Get(u => u.Id == id);
            //Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //Category? categoryFromDb2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault();

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _workContext.Category.Update(obj);
                _workContext.Save();
                TempData["success"] = "Category Updated successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _workContext.Category.Get(u => u.Id == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category? obj = _workContext.Category.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _workContext.Category.Remove(obj);
            _workContext.Save();
            TempData["success"] = "Category Deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
