using WPFMatrixOperations;

namespace ToArray_DoubleMatrix_ReturnsCorrectlyConvertedIntegerArray
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ToArray_DoubleMatrix_ReturnsCorrectlyConvertedIntegerArray()
        {
            double[,] matrix = { { 1.5, 2.5 }, { 3.5, 4.5 } };
            Matrix<double> matrixObject = new Matrix<double>(matrix);

            int[,] expectedResult = { { 0, 0 }, { 0, 0 } };
            int[,] actualResult = matrixObject.ToArray();

            Assert.AreEqual(expectedResult, actualResult);//Матрица с double в методе ToArray
        }
    }
}