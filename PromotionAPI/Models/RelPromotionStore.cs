using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PromotionAPI.Models
{
    [Table("RelPromotionStore", Schema = "dbo")]
    public class RelPromotionStore
    {
        [Key, Column(Order = 0)]
        [ForeignKey("Promotion")]
        public string PromotionId { get; set; }
        public virtual Promotion Promotion { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey("Store")]
        public string StoreId { get; set; }
        public virtual Store Store { get; set; }
    }
}
