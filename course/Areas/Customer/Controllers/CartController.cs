using course.Repository.Irepository;
using courseModels;
using CourseUtilty;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using System.Security.Claims;

namespace course.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
      //  [AllowAnonymous] //for unatchirzed

        private readonly IUnitOfWork _db;
        [BindProperty]
        public ShopingCartModel shopVM { get; set; }

       
        public CartController(IUnitOfWork db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            shopVM = new ShopingCartModel()
            {
                ListCart = _db.ShoppingCart.GetAll(u => u.ApplicationUserId == claim.Value, includeProperties: "Product"),
                OrderHeader =new()
            };
            foreach(var cart in shopVM.ListCart)
            {
                cart.Price = GetPrice(cart.count, cart.Product.Price, cart.Product.Price50, cart.Product.Price100);
                shopVM.OrderHeader.OrderTotal += (cart.Price * cart.count);
            }
            return View(shopVM);
        }

        public IActionResult Summary()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            shopVM = new ShopingCartModel()
            {
                ListCart = _db.ShoppingCart.GetAll(u => u.ApplicationUserId == claim.Value, includeProperties: "Product"),
                OrderHeader =new()
            };

            shopVM.OrderHeader.ApplicationUser = _db.ApplicationUser.GetFirst(u =>u.Id == claim.Value);
           shopVM.OrderHeader.Name = shopVM.OrderHeader.ApplicationUser.Name; 
            shopVM.OrderHeader.PhoneNumber = shopVM.OrderHeader.ApplicationUser.PhoneNumber; 
            shopVM.OrderHeader.StreetAddress = shopVM.OrderHeader.ApplicationUser.streetAddress;
            shopVM.OrderHeader.State = shopVM.OrderHeader.ApplicationUser.State;
            shopVM.OrderHeader.PostalCode = shopVM.OrderHeader.ApplicationUser.PostalCode;
            shopVM.OrderHeader.City = shopVM.OrderHeader.ApplicationUser.City;




            foreach (var cart in shopVM.ListCart)
            {
                cart.Price = GetPrice(cart.count, cart.Product.Price, cart.Product.Price50, cart.Product.Price100);
                shopVM.OrderHeader.OrderTotal += (cart.Price * cart.count);
            }
            


            return View(shopVM);
        }


        [HttpPost]
        [ActionName("Summary")]
        [ValidateAntiForgeryToken]
        public IActionResult SummaryPost(ShopingCartModel shopingVM)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);


            shopingVM.ListCart = _db.ShoppingCart.GetAll(u => u.ApplicationUserId == claim.Value, includeProperties: "Product");

            shopingVM.OrderHeader.PaymentStatus = SD.PaymentStatusPending;
            shopingVM.OrderHeader.OrderStatus = SD.StatusPending;
            shopingVM.OrderHeader.OrderDate = DateTime.Now;
            shopingVM.OrderHeader.ApplicationUserId = claim.Value;
            

            foreach (var cart in shopingVM.ListCart)
            {
                cart.Price = GetPrice(cart.count, cart.Product.Price, cart.Product.Price50, cart.Product.Price100);
                shopingVM.OrderHeader.OrderTotal += (cart.Price * cart.count);
            }
            //stripe setting

            _db.OrderHeader.Add(shopingVM.OrderHeader);
            _db.Save();
            foreach (var cart in shopingVM.ListCart)
            {
                OrderDetails orderDetail = new()
                {
                    ProductId = cart.ProductId,
                    OrderId = shopingVM.OrderHeader.Id,
                    Price = cart.Price,
                    Count = cart.count
                };
                _db.OrderDetail.Add(orderDetail);
                _db.Save();
            }


            var domain = "https://localhost:44336/";
            var options = new SessionCreateOptions
            {

                PaymentMethodTypes = new List<string>
                {
                    "card",
                },
                LineItems = new List<SessionLineItemOptions>(),
                
        
                Mode = "payment",
                SuccessUrl = domain + $"customer/cart/OrderConfirmation?id={shopingVM.OrderHeader.Id}",
                CancelUrl = domain + $"customer/cart/Index",
            };
            foreach(var item in shopingVM.ListCart)
            {
               

               var sessionLineItem = new SessionLineItemOptions
                {

                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(item.Price * 100),
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Product.Title,
                        },

                    },
                    Quantity = item.count,
                };
                options.LineItems.Add(sessionLineItem);
            }

            var service = new SessionService();
            Session session = service.Create(options);
            shopingVM.OrderHeader.SessionId = session.Id;

            shopingVM.OrderHeader.PaymentIntentId = session.PaymentIntentId;
            _db.OrderHeader.Save();
            //  _db.OrderHeader.UpdateStripePaymentId(shopingVM.OrderHeader.Id, session.Id, session.PaymentIntentId);
            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
            return RedirectToAction("OrderConfirmation", "Cart", new { id = shopingVM.OrderHeader.Id });




        }

        public IActionResult OrderConfirmation(int id)
        {
            OrderHeader orderHeader = _db.OrderHeader.GetFirst(u => u.Id == id);
            var service = new SessionService();
            Session session = service.Get(orderHeader.SessionId);
            //check stripe is succesful paymetn
            if(session.PaymentStatus.ToLower() == "paid")
            {
                _db.OrderHeader.UpdateStatus(id, SD.StatusApproved, SD.PaymentStatusApproved);
                _db.Save();
            }
            List<ShopingCart>shoopVM = _db.ShoppingCart.GetAll(u=>u.ApplicationUserId ==orderHeader.ApplicationUserId).ToList();
            _db.ShoppingCart.RemoveRange(shoopVM);
            _db.Save();
            return View(id);
        }

        public IActionResult Plus(int cartId)
        {
            var cart = _db.ShoppingCart.GetFirst(u=>u.Id == cartId);
            _db.ShoppingCart.IncreamentCount(cart, 1);
            _db.Save();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Remove(int cartId)
        {
            var cart = _db.ShoppingCart.GetFirst(u => u.Id == cartId);
            _db.ShoppingCart.Remove(cart);
            _db.Save();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Minus(int cartId)
        {
            
            var cart = _db.ShoppingCart.GetFirst(u => u.Id == cartId);
            if (cart.count <= 1)
            {
                _db.ShoppingCart.Remove(cart);
            }
            else
            {

                _db.ShoppingCart.DecrementCount(cart, 1);
            }
            _db.Save();
            return RedirectToAction(nameof(Index));
        }

        private double GetPrice(double quantity,double price,double price50,double price100)
        {
            if (quantity <= 50)
            {
                return price;
            }
            else if(quantity <=100)
            {
                return price50;
            }
            return price100;

        }
    }
}
