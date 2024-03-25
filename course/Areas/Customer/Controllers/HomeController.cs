
using course.Repository.Irepository;
using courseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;
using System.Security.Claims;

namespace course.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _db;

        public HomeController(ILogger<HomeController> logger,IUnitOfWork db)
        {
            _logger = logger;
            _db = db;

        }

        public IActionResult Index()
        {
            IEnumerable<Product> productList = _db.ProductType.GetAll();
            return View(productList);

        }

        public IActionResult Details(int productId)
        {
            ShopingCart Cartobj = new()
            {
                count = 1,
                ProductId = productId,
                Product = _db.ProductType.GetFirst(u => u.Id == productId),
        };
            return View(Cartobj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize] // only ahutoirzed pperson will get this page
        public IActionResult Details(ShopingCart shopingCart)
        {
            var claimsIdentity =(ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim == null)
            {

                return RedirectToAction(nameof(Index));
            }

            shopingCart.ApplicationUserId = claim.Value;

            ShopingCart cart = _db.ShoppingCart.GetFirst(
                u=>u.ApplicationUserId == claim.Value && u.ProductId ==shopingCart.ProductId);
            if(cart == null)
            {
                _db.ShoppingCart.Add(shopingCart);
            }
            else
            {
                _db.ShoppingCart.IncreamentCount(cart, shopingCart.count);
            }

          


           
            _db.Save();
           return  RedirectToAction(nameof(Index)); //it search index action
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