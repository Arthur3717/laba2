using System;

namespace WPFMatrixOperations
{
    public class Matrix<T>
    {
        private readonly T[,] array;
        public T[,] Array => array;

        public Matrix(T[,] array)
        {
            this.array = array;
        }

        public static Matrix<T> operator +(Matrix<T> a, Matrix<T> b)
        {
            if (a.array.GetLength(0) != b.array.GetLength(0) || a.array.GetLength(1) != b.array.GetLength(1))
            {
                throw new ArgumentException("Matrices must have the same dimensions for addition.");
            }

            int rows = a.array.GetLength(0);
            int cols = a.array.GetLength(1);
            T[,] totalSum = new T[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    //This will only work for numeric types
                    dynamic numA = a.array[i, j];
                    dynamic numB = b.array[i, j];
                    totalSum[i, j] = numA + numB;
                }
            }
            return new Matrix<T>(totalSum);
        }

        public T this[int index1, int index2]
        {
            get => array[index1, index2];
            set => array[index1, index2] = value;
        }

        public int[,] ToArray()
        {
            int rows = array.GetLength(0);
            int cols = array.GetLength(1);
            int[,] intArray = new int[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (int.TryParse(this[i, j].ToString(), out int value))
                    {
                        intArray[i, j] = value;
                    }
                    else
                    {
                        // Обработка ошибки: Что делать, если преобразование невозможно?
                        // Варианты:
                        // 1. Выбросить исключение: throw new InvalidCastException($"Cannot convert '{this[i, j]}' to int.");
                        // 2. Использовать значение по умолчанию: intArray[i, j] = 0;
                        // 3. Записать в лог ошибку и использовать значение по умолчанию: Console.WriteLine($"Warning: Cannot convert '{this[i, j]}' to int. Using default value 0."); intArray[i, j] = 0;

                        intArray[i, j] = 0; // Выбрано значение по умолчанию 0
                    }
                }
            }
            return intArray;
        }
    }
}