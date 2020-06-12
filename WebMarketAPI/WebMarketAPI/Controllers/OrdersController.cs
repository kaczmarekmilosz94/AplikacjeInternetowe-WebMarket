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

            var outputOrder = _mapper.Map<OrderOutputModel>(order);

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

                foreach (var product in order.Products)
                {
                    product.Order = null;
                }

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
        /// Get details of an order
        /// </summary>
        /// <param name="id">Order identification number</param>
        /// <returns>Order with products</returns>
        [Route("api/Order/{id}/products")]
        public IHttpActionResult GetOrderWithProducts(int id) 
        {
            var order = _unitOfWork.Orders.GetOrderWithProducts(id);

            if (order == null) return NotFound();

            var outputOrder = _mapper.Map<OrderWithProductsOutputModel>(order);
            outputOrder.Products = new List<ProductOutputModel>();

            foreach (var product in order.Products)
            {
                outputOrder.Products.Add(_mapper.Map<ProductOutputModel>(product));
            }

            return Ok(outputOrder);
        }
    }
}
