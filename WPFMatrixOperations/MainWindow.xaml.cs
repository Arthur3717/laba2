using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32; 

namespace WPFMatrixOperations
{
    public partial class MainWindow : Window
    {
        private readonly MatricesController<int> _matrixController = new();

        private bool _isFirstInputEntered;
        private bool _isSquareMatrix;
        private bool _isSecondInputEntered;

        public MainWindow()
        {
            InitializeComponent();

            AmendMatrix(matrixADataGrid);
            AmendMatrix(matrixBDataGrid);
            AmendMatrix(matrixCDataGrid);

            btnEnter.IsEnabled = false;
            btnCalculate.IsEnabled = false;
            cbRandomize.IsChecked = true;
            cbSquareMatrix.IsChecked = true;

            OnSquareMatrixChecked(null, null);
            SubscribeOnUI();

            btnDownload.Click += OnDownloadButtonClick;
        }

        private void SubscribeOnUI()
        {
            cbSquareMatrix.Click += OnSquareMatrixChecked;
            tbFirstSizeInput.TextChanged += OnSizeInput;
            tbSecondSizeInput.TextChanged += OnSizeInput;
            btnEnter.Click += OnCalculateButtonClick;
            btnCalculate.Click += OnCalculateSumButtonClick;
            matrixADataGrid.CellEditEnding += OnMatrixCellEdit;
            matrixBDataGrid.CellEditEnding += OnMatrixCellEdit;
        }

        private void AmendMatrix(DataGrid matrixDataGrid)
        {
            matrixDataGrid.CanUserAddRows = false;
            matrixDataGrid.CanUserDeleteRows = false;
            matrixDataGrid.CanUserReorderColumns = true;
            matrixDataGrid.CanUserSortColumns = false;
        }


        // Обработка установки/снятия флажка "Квадратная матрица"
        private void OnSquareMatrixChecked(object sender, RoutedEventArgs e)
        {
            _isSquareMatrix = cbSquareMatrix.IsChecked.Value;
            tbSecondSizeInput.Visibility = _isSquareMatrix ? Visibility.Hidden : Visibility.Visible;
            ChangeEnterButtonState();
        }

        private void OnSizeInput(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)e.Source;
            string content = textBox.Text;
            bool valid = content.IsDigit(out int result) && result.MoreThanZero();

            if (textBox == tbFirstSizeInput)
            {
                _isFirstInputEntered = valid;
            }
            else if (textBox == tbSecondSizeInput)
            {
                _isSecondInputEntered = valid;
            }

            ChangeEnterButtonState();
        }

        private void ChangeEnterButtonState() => btnEnter.IsEnabled = _isSquareMatrix ? _isFirstInputEntered : _isFirstInputEntered && _isSecondInputEntered;

        private void OnMatrixCellEdit(object? sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditingElement is TextBox textBox)
            {
                if (!int.TryParse(textBox.Text, out int value))
                {
                    MessageBox.Show("Некорректное значение. Пожалуйста, введите целое число.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    textBox.Text = "0"; // Возвращаем 0, если ввод некорректный
                    return;
                }

                int x = e.Row.GetIndex();
                int y = e.Column.DisplayIndex;
                _matrixController.ChangeValueForMatrixAt((DataGrid)sender, x, y, value);
            }
        }

        private void OnCalculateSumButtonClick(object sender, RoutedEventArgs e)
        {
            
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            
            matrixCDataGrid.Columns.Clear();
            matrixCDataGrid.ItemsSource = _matrixController.GetSumData();
            stopwatch.Stop();

            // Отображаем время в миллисекундах
            tbTime.Text = stopwatch.ElapsedMilliseconds.ToString("F2") + " мс";
        }

        private void OnCalculateButtonClick(object sender, RoutedEventArgs e)
        {
            btnCalculate.IsEnabled = true;
            SetMatrixSize();

            matrixCDataGrid.Columns.Clear();
            _matrixController.Clear();

            ChangeValueForMatrix(matrixADataGrid);
            ChangeValueForMatrix(matrixBDataGrid);
        }

        private void SetMatrixSize()
        {
            int firstSize = Convert.ToInt32(tbFirstSizeInput.Text);

            if (_isSquareMatrix)
            {
                _matrixController.Size = (firstSize, firstSize);
            }
            else
            {
                int secondSize = Convert.ToInt32(tbSecondSizeInput.Text);
                _matrixController.Size = (firstSize, secondSize);
            }
        }

        private void OnDownloadButtonClick(object sender, RoutedEventArgs e)
        {
            // Получение результирующей матрицы (замените на ваш собственный метод)
            _matrixController.FindSum(); // Сначала вызываем метод для вычисления суммы
            int[,] resultMatrix1 = _matrixController.ResultMatrix.ToArray(); // Доступ к результирующей матрице через свойство

            if (resultMatrix1 == null || resultMatrix1.GetLength(0) == 0)
            {
                MessageBox.Show("Нет данных для сохранения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV файлы (*.csv)|*.csv";
            saveFileDialog.Title = "Сохранить результирующую матрицу";

            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {

                    SaveMatrixToCsv(resultMatrix1, saveFileDialog.FileName);

                    MessageBox.Show("Матрица успешно сохранена в файл: " + saveFileDialog.FileName, "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при сохранении матрицы: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        // Сохраняет матрицу в файл CSV
        private void SaveMatrixToCsv(int[,] matrix, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                int rows = matrix.GetLength(0);
                int cols = matrix.GetLength(1);

                for (int i = 0; i < rows; i++)
                {
                    string line = "";
                    for (int j = 0; j < cols; j++)
                    {
                        line += matrix[i, j].ToString() + (j < cols - 1 ? ";" : ""); // Используется точка с запятой в качестве разделителя
                    }
                    writer.WriteLine(line);
                }
            }
        }

        private void ChangeValueForMatrix(DataGrid matrixDataGrid)
        {
            bool randomize = cbRandomize.IsChecked.Value;

            matrixDataGrid.Columns.Clear();         
            matrixDataGrid.ItemsSource = _matrixController.GetMatrixData(matrixDataGrid, randomize);
        }

        ~MainWindow()
        {
            cbSquareMatrix.Checked -= OnSquareMatrixChecked;
            tbFirstSizeInput.TextChanged -= OnSizeInput;
            tbSecondSizeInput.TextChanged -= OnSizeInput;
            btnEnter.Click -= OnCalculateButtonClick;
            btnCalculate.Click -= OnCalculateSumButtonClick;
            matrixADataGrid.CellEditEnding -= OnMatrixCellEdit;
            matrixBDataGrid.CellEditEnding -= OnMatrixCellEdit;
        }
    }
}