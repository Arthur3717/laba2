using WPFMatrixOperations;

namespace Test_ToArray_StringMatrix_HandlesNonNumeric
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test_ToArray_StringMatrix_HandlesNonNumeric()
        {
            // Arrange
            string[,] data = { { "1", "2" }, { "3", "a" } };
            Matrix<string> matrix = new Matrix<string>(data);

            // Act
            int[,] result = matrix.ToArray();

            // Assert
            int[,] expected = { { 1, 2 }, { 3, 0 } }; // "a" преобразуется в 0
            Assert.AreEqual(expected, result); // Проверяет обработку нечисловых значений в методе ToArray().
        }
    }
}