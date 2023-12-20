using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ItemStore.WebApi.Models.Entities
{
    public class ItemEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; } 

        public string name { get; set; } = "";

        public decimal price { get; set; } = 0;
        public DateTime created_at { get; set; } = DateTime.UtcNow;
        public Boolean is_deleted { get; set; } = false;
    }
}
