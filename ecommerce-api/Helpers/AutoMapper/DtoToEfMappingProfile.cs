using ecommerce_api.DTO;
using ecommerce_api.Models;
using AutoMapper;
using System;
using System.Linq;
using CodeUtility;

namespace ecommerce_api.Helpers.AutoMapper
{
    public class DtoToEfMappingProfile : Profile
    {
        public DtoToEfMappingProfile()
        {
            var ct = DateTime.Now;
            CreateMap<UserForDetailDto, User>();
            CreateMap<CategoryDto, Category>();
            CreateMap<ProductDto, Product>();
            CreateMap<BrandDto, Brand>();
            CreateMap<Blog_categoryDto, Blog_category>();
            CreateMap<BlogDto, Blog>();
            CreateMap<WishlistDto, Wishlist>();
            CreateMap<PolicyDto, Policy>();
            CreateMap<AddressesDto, Addresses>();
            CreateMap<CountryDto, Country>();
            CreateMap<SubscribeDto, Subscribe>();
            CreateMap<ShopDto, Shop>();
            CreateMap<Business_settingDto, Business_setting>();
            CreateMap<ReviewDto, Review>();
            CreateMap<ProductStockDto, Product_stock>();
            CreateMap<TaxeDto, Taxe>();
            CreateMap<ProductTaxeDto, Product_taxe>();
            CreateMap<AttributeDto, Attributes>();
            CreateMap<BannerDto, Banner>();
            CreateMap<CityDto, City>();
            CreateMap<CommissionHistoryDto, Commission_history>();
            CreateMap<ConversationDto, Conversation>();
            CreateMap<CouponUsageDto, Coupon_usages>();
            CreateMap<CouponDto, Coupon>();
            CreateMap<CurrencyDto, Currency>();
            CreateMap<CustomerPackagePaymentDto, Customer_package_payment>();
            CreateMap<CustomerPackageDto, Customer_package>();
            CreateMap<CustomerProductDto, Customer_product>();
            CreateMap<CustomerDto, Customer>();
            CreateMap<FlashDealProductDto, Flash_deal_product>();
            CreateMap<FlashDealDto, Flash_deal>();
            CreateMap<GeneralSettingDto, General_setting>();
            CreateMap<HomeCategoryDto, Home_category>();
            CreateMap<LinkDto, Link>();
            CreateMap<MessageDto, Messages>();
            CreateMap<OrderDto, Order>();
            CreateMap<OrderDetailDto, Order_detail>();
            CreateMap<PageDto, Page>();
            CreateMap<PaymentDto, Payment>();
            CreateMap<PickupPointDto, Pickup_point>();
            CreateMap<SellerWithdrawRequestDto, Seller_withdraw_request>();
            CreateMap<SeoSettingDto, Seo_setting>();
            CreateMap<SliderDto, Slider>();
            CreateMap<StaffDto, Staff>();
            CreateMap<TicketDto, Ticket>();
        }
    }
}