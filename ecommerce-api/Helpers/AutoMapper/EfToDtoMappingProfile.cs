using System;
using ecommerce_api.DTO;
using ecommerce_api.Models;
using AutoMapper;
using System.Linq;
using System.Collections.Generic;
using ecommerce_api.Constants;
using CodeUtility;

namespace ecommerce_api.Helpers.AutoMapper
{
    public class EfToDtoMappingProfile : Profile
    {
        char[] alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

        public EfToDtoMappingProfile()
        {
           CreateMap<Category, CategoryDto>();
           CreateMap<Product, ProductDto>();
           CreateMap<Brand, BrandDto>();
           CreateMap<Blog_category, Blog_categoryDto>();
           CreateMap<Blog, BlogDto>();
           CreateMap<Wishlist, WishlistDto>();
           CreateMap<Policy, PolicyDto>();
           CreateMap<Addresses, AddressesDto>();
           CreateMap<Country, CountryDto>();
           CreateMap<Subscribe, SubscribeDto>();
           CreateMap<Shop, ShopDto>();
           CreateMap<Business_setting, Business_settingDto>();
           CreateMap<Review, ReviewDto>();
           CreateMap<Product_stock, ProductStockDto>();
           CreateMap<Taxe, TaxeDto>();
           CreateMap<Product_taxe, ProductTaxeDto>();
           CreateMap<Attributes, AttributeDto>();
           CreateMap<Banner, BannerDto>();
           CreateMap<City, CityDto>();
           CreateMap<Commission_history, CommissionHistoryDto>();
           CreateMap<Conversation, ConversationDto>();
           CreateMap<Coupon_usages, CouponUsageDto>();
           CreateMap<Coupon, CouponDto>();
           CreateMap<Currency, CountryDto>();
           CreateMap<Customer_package_payment, CustomerPackagePaymentDto>();
           CreateMap<Customer_package, CustomerPackageDto>();
           CreateMap<Customer_product, CustomerProductDto>();
           CreateMap<Customer, CustomerDto>();
           CreateMap<Flash_deal_product, FlashDealProductDto>();
           CreateMap<Flash_deal, FlashDealDto>();
           CreateMap<General_setting, GeneralSettingDto>();
        }
    }
}