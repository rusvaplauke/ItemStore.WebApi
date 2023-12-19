using System.ComponentModel.DataAnnotations;

namespace ItemStore.WebApi.Models.DTOs
{
    public class PutItemDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [Range(0.01, (double)decimal.MaxValue, ErrorMessage = "The price must be greater than 0.01.")]
        public decimal Price { get; set; }
    }
}
