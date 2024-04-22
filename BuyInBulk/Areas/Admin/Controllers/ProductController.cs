using BuyInBulk.Models;
using BuyInBulk.Models.ViewModels;
using BuyInBulk.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BuyInBulk.DataAccess.Repository;

namespace BuyInBulk.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = CommonConstants.Role_Admin)]
    public class ProductController : Controller
    {
        private readonly IWorkContext _workContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IWorkContext workContext, IWebHostEnvironment webHostEnvironment)
        {
            _workContext = workContext;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Product> objProductList = _workContext.Product.GetAll(includeProperties: "Category").ToList();
           
            return View(objProductList);
        }

        public IActionResult Upsert(int? id)
        {
            IEnumerable<SelectListItem> categoryList = _workContext.Category.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString(),
            });
            ProductsVM productsVM = new()
            {
                CategoryList = categoryList,
                Product = new Product() 
            };
            if (id == null || id == 0)
            {
                //create
                return View(productsVM);
            }
            else
            {
                //update
                productsVM.Product = _workContext.Product.Get(u => u.Id == id);
                return View(productsVM);
            }
        }

        [HttpPost]
        public IActionResult Upsert(ProductsVM productVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if(file != null)
                {
                    string filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product");

                    if (!string.IsNullOrEmpty(productVM.Product.ImageUrl))
                    {
                        //delete the old image
                        var oldImagePath =
                            Path.Combine(wwwRootPath, productVM.Product.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var filestream = new FileStream(Path.Combine(productPath, filename), FileMode.Create))
                    {
                        file.CopyTo(filestream);
                    }
                    productVM.Product.ImageUrl =  @"\images\product\" + filename;
                }

                if (productVM.Product.Id == 0)
                {
                    _workContext.Product.Add(productVM.Product);
                }
                else
                {
                    _workContext.Product.Update(productVM.Product);
                }
                _workContext.Save();
                TempData["success"] = "Product created successfully";
                return RedirectToAction("Index");
            }
            else
            {
                productVM.CategoryList = _workContext.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                });
                return View(productVM);
            }
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> objProductList = _workContext.Product.GetAll(includeProperties: "Category").ToList();
            return Json(new { data = objProductList });
        }


        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var productToBeDeleted = _workContext.Product.Get(u => u.Id == id);
            if (productToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            var oldImagePath =
                           Path.Combine(_webHostEnvironment.WebRootPath,
                           productToBeDeleted.ImageUrl.TrimStart('\\'));

            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _workContext.Product.Remove(productToBeDeleted);
            _workContext.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }

        #endregion
    }
}