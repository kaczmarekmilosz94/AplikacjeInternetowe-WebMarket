using System.Collections.Generic;
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
    /// Controller that makes operations on Orders
    /// </summary>
    public class OrdersController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        /// <summary>
        /// Orders Controller constructor
        /// </summary>
        public OrdersController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        #region Base CRUD Actions

        /// <summary>
        /// Get order of given identifier
        /// </summary>
        /// <param name="id">Order identification number</param>
        /// <returns>Order</returns>
        public async Task<IHttpActionResult> Get(int id)
        {
            var order = await _unitOfWork.Orders.GetAsync(id);

            if (order == null) return NotFound();

            var outputOrder = _mapper.Map<ProductOutputModel>(order);

            return Ok(outputOrder);
        }
        /// <summary>
        /// Get all orders
        /// </summary>
        /// <returns>Collection of Orders</returns>
        public async Task<IHttpActionResult> GetAll()
        {
            var orders = await _unitOfWork.Orders.GetAllAsync();

            if (orders == null) return NotFound();

            var outputOrders = new List<OrderOutputModel>();

            orders.ForEach(o => outputOrders.Add(_mapper.Map<OrderOutputModel>(o)));

            return Ok(outputOrders);
        }        
        /// <summary>
        /// Remove existing order
        /// </summary>
        /// <param name="id">Order identification number</param>
        /// <returns>Action result</returns>
        public async Task<IHttpActionResult> Delete(int id)
        {
            try
            {
                var order = await _unitOfWork.Orders.GetAsync(id);
                if (order == null) return NotFound();

                _unitOfWork.Orders.Remove(order);
                await _unitOfWork.Commit();
            }
            catch
            {
                return InternalServerError();
            }

            return Ok("Order removed");
        }
        #endregion

        /// <summary>
        /// Get all orders of user
        /// </summary>
        /// <param name="userId">User identification number</param>
        /// <param name="pageIndex">Current page index</param>
        /// <param name="pageSize">Amount of orders on a single page</param>
        /// <returns>Collection of orders</returns>
        [Route("api/User/{userId}/orders")]
        public async Task<IHttpActionResult> GetOrdersOfUserAsync(int userId, int pageIndex, int pageSize)
        {
            var orders = await _unitOfWork.Orders.GetOrdersOfUserAsync( userId,  pageIndex,  pageSize);

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
        public async Task<IHttpActionResult> GetOrdersWithProductsOfUserAsync(int userId, int pageIndex, int pageSize) 
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
        /// Get order with products of user 
        /// </summary>
        /// <param name="id">Order identification number</param>
        /// <returns>Order with products</returns>
        [Route("api/User/orders-products/{id}")]
        public IHttpActionResult GetOrderWithProducts(int id) 
        {
            var order = _unitOfWork.Orders.GetOrderWithProducts(id);

            if (order == null) return NotFound();

            var outputOrder = _mapper.Map<OrderWithProductsOutputModel>(order);


            foreach (var product in order.Products)
            {
                outputOrder.Products.Add(_mapper.Map<ProductOutputModel>(product));
            }

            return Ok(outputOrder);
        }
    }
}
