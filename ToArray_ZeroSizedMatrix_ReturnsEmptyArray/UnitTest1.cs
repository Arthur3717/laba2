using WPFMatrixOperations;

namespace ToArray_ZeroSizedMatrix_ReturnsEmptyArray
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ToArray_ZeroSizedMatrix_ReturnsEmptyArray()
        {
            int[,] matrix = new int[0, 0];
            Matrix<int> matrixObject = new Matrix<int>(matrix);

            int[,] expectedResult = new int[0, 0];
            int[,] actualResult = matrixObject.ToArray();

            Assert.AreEqual(expectedResult, actualResult);//ѕроверка на матрицу с нулевым размером
        }
    }
}