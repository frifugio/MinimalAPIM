using Microsoft.EntityFrameworkCore;

namespace MinimalAPIM_Test.Models
{
    public class RestaurantOrderDb : DbContext
    {
        public RestaurantOrderDb(DbContextOptions<RestaurantOrderDb> options)
            : base(options) { }

        public DbSet<Order> Orders => Set<Order>();
    }
}
