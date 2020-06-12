using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using WebMarket.DataAccesLayer.Core.Abstract;
using WebMarket.Entities.Models;
using WebMarketAPI.Models.InputModels;
using WebMarketAPI.Models.OutputModels;
using AutoMapper;
using System.Web;
using System.Net.Http;
using System.IO;
using System;

namespace WebMarketAPI.Controllers
{
    /// <summary>
    /// Controller that makes operations on Products
    /// </summary>
    public class ProductsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        /// <summary>
        /// Products Controller constructor
        /// </summary>
        public ProductsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        #region Base CRUD Actions
        /// <summary>
        /// Get product of given identifier
        /// </summary>
        /// <param name="id">Product identification number</param>
        /// <returns>Product</returns>
        public async Task<IHttpActionResult> Get(int id) 
        {
            var product = await _unitOfWork.Products.GetAsync(id);

            if (product == null) return NotFound();

            var outputProduct = _mapper.Map<ProductOutputModel>(product);

            return Ok(outputProduct);
        }
        /// <summary>
        /// Get all products
        /// </summary>
        /// <returns>Collection of Products</returns>
        public async Task<IHttpActionResult> GetAll()
        {
            var products = await _unitOfWork.Products.GetAllAsync();

            if (products == null) return NotFound();

            var outputProducts = new List<ProductOutputModel>();

            products.ForEach(p => outputProducts.Add(_mapper.Map<ProductOutputModel>(p)));

            return Ok(outputProducts);
        }
        /// <summary>
        /// Add new product
        /// </summary>
        /// <param name="model">Product that should be added</param>
        /// <returns>Action result</returns>
        public async Task<IHttpActionResult> Post(ProductInputModel model) 
        {
            if (model == null) return BadRequest("Model can not be empty.");
           
            try
            {
                if (ModelState.IsValid)
                {
                    _unitOfWork.Products.Add(_mapper.Map<Product>(model));
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

            return Ok("Product added");
        }
        /// <summary>
        /// Update existing product
        /// </summary>
        /// <param name="id">Product identification number</param>
        /// <param name="model">New version of product</param>
        /// <returns>Action result</returns>
        public async Task<IHttpActionResult> Put(int id, ProductInputModel model)
        {
            if (model == null) return BadRequest("Model can not be empty.");
           
            try
            {
                if (ModelState.IsValid)
                {
                    var product = await _unitOfWork.Products.GetAsync(id);
                    if (product == null) return NotFound();

                    _mapper.Map<ProductInputModel, Product>(model, product);

                    _unitOfWork.Products.Modify(product);

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

            return Ok("Product updated");
        }
        /// <summary>
        /// Remove existing product
        /// </summary>
        /// <param name="id">Product identification number</param>
        /// <returns>Action result</returns>
        public async Task<IHttpActionResult> Delete(int id) 
        {
            try
            {
                var product = await _unitOfWork.Products.GetAsync(id);
                if (product == null) return NotFound();

                _unitOfWork.Products.Remove(product);
                await _unitOfWork.Commit();
            }
            catch
            {
                return InternalServerError();
            }

            return Ok("Product removed");
        }
        #endregion       
    }
}
