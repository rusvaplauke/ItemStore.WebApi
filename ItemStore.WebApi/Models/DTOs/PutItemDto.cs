using System.ComponentModel.DataAnnotations;

namespace ItemStore.WebApi.Models.DTOs
{
    public class PutItemDto
    {
        [Range(1, int.MaxValue-1, ErrorMessage = "The id must be a positive integer.")]
        public int Id { get; set; }
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; }

        // [Range(0.01, (double)decimal.MaxValue, ErrorMessage = "The price must be greater than 0.01.")]
        public decimal Price { get; set; }
    }
}
