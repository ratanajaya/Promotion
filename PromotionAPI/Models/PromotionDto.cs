namespace PromotionAPI.Models
{
    public class PromotionDto
    {
        public string PromotionId { get; set; }
        public string PromoType { get; set; }
        public string ValueType { get; set; }
        public decimal? Value { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public string Item { get; set; }
        public List<string> StoreIds { get; set; }
    }
}
