using AutoMapper;
using WebMarket.Entities.Models;
using WebMarketAPI.Models.InputModels;
using WebMarketAPI.Models.OutputModels;

namespace WebMarketAPI.App_Start
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region Product Mapping

            CreateMap<ProductInputModel, Product>()
                .ForMember(x => x.Id, d => d.Ignore())
                .ForMember(x => x.Baskets, d => d.Ignore())
                .ForMember(x => x.Category, d => d.Ignore())
                .ForMember(x => x.Seller, d => d.Ignore())
                .ForMember(x => x.OrderId, d => d.Ignore())
                .ForMember(x => x.Order, d => d.Ignore());

            CreateMap<Product, ProductOutputModel>()
                .ForMember(x => x.CategoryName, d => d.MapFrom(s => s.Category.Name))
                .ForMember(x => x.SellerName, d => d.MapFrom(s => s.Seller.Username));

            #endregion

            #region User Mapping

            CreateMap<UserInputModel, User>()
                .ForMember(x => x.Id, d => d.Ignore())
                .ForMember(x => x.IsAdmin, d => d.Ignore())
                .ForMember(x => x.Orders, d => d.Ignore())
                .ForMember(x => x.Products, d => d.Ignore())
                .ForMember(x => x.Basket, d => d.Ignore());

            CreateMap<User, UserOutputModel>();

            #endregion

            #region Order Mapping

            CreateMap<Order, OrderOutputModel>();
            CreateMap<Order, OrderWithProductsOutputModel>()
                .ForMember(o => o.Products, d => d.Ignore());

            #endregion

            #region Category Mapping

            CreateMap<CategoryInputModel, Category>()
                .ForMember(c => c.Id, d => d.Ignore())
                .ForMember(c => c.Products, d => d.Ignore());

            CreateMap<Category, CategoryOutputModel>();

            #endregion
        }
    }
}