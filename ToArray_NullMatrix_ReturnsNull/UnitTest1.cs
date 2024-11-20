using WPFMatrixOperations;

namespace AddMatrices_DifferentTypes_HandlesCorrectly
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }
        [Test]
        public void AddMatrices_DifferentTypes_HandlesCorrectly()
        {
            // Arrange
            Matrix<double> matrix1 = new Matrix<double>(new double[,] { { 1.5, 2.5 }, { 3.5, 4.5 } });
            Matrix<double> matrix2 = new Matrix<double>(new double[,] { { 5.5, 6.5 }, { 7.5, 8.5 } });

            // Act
            Matrix<double> result = matrix1 + matrix2;

            // Assert
            double[,] expectedResult = { { 7, 9 }, { 11, 13 } };
            double[,] actualResult = result.Array;

            Assert.AreEqual(expectedResult, actualResult);//ƒобавление матриц с различными типами
        }
    }
}