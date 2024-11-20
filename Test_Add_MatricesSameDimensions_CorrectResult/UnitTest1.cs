using WPFMatrixOperations;
using NUnit.Framework;

namespace Test_Add_MatricesSameDimensions_CorrectResult
{

    [TestFixture]
    public class MatrixTests
    {
        [Test]
        public void Test_Add_Matrices()
        {
            // Arrange
            int[,] dataA = { { 1, 2 }, { 3, 4 } };
            int[,] dataB = { { 5, 6 }, { 7, 8 } };
            Matrix<int> matrixA = new Matrix<int>(dataA);
            Matrix<int> matrixB = new Matrix<int>(dataB);

            // Act
            Matrix<int> sumMatrix = matrixA + matrixB;

            // Assert
            int[,] expected = { { 6, 8 }, { 10, 12 } };
            Assert.AreEqual(expected, sumMatrix.Array); // Проверяет корректность сложения матриц одинаковых размеров.
        }
    }
}