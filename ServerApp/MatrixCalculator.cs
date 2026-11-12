namespace ServerApp;

public static class MatrixCalculator
{
    public static double CalculateAverage(double[][] matrix)
    {
        if (matrix == null || matrix.Length == 0)
            throw new ArgumentException("Matrix cannot be empty");

        double sum = 0;
        int count = 0;

        foreach (var row in matrix)
        {
            foreach (var item in row)
            {
                sum += item;
                count++;
            }
        }

        return (count == 0) ? 0 : (sum / count);
    }
}
