using System.Collections.Generic;

namespace CentralDifferentialSolver.Input
{
    // Інтерфейс для стратегії введення даних
    interface IDataInputStrategy
    {
        void GetValues(List<double> xValues, List<double> yValues);
    }
}
