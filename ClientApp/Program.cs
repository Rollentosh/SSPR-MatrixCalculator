using System.Net.Http.Json;

namespace ClientApp;

public static class Program
{
    public static async Task Main(string[] args)
    {
        string serverUrl = Environment.GetEnvironmentVariable("SERVER_URL") ?? "http://localhost:5000";

        Console.Write("Введите количество строк матрицы: ");
        int rows = int.Parse(Console.ReadLine() ?? "0");

        Console.Write("Введите количество столбцов матрицы: ");
        int cols = int.Parse(Console.ReadLine() ?? "0");

        double[][] matrix = new double[rows][];
        bool randomGenerate = false;
        Random random = new Random(42);

        for (int i = 0; i < rows; i++)
        {
            matrix[i] = new double[cols];
            string[] parts = Array.Empty<string>();

            if (!randomGenerate)
            {
                Console.WriteLine($"Введите {cols} чисел для строки {i + 1} через пробел (или '-' для автозаполнения всей матрицы):");
                parts = (Console.ReadLine() ?? "").Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length > 0 && parts.Equals("-")) { randomGenerate = true; }
            }

            for (int j = 0; j < cols; j++)
            {
                matrix[i][j] = randomGenerate ? (random.NextDouble() * 201 - 100) : double.Parse(parts[j]);
            }
        }

        using HttpClient client = new HttpClient();
        Console.WriteLine($"[Клиент] Отправка данных на {serverUrl}/average ...");

        try
        {
            var response = await client.PostAsJsonAsync($"{serverUrl}/average", matrix);
            response.EnsureSuccessStatusCode();

            double average = await response.Content.ReadFromJsonAsync<double>();
            Console.WriteLine($"[Клиент] Ответ от сервера (среднее арифметическое): {average}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Ошибка] Не удалось связаться с сервером: {ex.Message}");
        }
    }
}