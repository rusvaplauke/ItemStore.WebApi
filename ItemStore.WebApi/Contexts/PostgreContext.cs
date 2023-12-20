using ItemStore.WebApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ItemStore.WebApi.Contexts
{
    public class PostgreContext : DbContext
    {
        public DbSet<ItemEntity> items { get; set; }

        public PostgreContext(DbContextOptions<PostgreContext> options) : base(options)
        {

        }
    }
}
