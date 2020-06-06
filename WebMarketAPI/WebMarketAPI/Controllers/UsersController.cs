using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using WebMarket.DataAccesLayer.Core.Abstract;
using WebMarket.Entities.Models;
using WebMarketAPI.Models.InputModels;
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
        public async Task<IHttpActionResult> Get(int id)
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
        /// Add new user
        /// </summary>
        /// <param name="model">User that should be added</param>
        /// <returns>Action result</returns>
        public async Task<IHttpActionResult> Post(UserInputModel model)
        {
            if (model == null) return BadRequest("Model can not be empty.");

            try
            {
                if (ModelState.IsValid)
                {
                    var newUser = _mapper.Map<User>(model);
                    newUser.Basket = new Basket();

                    _unitOfWork.Users.Add(newUser);
                    await _unitOfWork.Commit();
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch
            {
                return InternalServerError();
            }

            return Ok("User added");
        }
        /// <summary>
        /// Update existing user
        /// </summary>
        /// <param name="id">User identification number</param>
        /// <param name="model">New version of user</param>
        /// <returns>Action result</returns>
        public async Task<IHttpActionResult> Put(int id, UserInputModel model)
        {
            if (model == null) return BadRequest("Model can not be empty.");

            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _unitOfWork.Users.GetAsync(id);
                    if (user == null) return NotFound();

                    _mapper.Map<UserInputModel, User>(model, user);

                    _unitOfWork.Users.Modify(user);

                    await _unitOfWork.Commit();
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch
            {
                return InternalServerError();
            }

            return Ok("User updated");
        }
        /// <summary>
        /// Remove existing user
        /// </summary>
        /// <param name="id">User identification number</param>
        /// <returns>Action result</returns>
        public async Task<IHttpActionResult> Delete(int id)
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
        /// Add product to user's basket
        /// </summary>
        /// <param name="id">User identification number</param>
        /// <param name="productId">Product identification number</param>
        /// <returns>Action result</returns>
        [Route("api/Users/{id}/basket/add")]
        public async Task<IHttpActionResult> AddProductToBasket(int id, int productId)
        {
            try
            {
                var user = await _unitOfWork.Users.GetAsync(id);
                if (user == null) return NotFound();

                var product = await _unitOfWork.Products.GetAsync(productId);
                if (product == null) return NotFound();

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
        public async Task<IHttpActionResult> RemoveProductFromBasket(int id, int productId)
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
        public async Task<IHttpActionResult> RemoveAllProductsFromBasket(int id)
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
        public async Task<IHttpActionResult> Order(int id)
        {
            try
            {
                var user = await _unitOfWork.Users.GetAsync(id);
                if (user == null) return NotFound();

                var order = new Order()
                {
                    BuyerId = user.Id,
                    PurchaseTime = DateTime.Now,
                    Products = user.Basket.Products
                };

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
