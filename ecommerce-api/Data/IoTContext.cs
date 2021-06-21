using ecommerce_api.Data.MongoModels;
using ecommerce_api.Models;
using Microsoft.EntityFrameworkCore;

namespace ecommerce_api.Data
{
    public class IoTContext : DbContext
    {
        public IoTContext(DbContextOptions<IoTContext> options) : base(options) { }
        public DbSet<RawData> RawData { get; set; }
      

        

    }
}