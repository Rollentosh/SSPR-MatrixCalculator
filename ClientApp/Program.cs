using System.Net.Http.Json;

namespace ClientApp;

public static class Program
{
    public static async Task Main(string[] args)
    {
        string serverUrl = Environment.GetEnvironmentVariable("SERVER_URL") ?? "http://localhost:5000";

        Console.Write("Enter the number of matrix rows: ");
        int rows = int.Parse(Console.ReadLine() ?? "0");

        Console.Write("Enter the number of matrix columns: ");
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
                Console.WriteLine($"Enter {cols} numbers for row {i + 1} separated by spaces (or '-' to auto-fill the entire matrix):");
                parts = (Console.ReadLine() ?? "").Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length > 0 && parts[0].Equals("-")) { randomGenerate = true; }
            }

            for (int j = 0; j < cols; j++)
            {
                matrix[i][j] = randomGenerate ? (random.NextDouble() * 201 - 100) : double.Parse(parts[j]);
            }
        }

        using HttpClient client = new HttpClient();
        Console.WriteLine($"[Client] Sending data to {serverUrl}/average ...");

        try
        {
            var response = await client.PostAsJsonAsync($"{serverUrl}/average", matrix);
            response.EnsureSuccessStatusCode();

            double average = await response.Content.ReadFromJsonAsync<double>();
            Console.WriteLine($"[Client] Response from server (arithmetic mean): {average}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Error] Failed to connect to the server: {ex.Message}");
        }
    }
}
