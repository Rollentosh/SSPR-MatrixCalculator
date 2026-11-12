namespace ServerApp.Tests;

public class MatrixTests
{
    [Fact]
    public void CalculateAverageValidMatrix()
    {
        double[][] matrix = new double[][]
        {
            new double[] { 2, 4 },
            new double[] { 6, 8 }
        };

        double result = MatrixCalculator.CalculateAverage(matrix);
        Assert.Equal(5.0, result);
    }

    [Fact]
    public void CalculateAverageEmptyMatrix()
    {
        double[][] matrix = new double[0][];
        Assert.Throws<ArgumentException>(() => MatrixCalculator.CalculateAverage(matrix));
    }
}
