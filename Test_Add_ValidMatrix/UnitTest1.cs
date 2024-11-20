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
            // Arrange (����������)
            MatricesController<int> controller = new MatricesController<int>();
            int[,] data = { { 1, 2 }, { 3, 4 } };
            Matrix<int> matrix = new Matrix<int>(data);
            DataGrid dataGrid = new DataGrid(); // �������� �� �������� ������ DataGrid


            int[,] dataArray = matrix.ToArray();
            controller.Add(dataGrid, dataArray); // ��� � ������, ������� ������� int[,]

            // Assert (�����������)
            Assert.IsTrue(controller._matrixTable.ContainsKey(dataGrid), "������� �� ���� ��������� � �������.");
            Assert.AreEqual(matrix, controller._matrixTable[dataGrid], "����������� ������� �� ��������� � ���������");

        }
    }
}
