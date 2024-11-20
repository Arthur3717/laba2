using WPFMatrixOperations;

namespace MatrixToArray_MixedTypes_HandlesNonIntCorrectly
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void MatrixToArray_MixedTypes_HandlesNonIntCorrectly()
        {
            // This test verifies that ToArray() handles matrices with non-integer types (e.g., strings) gracefully, converting them to 0 in the resulting int array.
            Matrix<object> matrix = new Matrix<object>(new object[,] { { 1, 2 }, { 3, "4" } });
            int[,] result = matrix.ToArray();
            int[,] expected = { { 1, 2 }, { 3, 0 } };
            Assert.AreEqual(expected, result);
        }
    }
}