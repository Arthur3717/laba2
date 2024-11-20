using System.Windows.Controls;
using WPFMatrixOperations;

namespace MatrixToArray_NonInteger_HandlesConversionCorrectly
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }
        [Test]
        public void MatrixToArray_NonInteger_HandlesConversionCorrectly()
        {
            var matrix = new Matrix<object>(new object[,] { { 1, 2 }, { "abc", 4 } });

            var intArray = matrix.ToArray();
            // Assert - проверяем, что всё сохранилось правильно, несмотря на необработанные данные
            Assert.AreEqual(new int[,] { { 1, 2 }, { 0, 4 } }, intArray);
        }
    }
}