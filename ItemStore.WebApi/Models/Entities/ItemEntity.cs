using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ItemStore.WebApi.Models.Entities
{
    public class ItemEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 

        public string Name { get; set; } = "";

        public decimal Price { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Boolean IsDeleted { get; set; } = false;
    }
}
