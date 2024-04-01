using CentralDifferentialSolver.Input;
using CentralDifferentialSolver.Logic;
using CentralDifferentialSolver.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CentralDifferentialSolver
{
    internal class Program
    {
        static void Main()
        {
            // Дані за варіантом:
            // x       y
            // 1       0,6664
            // 1,15    0,4329
            // 1,3     0,2406
            // 1,45    0,0903
            // 1,6     -0,0178
            // 1,75    -0,0861
            // 1,9     -0,1185
            // E: 1,2

            Console.OutputEncoding = Encoding.Unicode;

            while (true)
            {
                TextViewer.ChangeColor("Введіть \"консоль\" для введення даних з консолі або \"файл\" для введення даних з файлу (або введіть 0 для виходу):", ConsoleColor.Green);
                string enterChooseValue = Console.ReadLine();

                if (enterChooseValue == "0")
                    break;

                // Створення списків для зберігання введених даних
                List<double> xValues = new List<double>();
                List<double> yValues = new List<double>();

                // Вибір стратегії введення даних залежно від введеного користувачем значення
                IDataInputStrategy inputStrategy = GetInputStrategy(enterChooseValue);
                inputStrategy.GetValues(xValues, yValues);

                TextViewer.ChangeColor("\nВведіть значення аргументу E:", ConsoleColor.Green);
                double xi;
                if (!double.TryParse(Console.ReadLine(), out xi))
                {
                    TextViewer.ChangeColor("\nНеправильний формат для E.", ConsoleColor.Red);
                    continue;
                }

                // Обчислення кроку h
                double h = xValues[1] - xValues[0];

                // Обчислення значень інтерполяції та похідних
                double result = NewtonInterpolation.Interpolate(xi, xValues, yValues);
                double firstDerivative = (NewtonInterpolation.Interpolate(xi + h, xValues, yValues) - result) / h;
                double secondDerivative = (NewtonInterpolation.Interpolate(xi + h, xValues, yValues) - 2 * result + NewtonInterpolation.Interpolate(xi - h, xValues, yValues)) / (h * h);

                TextViewer.ChangeColor($"\nЗначення поліному Інтерполяції Ньютона при E: {result}", ConsoleColor.Yellow);
                TextViewer.ChangeColor($"Перша похідна функції при E: {firstDerivative}", ConsoleColor.Yellow);
                TextViewer.ChangeColor($"Друга похідна функції при E: {secondDerivative}", ConsoleColor.Yellow);

                // Збереження розв'язку до файлу
                SaveSolutionToFile(result, firstDerivative, secondDerivative);

                TextViewer.ChangeColor("\nНатисніть будь-яку клавішу для продовження...", ConsoleColor.Green);
                Console.ReadKey();
                Console.Clear();
            }

            TextViewer.ChangeColor("\nПрограму завершено. Натисніть будь-яку клавішу для виходу...", ConsoleColor.Blue);
            Console.ReadKey();
        }

        // Метод для збереження розв'язку до файлу
        static void SaveSolutionToFile(double result, double firstDerivative, double secondDerivative)
        {
            string fileName = $"Result_{DateTime.Now:yyyyMMddHHmmss}.txt";
            try
            {
                using (StreamWriter sw = new StreamWriter(fileName))
                {
                    sw.WriteLine($"Значення поліному Інтерполяції Ньютона: {result}");
                    sw.WriteLine($"Перша похідна функції: {firstDerivative}");
                    sw.WriteLine($"Друга похідна функції: {secondDerivative}");
                }
                TextViewer.ChangeColor($"\nРезультат було збережено до файлу \"{fileName}\"", ConsoleColor.Yellow);
            }
            catch (Exception e)
            {
                TextViewer.ChangeColor($"\nПомилка збереження розв'язку до файлу: {e.Message}", ConsoleColor.Red);
            }
        }

        // Метод для вибору стратегії введення даних
        static IDataInputStrategy GetInputStrategy(string inputChoose)
        {
            if (inputChoose.ToLower() == "консоль")
            {
                // Використання стратегії введення даних з консолі
                return new ConsoleDataInputStrategy();
            }
            else if (inputChoose.ToLower() == "файл")
            {
                // Використання стратегії введення даних з файлу
                return new FileDataInputStrategy();
            }
            else
            {
                // Виведення повідомлення про неправильний вибір та використання стратегії введення даних з консолі за замовчуванням
                TextViewer.ChangeColor("\nНеправильний вибір. Використовується стратегія введення даних з консолі за замовчуванням.", ConsoleColor.Red);
                return new ConsoleDataInputStrategy();
            }
        }
    }
}
