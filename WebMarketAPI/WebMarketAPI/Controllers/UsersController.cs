using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using WebMarket.DataAccesLayer.Core.Abstract;
using WebMarket.Entities.Models;
using WebMarketAPI.Models.OutputModels;
using AutoMapper;
using System;

namespace WebMarketAPI.Controllers
{
    /// <summary>
    /// Controller that makes operations on Users
    /// </summary>
    public class UsersController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        /// <summary>
        /// Users Controller constructor
        /// </summary>
        public UsersController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        #region Base CRUD Actions

        /// <summary>
        /// Get user of given identifier
        /// </summary>
        /// <param name="id">User identification number</param>
        /// <returns>User</returns>
        public async Task<IHttpActionResult> Get(string id)
        {
            var user = await _unitOfWork.Users.GetAsync(id);

            if (user == null) return NotFound();

            var outputUser = _mapper.Map<UserOutputModel>(user);

            return Ok(outputUser);
        }
        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns>Collection of Users</returns>
        public async Task<IHttpActionResult> GetAll()
        {
            var users = await _unitOfWork.Users.GetAllAsync();

            if (users == null) return NotFound();

            var outputUsers = new List<UserOutputModel>();

            users.ForEach(u => outputUsers.Add(_mapper.Map<UserOutputModel>(u)));

            return Ok(outputUsers);
        }        
        /// <summary>
        /// Remove existing user
        /// </summary>
        /// <param name="id">User identification number</param>
        /// <returns>Action result</returns>
        public async Task<IHttpActionResult> Delete(string id)
        {
            try
            {
                var user = await _unitOfWork.Users.GetAsync(id);
                if (user == null) return NotFound();

                _unitOfWork.Users.Remove(user);
                await _unitOfWork.Commit();
            }
            catch
            {
                return InternalServerError();
            }

            return Ok("User removed");
        }
        #endregion


       
        /// <summary>
        /// Get all products from user's offer
        /// </summary>
        /// <param name="userId">User identification number</param>
        /// <param name="pageIndex">Current page index</param>
        /// <param name="pageSize">Amount of products on single page</param>
        /// <returns>Collection of products</returns>
        [Route("api/Users/{userId}/products")]
        public async Task<IHttpActionResult> GetOfferedProductsOfUserAsync(string userId, int pageIndex = 0, int pageSize = 10)
        {
            var products = await _unitOfWork.Products.GetOfferedProductsOfUserAsync(userId, pageIndex, pageSize);

            if (products == null) return NotFound();

            var outputProducts = new List<ProductOutputModel>();

            products.ForEach(p => outputProducts.Add(_mapper.Map<ProductOutputModel>(p)));

            return Ok(outputProducts);
        }
        /// <summary>
        /// Get all products sold by user
        /// </summary>
        /// <param name="userId">User identification number</param>
        /// <param name="pageIndex">Current page index</param>
        /// <param name="pageSize">Amount of products on single page</param>
        /// <returns>Collection of products</returns>
        [Route("api/Users/{userId}/sold_products")]
        public async Task<IHttpActionResult> GetSoldProductsWithOrdersOfUserAsync(string userId, int pageIndex = 0, int pageSize = 10)
        {
            var products = await _unitOfWork.Products.GetSoldProductsOfUserAsync(userId, pageIndex, pageSize);

            if (products == null) return NotFound();

            var outputProducts = new List<ProductOutputModel>();

            products.ForEach(p => outputProducts.Add(_mapper.Map<ProductOutputModel>(p)));

            return Ok(outputProducts);
        }
        /// <summary>
        /// Get all products from user basket
        /// </summary>
        /// <param name="userId">User identification number</param>
        /// <param name="pageIndex">Current page index</param>
        /// <param name="pageSize">Amount of products on single page</param>
        /// <returns>Collection of products</returns>
        [Route("api/Users/{userId}/basket/products")]
        public IHttpActionResult GetProductsFromBasketOfUser(string userId, int pageIndex = 0, int pageSize = 10)
        {
            var products = _unitOfWork.Products.GetProductsFromBasketOfUser(userId, pageIndex, pageSize);

            if (products == null) return NotFound();

            var outputProducts = new List<ProductOutputModel>();

            products.ForEach(p => outputProducts.Add(_mapper.Map<ProductOutputModel>(p)));

            return Ok(outputProducts);
        }




