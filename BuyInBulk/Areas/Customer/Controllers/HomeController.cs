using BuyInBulk.Models;
using BuyInBulk.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using BuyInBulk.DataAccess.Repository;

namespace BuyInBulk.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWorkContext _workContext;

        public HomeController(ILogger<HomeController> logger, IWorkContext workContext)
        {
            _workContext = workContext;
            _logger = logger;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> productList = _workContext.Product.GetAll(includeProperties: "Category");
            return View(productList);
        }

        public IActionResult Details(int productId)
        {
            ShoppingCart cart = new()
            {
                Product = _workContext.Product.Get(u => u.Id == productId, includeProperties: "Category"),
                Count = 1,
                ProductId = productId
            };
            return View(cart);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Details(ShoppingCart shoppingCart)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            shoppingCart.ApplicationUserId = userId;

            ShoppingCart cartFromDb = _workContext.ShoppingCart.Get(u => u.ApplicationUserId == userId &&
             u.ProductId == shoppingCart.ProductId);

            if (cartFromDb != null)
            {
                //shopping cart exists
                cartFromDb.Count += shoppingCart.Count;
                _workContext.ShoppingCart.Update(cartFromDb);
                _workContext.Save();
            }
            else
            {
                //add cart record
                _workContext.ShoppingCart.Add(shoppingCart);
                _workContext.Save();
                HttpContext.Session.SetInt32(CommonConstants.SessionCart, _workContext.ShoppingCart.GetAll(u => u.ApplicationUserId == userId).Count());
            }

            _workContext.Save();
            return RedirectToAction(nameof(Index), "ShoppingCart");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
