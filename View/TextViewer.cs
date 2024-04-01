using System;

namespace CentralDifferentialSolver.View
{
    public class TextViewer
    {
        // Метод для зміни кольору тексту у консолі
        public static void ChangeColor(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}
