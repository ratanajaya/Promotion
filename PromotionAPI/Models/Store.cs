using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PromotionAPI.Models
{
    [Table("Store", Schema = "dbo")]
    public class Store
    {
        [Key]
        public string StoreId { get; set; }
        public string Name { get; set; }
    }
}
