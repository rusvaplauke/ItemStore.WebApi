using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ItemStore.WebApi.Models.DTOs.ShopDtos
{
    public class PostShopDto
    {
        public string Name { get; set; } = "";

        public string Address { get; set; } = "";
    }
}
