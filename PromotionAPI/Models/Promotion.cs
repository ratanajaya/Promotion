using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PromotionAPI.Models
{
    [Table("Promotion", Schema = "dbo")]
    public class Promotion
    {
        [Key]
        public string PromotionId { get; set; }
        public string PromoType { get; set; }
        public string ValueType { get; set; }
        public decimal? Value { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public string Item { get; set; }
        public ICollection<RelPromotionStore> RelPromotionStores { get; set; }
    }
}
