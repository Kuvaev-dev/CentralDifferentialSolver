using CentralDifferentialSolver.View;
using System;
using System.Collections.Generic;

namespace CentralDifferentialSolver.Input
{
    // Стратегія для введення даних з консолі
    class ConsoleDataInputStrategy : IDataInputStrategy
    {
        public void GetValues(List<double> xValues, List<double> yValues)
        {
            TextViewer.ChangeColor("\nВведіть значення x[i] та y[i] для кожного вузла (наприклад, 1 2):\n", ConsoleColor.Green);
            for (int i = 0; i <= 6; i++)
            {
                while (true)
                {
                    TextViewer.ChangeColor($"x[{i}] y[{i}]: ", ConsoleColor.Cyan);
                    string[] inputData = Console.ReadLine().Split(' ');

                    // Перевірка на коректність введених даних
                    if (inputData.Length != 2)
                    {
                        TextViewer.ChangeColor("\nНеправильний формат введення. Будь ласка, введіть значення у форматі \"x y\".\n", ConsoleColor.Red);
                        continue;
                    }

                    double x, y;
                    if (!double.TryParse(inputData[0], out x) || !double.TryParse(inputData[1], out y))
                    {
                        TextViewer.ChangeColor("\nНеправильний формат введення. Будь ласка, введіть числові значення для x та y.\n", ConsoleColor.Red);
                        continue;
                    }

                    // Якщо валідація успішна, додати дані до списку та вийти з циклу
                    xValues.Add(x);
                    yValues.Add(y);
                    break;
                }
            }
        }
    }
}
