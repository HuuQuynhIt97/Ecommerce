using ecommerce_api.Data;
using ecommerce_api.Helpers;
using ecommerce_api.SignalrHub;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Microsoft.AspNetCore.CookiePolicy;
using ecommerce_api.Helpers.Extensions;
using ecommerce_api.SchedulerHelper;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using ecommerce_api._Repositories.Repositories;
using ecommerce_api._Repositories.Interface;
using ecommerce_api._Services.Services;
using ecommerce_api._Services.Interface;

namespace ecommerce_api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var appsettings = Configuration.GetSection("Appsettings").Get<Appsettings>();
            //Install repository and unitofwork
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddDatabaseExtention(Configuration)
                    .AddRepositoriesExtention()
                    .AddServicesExtention();
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = "localhost";
                options.InstanceName = "IoT";
            });
            services.AddSignalR();

            services.AddLogging();
          
            services.AddControllers()
                    .AddNewtonsoftJson(options =>
                                    {
                                        options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Unspecified;
                                        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                                    });
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.WithOrigins(appsettings.CorsPolicy
                    ) //register for client
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
            services.AddTransient<DbInitializer>();
            //Auto Mapper
            services.AddAutoMapperExtention();

            services.AddAuthenticationWithSwaggerExtention(Configuration);

            services.AddHttpClientExtention(Configuration);

            services.AddShedulerExtention(Configuration);

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = @"wwwroot/ClientApp";
            });

            // Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAddonRepository, AddonRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IAppSettingRepository, AppSettingRepository>();
            services.AddScoped<IAttributeRepository, AttributeRepository>();
            services.AddScoped<IAttributeTranslationRepository, AttributeTranslationRepository>();
            services.AddScoped<IBannerRepository, BannerRepository>();
            services.AddScoped<IBlogCategoryRepository, BlogCategoryRepository>();
            services.AddScoped<IBlogRepository, BlogRepository>();
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<IBrandTranslationRepository, BrandTranslationRepository>();
            services.AddScoped<IBusinessSettingRepository, BusinessSettingRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryTranslationRepository, CategoryTranslationRepository>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<ICityTranslationRepository, CityTranslationRepository>();
            services.AddScoped<IColorRepository, ColorRepository>();
            services.AddScoped<ICommissionHistorieRepository, CommissionHistorieRepository>();
            services.AddScoped<IConversationRepository, ConversationRepository>();
            services.AddScoped<ICountrieRepository, CountrieRepository>();
            services.AddScoped<ICouponRepository, CouponRepository>();
            services.AddScoped<ICouponUsageRepository, CouponUsageRepository>();
            services.AddScoped<ICurrencieRepository, CurrencieRepository>();
            services.AddScoped<ICustomerPackagePaymentRepository, CustomerPackagePaymentRepository>();
            services.AddScoped<ICustomerPackageRepository, CustomerPackageRepository>();
            services.AddScoped<ICustomerPackageTranslationRepository, CustomerPackageTranslationRepository>();
            services.AddScoped<ICustomerProductRepository, CustomerProductRepository>();
            services.AddScoped<ICustomerProductTranslationRepository, CustomerProductTranslationRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IFlashDealProductRepository, FlashDealProductRepository>();
            services.AddScoped<IFlashDealRepository, FlashDealRepository>();
            services.AddScoped<IFlashDealTranslationRepository, FlashDealTranslationRepository>();
            services.AddScoped<IGeneralSettingRepository, GeneralSettingRepository>();
            services.AddScoped<IHomeCategorieRepository, HomeCategorieRepository>();
            services.AddScoped<ILanguageRepository, LanguageRepository>();
            services.AddScoped<ILinkRepository, LinkRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IPageRepository, PageRepository>();
            services.AddScoped<IPageTranslationRepository, PageTranslationRepository>();
            services.AddScoped<IPasswordResetRepository, PasswordResetRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IPickupPointRepository, PickupPointRepository>();
            services.AddScoped<IPickupPointTranslationRepository, PickupPointTranslationRepository>();
            services.AddScoped<IPolicieRepository, PolicieRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductStockRepository, ProductStockRepository>();
            services.AddScoped<IProductTaxeRepository, ProductTaxeRepository>();
            services.AddScoped<IProductTranslationRepository, ProductTranslationRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IRoleTranslationRepository, RoleTranslationRepository>();
            services.AddScoped<ISearchRepository, SearchRepository>();
            services.AddScoped<ISellerRepository, SellerRepository>();
            services.AddScoped<ISellerWithdrawRequestRepository, SellerWithdrawRequestRepository>();
            services.AddScoped<ISeoSettingRepository, SeoSettingRepository>();
            services.AddScoped<IShopRepository, ShopRepository>();
            services.AddScoped<ISliderRepository, SliderRepository>();
            services.AddScoped<IStaffRepository, StaffRepository>();
            services.AddScoped<ISubscribeRepository, SubscribeRepository>();
            services.AddScoped<ITaxRepository, TaxRepository>();
            services.AddScoped<ITicketRepository, TicketRepository>();
            services.AddScoped<ITranslationRepository, TranslationRepository>();
            services.AddScoped<IUploadRepository, UploadRepository>();
            services.AddScoped<IWalletRepository, WalletRepository>();
            services.AddScoped<IWishlistRepository, WishlistRepository>();
            services.AddScoped<IAuthRepository, AuthRepository>();

            // Services
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataContext dataContext)
        {

            dataContext.Database.Migrate();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Digital mixing room system");
            });
            app.UseCors("CorsPolicy");
            app.UseCors(x => x
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                           //    .SetIsOriginAllowed(origin => true) // allow any origin
                           .AllowCredentials()); // allow credentials
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseDefaultFiles();
            app.UseStaticFiles();
           
            app.UseAuthentication()
               .UseCookiePolicy(new CookiePolicyOptions
               {
                    HttpOnly = HttpOnlyPolicy.Always
               });

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ECHub>("/ec-hub");

            });
            app.UseSpaStaticFiles();
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = @"wwwroot/ClientApp";
                //if (env.IsDevelopment())
                //{
                //    spa.Options.SourcePath = @"../dmr-app";
                //    spa.UseAngularCliServer(npmScript: "start");
                //}
            });

           
        }
    }
}
