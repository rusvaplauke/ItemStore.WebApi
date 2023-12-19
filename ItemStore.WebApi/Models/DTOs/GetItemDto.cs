using System.ComponentModel.DataAnnotations;

namespace ItemStore.WebApi.Models.DTOs
{
    public class GetItemDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}
