// Задача 52. Задайте двумерный массив из целых чисел. Найдите среднее арифметическое элементов в каждом столбце.
// Например, задан массив:
// 1 4 7 2
// 5 9 2 3
// 8 4 2 4
// Среднее арифметическое каждого столбца: 4,6; 5,6; 3,6; 3.

namespace HW50
{
    class ConsoleApp
    {
        static void Main()
        {
            Console.WriteLine("Welcome to the array column average finder!");
            var arr = new ArrayBuilder(3, 4);
            Console.WriteLine("Here is you array:");
            Console.WriteLine(arr.ToString());
            Console.WriteLine($"Average for each column: {arr.GetAverages()}");
        }
    }

    public class ArrayBuilder
    {
        private double[,] _arr;
        private Dictionary<double, string> _values;
        private string _averages;
        public ArrayBuilder(int row, int col)
        {
            _values = new Dictionary<double, string>();
            _averages = string.Empty;
            _arr = InitializeRadomArray(row, col);
        }

        public bool TryGetIndexes(double key, out string indexes)
        {
            indexes = string.Empty;
            return _values.TryGetValue(key, out indexes);
        }

        public override string ToString()
        {
            return _arr.ToArrString();
        }

        public string GetAverages()
        {
            return _averages;
        }

        double[,] InitializeRadomArray(int row, int col)
        {
            var result = new double[row, col];
            var rnd = new Random();
            for (int i = 0; i < col; i++)
            {
                var colSumm = default(double);
                for (int j = 0; j < row; j++)
                {
                    var signPow = rnd.Next(1, 3);
                    var tenPow = rnd.Next(0, 3);
                    var doubleValue = rnd.NextDouble();
                    var sign = ((double)Math.Pow(-1, signPow));
                    var tens = ((double)Math.Pow(10, tenPow));
                    var roundCount = rnd.Next(0, 3);
                    var arrInput =  Math.Round(doubleValue * sign * tens, roundCount);
                    colSumm += arrInput;
                    AddValues(arrInput, j, i);
                    result[j, i] = arrInput;
                }

                _averages += $"{Math.Round(colSumm / (double)row, 2)}; ";
            }

            _averages = _averages.Remove(_averages.Length - 2);
            _averages += ".";
            return result;
        }

        void AddValues(double key, int row, int col)
        {
            var indexes = $"{row}, {col}";
            if (_values.ContainsKey(key))
            {
                var oldString = _values[key];
                var newString = $"[{oldString}], [{indexes}]";
                _values[key] = newString;
            }
            else
            {
                _values.Add(key, indexes);
            }
        }
    }

    public static class ArrExtension
    {
        public static string ToArrString(this double[,] arr)
        {
            var result = string.Empty;
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    result += arr[i, j] + "\t";
                }

                result += "\n";
            }

            return result;
        }
    }
}