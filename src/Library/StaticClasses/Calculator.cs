using System.Globalization;

namespace Library.StaticClasses;

/// <summary>
/// This class has the responsabilities of making calculations, these being:
///     - Index in range
///     - Approving of inputs
///     - Damage calculations
/// </summary>
public static class Calculator
{
    /// <summary>
    /// Function to validate that a number is in between two given values
    /// </summary>
    /// <param name="number"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static int ValidateSelectionInGivenRange(int number, int min, int max)
    {
        while (!(Enumerable.Range(min,max).Contains(number)))
        {
            Printer.IndexOutOfRange(min, max);
            number = Convert.ToInt16(Console.ReadLine(), CultureInfo.InvariantCulture);
        }
        return number;
    }
}