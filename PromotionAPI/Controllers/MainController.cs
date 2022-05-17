using Microsoft.AspNetCore.Mvc;
using PromotionAPI.Models;
using PromotionAPI.Services;
using System.IO;
using System.Text;

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
            var dateId = $"P{DateTime.Now.ToString("yyyyMMdd")}";
            var sequence = _db.Promotions.Where(a => a.PromotionId.StartsWith(dateId)).Count() + 1;
            var newId = $"{dateId}{sequence.ToString("D4")}";

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

            var sb = new StringBuilder();
            sb.AppendLine($"FHEAD|{promotion.Description}|||;");
            sb.AppendLine($"FITEM|[NOT IMPLEMENTED]|{promotion.PromoType}|{promotion.ValueType}|;");
            promotion.StoreIds.ForEach(a => sb.AppendLine($"FSTORE|{a}|{promotion.StartDate.GetValueOrDefault().ToString("yyyyMMdd")}|{promotion.EndDate.GetValueOrDefault().ToString("yyyyMMdd")}|;"));
            sb.Append("FTAIL||||");
            var str = sb.ToString();

            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{newId}.txt");
            System.IO.File.WriteAllText(filePath, str);

            return Ok(newId);
        }
    }
}