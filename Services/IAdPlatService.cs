// Интерфейс для работы с рекламными площадками
using AdPlat.Models;

namespace AdPlat.Services
{
    public interface IAdPlatService
    {
        // Загрузка данных из указанного файла locations.txt
        void UploadPlatforms(string filePath);
        // Находит реакламные площадки локации и возвращает их список
        List<string> SearchPlatforms(string location);
    }
}