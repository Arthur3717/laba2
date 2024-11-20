using NUnit.Framework;
using WPFMatrixOperations; // или другой фреймворк юнит-тестирования

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
            string dataGridId = "DataGrid1"; // Используем строку в качестве идентификатора

            // Act
            controller.Add(dataGridId, matrix); // Используем строковый идентификатор

            // Assert
            Assert.IsTrue(controller._matrixTable.ContainsKey(dataGridId), "Матрица не была добавлена в словарь.");

            // Проверка содержимого матрицы
            Matrix<int> retrievedMatrix = controller._matrixTable[dataGridId];
            Assert.AreEqual(matrix.Rows, retrievedMatrix.Rows, "Количество строк не совпадает");
            Assert.AreEqual(matrix.Cols, retrievedMatrix.Cols, "Количество столбцов не совпадает");
            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Cols; j++)
                {
                    Assert.AreEqual(matrix[i, j], retrievedMatrix[i, j], $"Значение в [{i}, {j}] не совпадает");
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
            Assert.Throws<ArgumentException>(() => controller.Add(dataGridId, matrix), "Должно было быть выброшено исключение при добавлении дубликата ключа.");
        }
    }
}