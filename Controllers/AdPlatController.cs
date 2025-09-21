// API контроллер для управления рекламными площадками.
using Microsoft.AspNetCore.Mvc;
using AdPlat.Services;
using AdPlat.DTOs;

namespace AdPlat.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdPlatController : ControllerBase
    {
        private readonly IAdPlatService _adPlatService;

        public AdPlatController(IAdPlatService adPlatService)
        {
            _adPlatService = adPlatService;
        }

        // Загрузка данных о площадках из файла
        [HttpPost("upload")]
        public IActionResult UploadPlatforms([FromBody] UploadRequest request)
        {
            try
            {
                // Проверка входных данных
                if (string.IsNullOrEmpty(request?.FilePath))
                {
                    return BadRequest(new { Error = "FilePath is required" });
                }

                // Загрузка данных
                _adPlatService.UploadPlatforms(request.FilePath);

                return Ok(new
                { Message = "File uploaded successfully", });
            }
            catch (FileNotFoundException ex)
            {
                return NotFound(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = $"Error: {ex.Message}" });
            }
        }

        //Ищет рекламные площадки для указанной локации
        [HttpGet("search")]
        public ActionResult<SearchResponse> SearchPlatforms([FromQuery] string location)
        {
            try
            {
                // Проверяем обязательный параметр
                if (string.IsNullOrEmpty(location))
                {
                    return BadRequest(new { Error = "Location parameter is required" });
                }

                // Выполняем поиск
                List<string> platforms = _adPlatService.SearchPlatforms(location);
                return Ok(new SearchResponse { Platforms = platforms });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = $"Error: {ex.Message}" });
            }
        }
    }
}