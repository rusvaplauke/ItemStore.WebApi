using System.ComponentModel.DataAnnotations;

namespace ItemStore.WebApi.Models.DTOs.ItemDtos;

public class PostItemDto
{
    [StringLength(50, MinimumLength = 2)]
    public string Name { get; set; }

    public decimal Price { get; set; }
}
