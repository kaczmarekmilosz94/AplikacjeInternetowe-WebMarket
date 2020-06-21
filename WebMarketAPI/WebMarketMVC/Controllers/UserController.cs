using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebMarket.DataAccesLayer.Core.Abstract;
using WebMarket.Entities.Identity;
using WebMarket.Entities.Models;

namespace WebMarketMVC.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        // GET: User/Index
        public ActionResult Index()
        {
            return RedirectToAction("ShowBasket");
        }

        [HttpGet]
        public async Task<ActionResult> GetOfferedProducts()
        {
            string userId = User.Identity.GetUserId();
            var offeredProducts = await _unitOfWork.Products.GetOfferedProductsOfUserAsync(userId, 0, 10);
            if (offeredProducts == null)
                return HttpNotFound();
            return View(offeredProducts);
        }

        [HttpGet]
        public async Task<ActionResult> GetSoldProducts()
        {
            string userId = User.Identity.GetUserId();
            var soldProducts = await _unitOfWork.Products.GetSoldProductsOfUserAsync(userId, 0, 10);
            if (soldProducts == null)
                return HttpNotFound();
            return View(soldProducts);
        }

        [HttpGet]
        public ActionResult ShowBasket()
        {
            string userId = User.Identity.GetUserId();
            var basketProducts = _unitOfWork.Products.GetProductsFromBasketOfUser(userId, 0, 10);
            if (basketProducts == null)
                return HttpNotFound();
            return View(basketProducts);
        }

        [HttpGet]
        public async Task<ActionResult> AddToBasket(int? productId)
        {
            try
            {
                if (productId == null)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                var user = await _unitOfWork.Users.GetAsync(User.Identity.GetUserId());
                var product = await _unitOfWork.Products.GetAsync(productId.Value);

                if (product == null)
                    return HttpNotFound();

                if (product.SellerId == user.Id)
                    return View("Error");

                user.Basket.Products.Add(product);
                _unitOfWork.Users.Modify(user);
                await _unitOfWork.Commit();
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
            return RedirectToAction("ShowBasket");
        }

        [HttpGet]
        public async Task<ActionResult> RemoveFromBasket(int? productId)
        {
            try
            {
                if (productId == null)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                var user = await _unitOfWork.Users.GetAsync(User.Identity.GetUserId());
                var product = await _unitOfWork.Products.GetAsync(productId.Value);
                if (product == null)
                    return HttpNotFound();

                user.Basket.Products.Remove(product);
                _unitOfWork.Users.Modify(user);
                await _unitOfWork.Commit();
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }

            return RedirectToAction("ShowBasket");
        }



        [HttpGet]
        public async Task<ActionResult> ClearBasket()
        {
            try
            {
                var user = await _unitOfWork.Users.GetAsync(User.Identity.GetUserId());
               
                user.Basket.Products.Clear();
                _unitOfWork.Users.Modify(user);
                await _unitOfWork.Commit();
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }

            return RedirectToAction("ShowBasket");
        }

        [HttpGet]
        public async Task<ActionResult> GetOrders()
        {
            string userId = User.Identity.GetUserId();
            var orders = await _unitOfWork.Orders.GetOrdersWithProductsOfUserAsync(userId, 0, 10);
            if (orders == null)
                return HttpNotFound();

            return View(orders);

        }

        [HttpGet]
        public async Task<ActionResult> Order()
        {
            try
            {
                var user = await _unitOfWork.Users.GetAsync(User.Identity.GetUserId());

                if (!user.Basket.Products.Any())
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                var orderedProducts = user.Basket.Products.ToArray();

                var order = new Order()
                {
                    BuyerId = user.Id,
                    PurchaseTime = DateTime.Now,
                    Products = orderedProducts
                };
                user.Basket.Products.Clear();
                _unitOfWork.Orders.Add(order);
                await _unitOfWork.Commit();
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
            return RedirectToAction("GetOrders");
        }

    }
}
