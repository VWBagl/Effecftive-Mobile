//Модель данных рекламной площадки
namespace AdPlat.Models
{
    public class AdPlatform
    {
        public string Name { get; set; } = string.Empty;
        public List<string> Locations { get; set; } = new List<string>();
    }
}