        /// <summary>
        /// Get all orders of user
        /// </summary>
        /// <param name="userId">User identification number</param>
        /// <param name="pageIndex">Current page index</param>
        /// <param name="pageSize">Amount of orders on a single page</param>
        /// <returns>Collection of orders</returns>
        [Route("api/User/{userId}/orders")]
        public async Task<IHttpActionResult> GetOrdersOfUserAsync(string userId, int pageIndex = 0, int pageSize = 10)
        {
            var orders = await _unitOfWork.Orders.GetOrdersOfUserAsync(userId, pageIndex, pageSize);

            if (orders == null) return NotFound();

            var outputOrders = new List<OrderOutputModel>();

            orders.ForEach(o => outputOrders.Add(_mapper.Map<OrderOutputModel>(o)));

            return Ok(outputOrders);
        }
        /// <summary>
        /// Get all orders with products of user 
        /// </summary>
        /// <param name="userId">User identification number</param>
        /// <param name="pageIndex">Current page index</param>
        /// <param name="pageSize">Amount of orders on a single page</param>
        /// <returns>Collection of orders with products</returns>
        [Route("api/User/{userId}/orders-products")]
        public async Task<IHttpActionResult> GetOrdersWithProductsOfUserAsync(string userId, int pageIndex = 0, int pageSize = 10)
        {
            var orders = await _unitOfWork.Orders.GetOrdersWithProductsOfUserAsync(userId, pageIndex, pageSize);

            if (orders == null) return NotFound();

            var outputOrders = new List<OrderWithProductsOutputModel>();

            orders.ForEach(o => outputOrders.Add(_mapper.Map<OrderWithProductsOutputModel>(o)));


            for (int i = 0; i < orders.Count; i++)
            {
                outputOrders[i].Products = new List<ProductOutputModel>();

                foreach (var product in orders[i].Products)
                {
                    outputOrders[i].Products.Add(_mapper.Map<ProductOutputModel>(product));
                }
            }

            return Ok(outputOrders);
        }




        /// <summary>
        /// Add product to user's basket
        /// </summary>
        /// <param name="id">User identification number</param>
        /// <param name="productId">Product identification number</param>
        /// <returns>Action result</returns>
        [Route("api/Users/{id}/basket/add")]
        public async Task<IHttpActionResult> AddProductToBasket(string id, int productId)
        {
            try
            {
                var user = await _unitOfWork.Users.GetAsync(id);
                if (user == null) return NotFound();

                var product = await _unitOfWork.Products.GetAsync(productId);
                if (product == null) return NotFound();

                if (product.SellerId == user.Id)
                    return BadRequest("Can not add an own product.");


                user.Basket.Products.Add(product);

                _unitOfWork.Users.Modify(user);
                await _unitOfWork.Commit();
            }
            catch
            {
                return InternalServerError();
            }

            return Ok("Product added");
        }
        /// <summary>
        /// Remove product from user's basket
        /// </summary>
        /// <param name="id">User identification number</param>
        /// <param name="productId">Product identification number</param>
        /// <returns>Action result</returns>
        [Route("api/Users/{id}/basket/remove")]
        public async Task<IHttpActionResult> RemoveProductFromBasket(string id, int productId)
        {
            try
            {
                var user = await _unitOfWork.Users.GetAsync(id);
                if (user == null) return NotFound();

                var product = await _unitOfWork.Products.GetAsync(productId);
                if (product == null) return NotFound();

                user.Basket.Products.Remove(product);

                _unitOfWork.Users.Modify(user);

                await _unitOfWork.Commit();
            }
            catch
            {
                return InternalServerError();
            }

            return Ok("Product added");
        }
        /// <summary>
        /// Remove all products from user's basket
        /// </summary>
        /// <param name="id">User identification number</param>
        /// <returns>Action result</returns>
        [Route("api/Users/{id}/basket/remove-all")]
        public async Task<IHttpActionResult> RemoveAllProductsFromBasket(string id)
        {
            try
            {
                var user = await _unitOfWork.Users.GetAsync(id);
                if (user == null) return NotFound();

                user.Basket.Products.Clear();

                _unitOfWork.Users.Modify(user);
                await _unitOfWork.Commit();
            }
            catch
            {
                return InternalServerError();
            }

            return Ok("Product added");
        }
        /// <summary>
        /// Make an order with user's baket products
        /// </summary>
        /// <param name="id">User identification number</param>
        /// <returns>Action result</returns>
        [Route("api/Users/{id}/basket/order")]
        public async Task<IHttpActionResult> Order(string id)
        {
            try
            {
                var user = await _unitOfWork.Users.GetAsync(id);
                if (user == null) return NotFound();

                if (!user.Basket.Products.Any())
                    return BadRequest("Can not make an empty order.");  

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
                return InternalServerError();
            }

            return Ok("Order made");
        }
    }
}
