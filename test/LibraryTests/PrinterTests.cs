
using Library.Game.Pokemons;
using NUnit.Framework;

using Library.Game.Attacks;//using NUnit.Framework;

using Moq;

using Library.Game.Items;
using NUnit.Framework.Legacy;
using Assert = NUnit.Framework.Assert;
using StringWriter = System.IO.StringWriter;
using TextWriter = System.IO.TextWriter;

namespace Library.Game.Utilities.Tests;
using NUnit.Framework;
using System;
using System.IO;
using Library.Game.Utilities;

[TestFixture]
public class PrinterTests
{
    private StringWriter consoleOutput;
    private TextWriter originalOutput;

    [NUnit.Framework.SetUp]
    public void Setup()
    {
        // Redirigir la salida de consola para poder hacer asserts
        consoleOutput = new StringWriter();
        originalOutput = Console.Out;
        Console.SetOut(consoleOutput);
    }

    [TearDown]
    public void TearDown()
    {
        // Restaurar la salida de consola original
        Console.SetOut(originalOutput);
        consoleOutput.Dispose();
    }

    [Test]
    public void StartPrint_ShouldPrintWelcomeAndMenuOptions()
    {
        // Arrange
        Console.Clear(); // Simular la limpieza de consola

        // Act
        Printer.StartPrint();
        var output = consoleOutput.ToString();

        // Assert
        StringAssert.Contains("Welcome to Pokemon Battle", output);
        StringAssert.Contains("1) Start", output);
        StringAssert.Contains("2) Leave", output);
    }

    [Test]
    public void EndPrint_ShouldPrintThankYouMessage()
    {
        // Act
        Printer.EndPrint();
        var output = consoleOutput.ToString();

        // Assert
        StringAssert.Contains("Thanks for playing!!", output);
    }

    [Test]
    public void StartPrint_ShouldClearConsole()
    {
        // Este test verifica que se llama a Console.Clear()
        // Nota: Es difícil probar directamente Console.Clear(), así que este es un test básico
        Assert.DoesNotThrow(() => Printer.StartPrint());
    }

[Test]
    public void DisplayWinner_ShouldContainWinnerName_WhenWinnerNameIsGiven()
    {
        // Arrange
        string winnerName = "Alice";
        string expectedFragment = "Alice"; // Only check that the winner's name is present

        var originalOut = Console.Out; // Save the original console output
        var originalIn = Console.In;   // Save the original console input

        using (StringWriter sw = new StringWriter())
        using (StringReader sr = new StringReader("\n"))
        {
            Console.SetOut(sw); // Redirect console output to StringWriter
            Console.SetIn(sr);  // Redirect console input to simulate key press

            try
            {
                // Act
                Printer.DisplayWinner(winnerName);

                // Assert
                string actualOutput = sw.ToString().Replace("\r", ""); // Normalize line endings
                Assert.That(actualOutput, Does.Contain(expectedFragment),
                    $"The output does not contain the expected name '{expectedFragment}'.");
            }
            finally
            {
                // Restore original console state
                Console.SetOut(originalOut);
                Console.SetIn(originalIn);
            }
        }
    }
    [Test]
    public void IndexOutOfRange_ShouldContainMinAndMaxMessages()
    {
        // Arrange
        int min = 10;
        int max = 20;
        string expectedFirstMessage = $"El valor debe ser mayor que {min}";
        string expectedSecondMessage = $"y menor que {max}";

        var originalOut = Console.Out; // Save the original console output

        using (StringWriter sw = new StringWriter())
        {
            Console.SetOut(sw); // Redirect console output to StringWriter

            try
            {
                // Act
                Printer.IndexOutOfRange(min, max);

                // Assert
                string actualOutput = sw.ToString().Replace("\r", ""); // Normalize line endings
                Assert.That(actualOutput, Does.Contain(expectedFirstMessage),
                    $"The output does not contain the expected message: '{expectedFirstMessage}'");
                Assert.That(actualOutput, Does.Contain(expectedSecondMessage),
                    $"The output does not contain the expected message: '{expectedSecondMessage}'");
            }
            finally
            {
                // Restore original console state
                Console.SetOut(originalOut);
            }
        }
    }
    }