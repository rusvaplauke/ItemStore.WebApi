using System.ComponentModel.DataAnnotations;

namespace ItemStore.WebApi.Models.DTOs
{
    public class PostItemDto
    {
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; }

        // [Range(0.01, (double)decimal.MaxValue, ErrorMessage = "The price must be greater than 0.01.")]
        public decimal Price { get; set; }
    }
}
