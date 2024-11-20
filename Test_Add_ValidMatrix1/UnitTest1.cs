using NUnit.Framework;
using WPFMatrixOperations; // ��� ������ ��������� ����-������������

namespace Test_Add_ValidMatrix1
{
    public class Tests
    {
        [Test]
        public void Test_Add_ValidMatrix_AddsToDictionary()
        {
            // Arrange
            MatricesController<int> controller = new MatricesController<int>();
            int[,] data = { { 1, 2 }, { 3, 4 } };
            Matrix<int> matrix = new Matrix<int>(data);
            string dataGridId = "DataGrid1"; // ���������� ������ � �������� ��������������

            // Act
            controller.Add(dataGridId, matrix); // ���������� ��������� �������������

            // Assert
            Assert.IsTrue(controller._matrixTable.ContainsKey(dataGridId), "������� �� ���� ��������� � �������.");

            // �������� ����������� �������
            Matrix<int> retrievedMatrix = controller._matrixTable[dataGridId];
            Assert.AreEqual(matrix.Rows, retrievedMatrix.Rows, "���������� ����� �� ���������");
            Assert.AreEqual(matrix.Cols, retrievedMatrix.Cols, "���������� �������� �� ���������");
            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Cols; j++)
                {
                    Assert.AreEqual(matrix[i, j], retrievedMatrix[i, j], $"�������� � [{i}, {j}] �� ���������");
                }
            }
        }


        [Test]
        public void Test_Add_DuplicateKey_ThrowsException()
        {
            // Arrange
            MatricesController<int> controller = new MatricesController<int>();
            int[,] data = { { 1, 2 }, { 3, 4 } };
            Matrix<int> matrix = new Matrix<int>(data);
            string dataGridId = "DataGrid1";

            controller.Add(dataGridId, matrix);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => controller.Add(dataGridId, matrix), "������ ���� ���� ��������� ���������� ��� ���������� ��������� �����.");
        }
    }
}