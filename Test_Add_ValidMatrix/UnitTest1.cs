using NUnit.Framework;
using System.Windows.Controls;
using WPFMatrixOperations; 
namespace Test_Add_ValidMatrix
{
    [Parallelizable(ParallelScope.Self)]
    [TestFixture]
    public class Tests : PageTest
    {

        [Test]
        public void Test_Add_ValidMatrix_AddsToDictionary()
        {
            // Arrange (Подготовка)
            MatricesController<int> controller = new MatricesController<int>();
            int[,] data = { { 1, 2 }, { 3, 4 } };
            Matrix<int> matrix = new Matrix<int>(data);
            DataGrid dataGrid = new DataGrid(); // Замените на создание вашего DataGrid


            int[,] dataArray = matrix.ToArray();
            controller.Add(dataGrid, dataArray); // Или в методе, который требует int[,]

            // Assert (Утверждение)
            Assert.IsTrue(controller._matrixTable.ContainsKey(dataGrid), "Матрица не была добавлена в словарь.");
            Assert.AreEqual(matrix, controller._matrixTable[dataGrid], "Добавленная матрица не совпадает с ожидаемой");

        }
    }
}
