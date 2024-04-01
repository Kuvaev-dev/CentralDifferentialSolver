using CentralDifferentialSolver.View;
using System;
using System.Collections.Generic;
using System.IO;

namespace CentralDifferentialSolver.Input
{
    // Стратегія для введення даних з файлу
    class FileDataInputStrategy : IDataInputStrategy
    {
        public void GetValues(List<double> xValues, List<double> yValues)
        {
            TextViewer.ChangeColor("Введіть шлях до файлу:", ConsoleColor.Green);
            string filePath = Console.ReadLine();

            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] inputData = line.Split(' ');

                        // Перевірка на коректність введених даних
                        if (inputData.Length != 2)
                        {
                            TextViewer.ChangeColor("\nНеправильний формат введення у файлі. Рядок ігнорується.\n", ConsoleColor.Red);
                            continue;
                        }

                        double x, y;
                        if (!double.TryParse(inputData[0], out x) || !double.TryParse(inputData[1], out y))
                        {
                            TextViewer.ChangeColor("\nНеправильний формат введення числових значень у файлі. Рядок ігнорується.\n", ConsoleColor.Red);
                            continue;
                        }

                        // Якщо валідація успішна, додати дані до списку
                        xValues.Add(x);
                        yValues.Add(y);
                    }
                }
            }
            catch (Exception e)
            {
                TextViewer.ChangeColor("\nПомилка читання файлу: " + e.Message, ConsoleColor.Red);
            }
        }
    }
}
