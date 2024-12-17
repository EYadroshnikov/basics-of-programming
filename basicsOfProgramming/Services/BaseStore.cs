using System.Text.Json;

namespace basicsOfProgramming.Services;

public abstract class BaseStore<T>
{
    protected abstract string FilePath { get; }

    public void SaveToFile(IEnumerable<T> items)
    {
        try
        {
            var json = JsonSerializer.Serialize(items, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            File.WriteAllText(FilePath, json);
            Console.WriteLine("Данные успешно сохранены в файл.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при сохранении данных: {ex.Message}");
        }
    }

    public List<T> LoadFromFile()
    {
        try
        {
            if (!File.Exists(FilePath))
            {
                Console.WriteLine("Файл не найден, загрузка отменена.");
                return new List<T>();
            }

            var json = File.ReadAllText(FilePath);
            var items = JsonSerializer.Deserialize<List<T>>(json);

            Console.WriteLine("Данные успешно загружены из файла.");
            return items ?? new List<T>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при загрузке данных: {ex.Message}");
            return new List<T>();
        }
    }
}