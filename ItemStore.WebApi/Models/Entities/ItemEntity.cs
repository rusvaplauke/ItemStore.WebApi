using System.ComponentModel.DataAnnotations;

namespace ItemStore.WebApi.Models.Entities
{
    public class ItemEntity
    {
        public int Id { get; set; } //= 0;

        public string Name { get; set; } = "";

        public decimal Price { get; set; } = 0;
        public DateTime Created_at { get; set; } = DateTime.UtcNow;
        public Boolean Is_deleted { get; set; } = false;
    }
}
