using System.Collections.Generic;

namespace CentralDifferentialSolver.Logic
{
    // Клас для проведення інтерполяції за допомогою методу Ньютона
    public class NewtonInterpolation
    {
        // Метод для інтерполяції значення функції методом Ньютона
        // xi: значення аргументу, для якого проводиться інтерполяція
        // xValues: список значень x (аргументів) вузлів інтерполяції
        // yValues: список значень y (функцій) вузлів інтерполяції
        // Повертає інтерпольоване значення функції у точці xi
        public static double Interpolate(double xi, List<double> xValues, List<double> yValues)
        {
            // Кількість вузлів інтерполяції
            int n = xValues.Count;
            // Результат інтерполяції
            double result = 0;

            // Проводимо інтерполяцію за допомогою методу Ньютона
            for (int i = 0; i < n; i++)
            {
                // Початкове значення для ітераційного результату
                double iterationResult = yValues[i];

                // Обчислюємо коефіцієнт Лагранжа для інтерполяції
                for (int j = 0; j < i; j++)
                {
                    iterationResult *= (xi - xValues[j]) / (xValues[i] - xValues[j]);
                }

                // Додаємо до результату вагований внесок вузла
                result += iterationResult;
            }

            // Повертаємо інтерпольоване значення функції у точці xi
            return result;
        }
    }
}
