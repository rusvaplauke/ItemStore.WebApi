using System.ComponentModel.DataAnnotations;

namespace ItemStore.WebApi.Models.Entities
{
    public class Item
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }
        public DateTime Created_at { get; set; }
        public Boolean Is_deleted { get; set; }
    }
}
