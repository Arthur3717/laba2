using WPFMatrixOperations;

namespace Test_ToArray_IntMatrix_CorrectConversion
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test_ToArray_IntMatrix_CorrectConversion()
        {
            // Arrange
            int[,] data = { { 1, 2 }, { 3, 4 } };
            Matrix<int> matrix = new Matrix<int>(data);

            // Act
            int[,] result = matrix.ToArray();

            // Assert
            Assert.AreEqual(data, result); // ѕровер€ет корректное преобразование целочисленной матрицы в массив int[,].
        }
    }
}