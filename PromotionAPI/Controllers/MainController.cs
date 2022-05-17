using Microsoft.AspNetCore.Mvc;
using PromotionAPI.Models;
using PromotionAPI.Services;

namespace PromotionAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MainController : ControllerBase
    {
        AppDbContext _db;

        public MainController(AppDbContext db) {
            _db = db;
        }

        [HttpGet("Test")]
        public IActionResult Test() {
            return Ok("lorem ipsum");
        }

        [HttpGet("GetPromotion")]
        public IActionResult GetPromotion(string promotionId) {
            var result = _db.Promotions.FirstOrDefault(a => a.PromotionId == promotionId);

            return Ok(result);
        }

        [HttpGet("GetStores")]
        public IActionResult GetStores() {
            var result = _db.Stores.ToList();

            return Ok(result);
        }

        [HttpPost("SubmitPromotion")]
        public IActionResult SubmitPromotion(PromotionDto promotion) {
            var dateId = $"P{DateTime.Now.ToString("YYYYMMDD")}";
            var existingCount = _db.Promotions.Where(a => a.PromotionId.StartsWith(dateId)).Count();
            var newId = $"{dateId}{existingCount.ToString("D4")}";

            _db.Promotions.Add(new Promotion {
                PromotionId = newId,
                PromoType = promotion.PromoType,
                Description = promotion.Description,
                StartDate = promotion.StartDate,
                EndDate = promotion.EndDate,
                Item = promotion.Item,
                ValueType = promotion.ValueType,
                Value = promotion.Value
            });

            var relPromotionStores = promotion.StoreIds.Select(a => new RelPromotionStore {
                PromotionId = newId,
                StoreId = a
            });
            _db.RelPromotionStores.AddRange(relPromotionStores);

            _db.SaveChanges();

            return Ok(newId);
        }
    }
}