using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace WPFMatrixOperations
{
    public class MatricesController<T>
        where T : struct
    {
        public Matrix<T> _sumMatrix;

        // Словарь для хранения матриц, связанных с DataGrid
        public readonly Dictionary<DataGrid, Matrix<T>> _matrixTable = new();

        // Свойство для хранения размера матрицы
        public (int X, int Y) Size { get; set; }

       

        // Метод для создания массива данных для матрицы
        private T[,] CreateDataArray(bool randomize, int maxValue = 10)
        {
            // Создаем генератор случайных чисел
            Random random = new();

            // Создаем двумерный массив типа T с заданным размером
            T[,] array = new T[Size.X, Size.Y];

            // Заполняем массив случайными значениями или нулями
            for (int i = 0; i < Size.X; i++)
            {
                for (int j = 0; j < Size.Y; j++)
                {
                    T value = default; // Начальное значение

                    // Определяем тип данных T и генерируем случайные значения
                    if (typeof(T) == typeof(int))
                    {
                        value = (T)(object)random.Next(maxValue); // Случайное целое число
                    }
                    else if (typeof(T) == typeof(double))
                    {
                        value = (T)(object)(random.NextDouble() * maxValue); // Случайное число с плавающей точкой
                    }
                    else if (typeof(T) == typeof(float))
                    {
                        value = (T)(object)(random.NextSingle() * maxValue); // Случайное число с плавающей точкой
                    }
                    else if (typeof(T) == typeof(long))
                    {
                        value = (T)(object)random.NextInt64(maxValue); // Случайное целое число 64 бит
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException(); // Неизвестный тип данных
                    }

                    // Заполняем ячейку массива случайным значением (если randomize=true) или 0 (если randomize=false)
                    array[i, j] = randomize ? value : default;
                }
            }

            // Возвращаем созданный массив
            return array;
        }

        // Метод для добавления матрицы в словарь
        public void Add(DataGrid matrixDataGrid, T[,] array)
        {
            // Создаем объект матрицы 
            Matrix<T> matrix = new(array);

            // Добавляем матрицу в словарь, если ее там еще нет
            if (_matrixTable.ContainsKey(matrixDataGrid) == false)
            {
                _matrixTable.Add(matrixDataGrid, matrix);
            }
            else
            {
                throw new Exception(); // Выбрасываем исключение, если матрица уже существует
            }
        }

        // Метод для очистки словаря матриц
        public void Clear() => _matrixTable.Clear();

        // Метод для получения DataView со значениями суммы матриц
        public DataView GetSumData() => ConvertArrayToDataTable(FindSum().Array);

        // Метод для получения DataView с данными матрицы (случайное заполнение или нулями)
        public DataView GetMatrixData(DataGrid dataGrid, bool randomize)
        {
            // Создаем массив данных для матрицы
            var array = CreateDataArray(randomize);

            // Добавляем матрицу в словарь
            Add(dataGrid, array);

            // Преобразуем массив в DataView
            return ConvertArrayToDataTable(array);
        }


      

        // Метод для поиска суммы матриц
        public Matrix<T> FindSum()
        {
            // Создаем матрицу для хранения суммы
            _sumMatrix = new Matrix<T>(new T[Size.X, Size.Y]);
            _matrixTable.Values.ToList().ForEach(x => _sumMatrix += x);
            return _sumMatrix; // Возвращаем вычисленную матрицу
        }

        public Matrix<T> ResultMatrix => _sumMatrix; // Публичное свойство для доступа к результату

        // Метод для преобразования двумерного массива в DataView
        public DataView ConvertArrayToDataTable(T[,] array)
        {
            // Создаем DataTable
            DataTable dataTable = new();

            // Добавляем столбцы в DataTable
            for (int i = 0; i < array.GetLength(1); i++)
            {
                dataTable.Columns.Add("Column " + (i + 1));
            }

            // Добавляем строки в DataTable
            for (int i = 0; i < array.GetLength(0); i++)
            {
                DataRow row = dataTable.NewRow();
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    row[j] = array[i, j];
                }
                dataTable.Rows.Add(row);
            }

            // Возвращаем DataView, созданный из DataTable
            return dataTable.DefaultView;
        }

        // Метод для изменения значения в матрице, связанной с DataGrid
        public void ChangeValueForMatrixAt(DataGrid dataGrid, int x, int y, T value)
        {
            // Получаем матрицу из словаря
            var matrix = _matrixTable[dataGrid];

            // Изменяем значение в матрице
            matrix[x, y] = value;
        }
    }
}