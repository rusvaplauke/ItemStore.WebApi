using System.ComponentModel.DataAnnotations;

namespace ItemStore.WebApi.Models.DTOs.ItemDtos;

public class PutItemDto
{
    [Range(1, int.MaxValue - 1, ErrorMessage = "The id must be a positive integer.")]
    public int Id { get; set; }

    [StringLength(50, MinimumLength = 2)]
    public string Name { get; set; }

    public decimal Price { get; set; }
}
