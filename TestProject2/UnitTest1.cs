using NUnit.Framework;
using System.Windows.Controls;
using WPFMatrixOperations;

namespace TestProject2
{
 

    [TestFixture]
    public class MatrixTests
    {
        [Test]
        public void Test_Constructor_CreatesMatrixCorrectly()
        {
            Matrix<int> controller = new Matrix<int>();

            int[,] data = { { 1, 2 }, { 3, 4 } };
            Matrix<int> matrix = new Matrix<int>(data);
            Assert.AreEqual(data, matrix.Array);
        }
    }