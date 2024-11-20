using WPFMatrixOperations;

namespace Test_Add_MatricesDifferentDimensions_ThrowsException
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test_Add_MatricesDifferentDimensions_ThrowsException()
        {
            // Arrange
            int[,] dataA = { { 1, 2 }, { 3, 4 } };
            int[,] dataB = { { 5, 6 }, { 7, 8 }, { 9, 10 } };
            Matrix<int> matrixA = new Matrix<int>(dataA);
            Matrix<int> matrixB = new Matrix<int>(dataB);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => { Matrix<int> result = matrixA + matrixB; });  // Проверяет, что сложение матриц разных размеров вызывает исключение ArgumentException.
        }
    }
}