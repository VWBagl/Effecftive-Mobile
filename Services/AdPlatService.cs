// Реализация сервиса для управления рекламными площадками.
using AdPlat.Models;

namespace AdPlat.Services
{
    public class AdPlatService : IAdPlatService
    {
        private List<AdPlatform> _platforms = new List<AdPlatform>();

        // Загружает данные о рекламных площадках из файла.
        // Заменяет текущие данные в памяти.
        public void UploadPlatforms(string filePath)
        {
            // Проверка наличия файла
            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
            {
                throw new FileNotFoundException($"File not found: {filePath}");
            }

            // Читает содержимое файла
            string fileContent = File.ReadAllText(filePath);

            // Очищает текущие данные
            _platforms.Clear();

            // Разбивает содержимое на строки и обрабатывает каждую
            string[] lines = fileContent.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string line in lines)
            {
                ProcessLine(line.Trim());
            }
        }

        // Ищет рекламные площадки для указанной локации.
        // Возвращает список подходящих площадок.
        public List<string> SearchPlatforms(string location)
        {
            // Проверяет валидность входных данных
            if (string.IsNullOrEmpty(location) || !location.StartsWith('/'))
                return new List<string>();

            List<string> result = new List<string>();
            string searchLocation = location.TrimEnd('/');

            // Проверяет каждую площадку
            foreach (AdPlatform platform in _platforms)
            {
                if (IsPlatformSuitable(platform, searchLocation))
                {
                    result.Add(platform.Name);
                }
            }

            // Возвращает отсортированный результат
            return result.OrderBy(x => x).ToList();
        }

        // Проверяет, подходит ли площадка для указанной локации
        private bool IsPlatformSuitable(AdPlatform platform, string searchLocation)
        {
            // Проверяет все локации площадки
            foreach (string platformLocation in platform.Locations)
            {
                string normalizedLocation = platformLocation.TrimEnd('/');

                // Платформа подходит, если её локация является префиксом запрошенной локации
                if (searchLocation.StartsWith(normalizedLocation) &&
                    (searchLocation.Length == normalizedLocation.Length ||
                     searchLocation[normalizedLocation.Length] == '/'))
                {
                    return true;
                }
            }
            return false;
        }

        // Разбивает строку файла и добавляет данные о площадке
        private void ProcessLine(string line)
        {
            // Пропускаем пустые строки или строки без разделителя
            if (string.IsNullOrEmpty(line) || !line.Contains(':'))
                return;

            // Разделяем на название и локации
            string[] parts = line.Split(':', 2);
            if (parts.Length != 2)
                return;

            string platformName = parts[0].Trim();

            // Обрабатываем локации
            string[] locationParts = parts[1].Split(',');
            List<string> locations = new List<string>();

            foreach (string loc in locationParts)
            {
                string trimmedLoc = loc.Trim();
                if (trimmedLoc.StartsWith('/'))
                {
                    locations.Add(trimmedLoc);
                }
            }

            // Добавляем новую площадку
            _platforms.Add(new AdPlatform
            {
                Name = platformName,
                Locations = locations
            });
        }
    }
}