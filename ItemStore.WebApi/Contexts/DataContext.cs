using ItemStore.WebApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ItemStore.WebApi.Contexts
{
    public class DataContext : DbContext
    {
        public DbSet<ItemEntity> Items { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
    }
}
