using ecommerce_api.Data.Interface;
using ecommerce_api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ecommerce_api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Password_reset>()
                .HasNoKey();
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Upload> Uploads { get; set; }
        public DbSet<Translation> Translations { get; set; }
        public DbSet<Ticket_reply> Ticket_Replies { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Taxe> Taxes { get; set; }
        public DbSet<Subscribe> Subscribes { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Seo_setting> Seo_Settings { get; set; }
        public DbSet<Seller_withdraw_request> Seller_Withdraw_Requests { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<Search> Searches { get; set; }
        public DbSet<Role_translation> Role_Translations { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Product_translation> Product_Translations { get; set; }
        public DbSet<Product_taxe> Product_Taxes { get; set; }
        public DbSet<Product_stock> Product_Stocks { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Policy> Policies { get; set; }
        public DbSet<Pickup_point_translation> Pickup_Point_Translations { get; set; }
        public DbSet<Pickup_point> Pickup_Points { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Password_reset> Password_Resets { get; set; }
        public DbSet<Page_translation> Page_Translations { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<Order_detail> Order_Details { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Messages> Messages { get; set; }
        public DbSet<Link> Links { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Home_category> Home_Categories { get; set; }
        public DbSet<General_setting> General_Settings { get; set; }
        public DbSet<Flash_deal_translation> Flash_Deal_Translations { get; set; }
        public DbSet<Flash_deal_product> Flash_Deal_Products { get; set; }
        public DbSet<Flash_deal> Flash_Deals { get; set; }
        public DbSet<Customer_product_translation> Customer_Product_Translations { get; set; }
        public DbSet<Customer_product> Customer_Products { get; set; }
        public DbSet<Customer_package_translation> Customer_Package_Translations { get; set; }
        public DbSet<Customer_package_payment> Customer_Package_Payments { get; set; }
        public DbSet<Customer_package> Customer_Packages { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Coupon_usages> Coupon_Usages { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<Commission_history> Commission_Histories { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<City_translation> City_Translations { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Category_translation> Category_Translations { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Business_setting> Business_Settings { get; set; }
        public DbSet<Brand_translation> Brand_Translations { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Blog_category> Blog_Categories { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<Attribute_translation> Attribute_Translations { get; set; }
        public DbSet<Attributes> Attributes { get; set; }
        public DbSet<App_setting> App_Settings { get; set; }
        public DbSet<Addresses> Addresses { get; set; }
        public DbSet<Addon> Addons { get; set; }

    }
}