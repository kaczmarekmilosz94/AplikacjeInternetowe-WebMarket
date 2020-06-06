using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using WebMarket.DataAccesLayer.Core.Abstract;
using WebMarket.Entities.Models;
using WebMarketAPI.Models.InputModels;
using WebMarketAPI.Models.OutputModels;
using AutoMapper;

namespace WebMarketAPI.Controllers
{
    /// <summary>
    /// Controller that makes operations on Categories
    /// </summary>
    public class CategoriesController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        /// <summary>
        /// Categories Controller constructor
        /// </summary>
        public CategoriesController(IUnitOfWork unitOfWork, IMapper mapper)
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
            var category = await _unitOfWork.Categories.GetAsync(id);

            if (category == null) return NotFound();

            var categoryProduct = _mapper.Map<CategoryOutputModel>(category);

            return Ok(categoryProduct);
        }
        /// <summary>
        /// Get all categories
        /// </summary>
        /// <returns>Collection of Categories</returns>
        public async Task<IHttpActionResult> GetAll()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync();

            if (categories == null) return NotFound();

            var outputCategories = new List<CategoryOutputModel>();

            categories.ForEach(p => outputCategories.Add(_mapper.Map<CategoryOutputModel>(p)));

            return Ok(outputCategories);
        }
        /// <summary>
        /// Add new category
        /// </summary>
        /// <param name="model">Category that should be added</param>
        /// <returns>Action result</returns>
        public async Task<IHttpActionResult> Post(CategoryInputModel model)
        {
            if (model == null) return BadRequest("Model can not be empty.");

            try
            {
                if (ModelState.IsValid)
                {
                    _unitOfWork.Categories.Add(_mapper.Map<Category>(model));
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

            return Ok("Category added");
        }
        /// <summary>
        /// Update existing category
        /// </summary>
        /// <param name="id">Category identification number</param>
        /// <param name="model">New version of category</param>
        /// <returns>Action result</returns>
        public async Task<IHttpActionResult> Put(int id, CategoryInputModel model)
        {
            if (model == null) return BadRequest("Model can not be empty.");

            try
            {
                if (ModelState.IsValid)
                {
                    var category = await _unitOfWork.Categories.GetAsync(id);
                    if (category == null) return NotFound();

                    _mapper.Map<CategoryInputModel, Category>(model, category);

                    _unitOfWork.Categories.Modify(category);

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

            return Ok("Category updated");
        }
        /// <summary>
        /// Remove existing category
        /// </summary>
        /// <param name="id">Category identification number</param>
        /// <returns>Action result</returns>
        public async Task<IHttpActionResult> Delete(int id)
        {
            try
            {
                var category = await _unitOfWork.Categories.GetAsync(id);
                if (category == null) return NotFound();

                _unitOfWork.Categories.Remove(category);
                await _unitOfWork.Commit();
            }
            catch
            {
                return InternalServerError();
            }

            return Ok("Category removed");
        }
        #endregion
    }
}
