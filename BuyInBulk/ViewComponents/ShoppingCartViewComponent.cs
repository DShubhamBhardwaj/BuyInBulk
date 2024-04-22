using BuyInBulk.Utility;
using BuyInBulk.DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BuyInBulk.ViewComponents
{
    public class ShoppingCartViewComponent : ViewComponent
    {
        private readonly IWorkContext _workContext;
        public ShoppingCartViewComponent(IWorkContext unitOfWork)
        {
            _workContext = unitOfWork;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            if (claim != null)
            {

                if (HttpContext.Session.GetInt32(CommonConstants.SessionCart) == null)
                {
                    HttpContext.Session.SetInt32(CommonConstants.SessionCart,
                    _workContext.ShoppingCart.GetAll(u => u.ApplicationUserId == claim.Value).Count());
                }

                return View(HttpContext.Session.GetInt32(CommonConstants.SessionCart));
            }
            else
            {
                HttpContext.Session.Clear();
                return View(0);
            }
        }
    }
}
