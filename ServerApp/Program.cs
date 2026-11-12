namespace ServerApp;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();

        app.MapPost("/average", (double[][] matrix) =>
        {
            double result = MatrixCalculator.CalculateAverage(matrix);
            Console.WriteLine($"[Сервер] Получена матрица. Среднее арифметическое: {result}");
            return Results.Ok(result);
        });

        app.Run("http://0.0.0.0:5000");
    }
}
