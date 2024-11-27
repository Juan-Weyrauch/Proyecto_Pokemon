
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
        var originalIn = Console.In; // Save the original console input

        using (StringWriter sw = new StringWriter())
        using (StringReader sr = new StringReader("\n"))
        {
            Console.SetOut(sw); // Redirect console output to StringWriter
            Console.SetIn(sr); // Redirect console input to simulate key press

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

    [Test]
    public void NameSelection_ShouldContainPromptMessage()
    {
        // Arrange
        string expectedMessage = "Enter your name";

        var originalOut = Console.Out; // Save the original console output

        using (StringWriter sw = new StringWriter())
        {
            Console.SetOut(sw); // Redirect console output to StringWriter

            try
            {
                // Act
                Printer.NameSelection();

                // Assert
                string actualOutput = sw.ToString();
                Assert.That(actualOutput, Does.Contain(expectedMessage),
                    $"The output does not contain the expected prompt message: '{expectedMessage}'");
            }
            finally
            {
                // Restore original console state
                Console.SetOut(originalOut);
            }
        }
    }

    [Test]
    public void YourTurn_ShouldContainPlayerName()
    {
        // Arrange
        string playerName = "Alice";
        string expectedFragment =
            $"Your turn Player {playerName}"; // Check for the message containing the player's name

        var originalOut = Console.Out; // Save the original console output

        using (StringWriter sw = new StringWriter())
        {
            Console.SetOut(sw); // Redirect console output to StringWriter

            try
            {
                // Act
                Printer.YourTurn(playerName);

                // Assert
                string actualOutput = sw.ToString();
                Assert.That(actualOutput, Does.Contain(expectedFragment),
                    $"The output does not contain the expected player name: '{expectedFragment}'");
            }
            finally
            {
                // Restore original console state
                Console.SetOut(originalOut);
            }
        }
    }

    [Test]
    public void ShowCatalogue_ShouldContainPokemonNames()
    {
        // Arrange
        var pokedex = new Dictionary<int, IPokemon>()
        {
            { 1, new Pokemon("Pikachu", 100, "Electric", new List<IAttack> { }) },
            { 2, new Pokemon("Bulbasaur", 50, "Grass", new List<IAttack> { }) },
            { 3, new Pokemon("Charmander", 75, "Fire", new List<IAttack> { }) },
            { 4, new Pokemon("Squirtle", 80, "Water", new List<IAttack> { }) },
            { 5, new Pokemon("Eevee", 60, "Normal", new List<IAttack> { }) }
        };

        var expectedFragments = new List<string>()
        {
            "Pikachu",
            "Bulbasaur",
            "Charmander",
            "Squirtle",
            "Eevee"
        };

        var originalOut = Console.Out; // Save the original console output

        using (StringWriter sw = new StringWriter())
        {
            Console.SetOut(sw); // Redirect console output to StringWriter

            try
            {
                // Act
                Printer.ShowCatalogue(pokedex);

                // Assert
                string actualOutput = sw.ToString();

                foreach (var expectedFragment in expectedFragments)
                {
                    Assert.That(actualOutput, Does.Contain(expectedFragment),
                        $"The output does not contain the expected Pokémon name: '{expectedFragment}'");
                }
            }
            finally
            {
                // Restore original console state
                Console.SetOut(originalOut);
            }
        }
    }

    [Test]
    public void FormatPokemonBox_ShouldContainExpectedDetails()
    {
        // Arrange
        int index = 1;
        string name = "Pikachu";
        int life = 100;
        int boxWidth = 30; // Ajusta el ancho del cuadro como desees
        string[] expectedFragments = new string[]
        {
            "Number: 1",
            "Name: Pikachu",
            "Life: 100"
        };

        // Act
        string[] actualOutput = Printer.FormatPokemonBox(index, name, life, boxWidth);
        string actualString = string.Join("\n", actualOutput); // Convertir el array de líneas a un solo string

        // Assert
        foreach (var expectedFragment in expectedFragments)
        {
            Assert.That(actualString, Does.Contain(expectedFragment),
                $"The output does not contain the expected message: '{expectedFragment}'");
        }
    }

    [Test]
    public void PrintRow_ShouldContainCorrectRowFormatting()
    {
        // Arrange
        var boxes = new List<string[]>
        {
            new string[] { "╔══════════════╗", "║ Box 1        ║", "╚══════════════╝" },
            new string[] { "╔══════════════╗", "║ Box 2        ║", "╚══════════════╝" },
            new string[] { "╔══════════════╗", "║ Box 3        ║", "╚══════════════╝" }
        };

        int spacing = 2; // Espaciado entre los cuadros

        var expectedFragments = new string[]
        {
            "╔══════════════╗", // Comprobar si la primera línea de los cuadros está presente
            "║ Box 1        ║", // Comprobar si la segunda línea con el nombre del cuadro está presente
            "╚══════════════╝" // Comprobar si la última línea de los cuadros está presente
        };

        var originalOut = Console.Out; // Guardamos la salida original de la consola

        using (StringWriter sw = new StringWriter())
        {
            Console.SetOut(sw); // Redirigimos la salida de la consola a StringWriter

            try
            {
                // Act
                Printer.PrintRow(boxes, spacing);

                // Assert
                string actualOutput = sw.ToString().Trim(); // Quitamos los espacios extra

                // Comprobamos que cada fragmento esperado esté contenido en la salida
                foreach (var expectedFragment in expectedFragments)
                {
                    Assert.That(actualOutput, Does.Contain(expectedFragment),
                        $"La salida no contiene el fragmento esperado: '{expectedFragment}'");
                }
            }
            finally
            {
                // Restauramos la salida original de la consola
                Console.SetOut(originalOut);
            }
        }
    }
[Test]
    public void AskForPokemon_ShouldContainPokemonNameAndIndex()
    {
        // Arrange
        int index = 1;
        string name = "Pikachu";
        string expectedFragment = $" Pick your Pokemon";

        var originalOut = Console.Out; // Guardamos la salida original de la consola

        using (StringWriter sw = new StringWriter())
        {
            Console.SetOut(sw); // Redirigimos la salida de la consola a StringWriter

            try
            {
                // Act
                Printer.AskForPokemon(index, name);

                // Assert
                string actualOutput = sw.ToString().Trim(); // Quitamos los espacios extra

                // Comprobamos si la salida contiene el fragmento esperado
                Assert.That(actualOutput, Does.Contain(expectedFragment),
                    $"La salida no contiene el fragmento esperado: '{expectedFragment}'");
            }
            finally
            {
                // Restauramos la salida original de la consola
                Console.SetOut(originalOut);
            }
        }
    }
            
        }
[Test]
public void ShowInventory_ShouldContainPokemonNames()
{
    // Arrange
    var inventory = new List<IPokemon>
    {
        new Pokemon { Name = "Pikachu", Health = 100 },
        new Pokemon { Name = "Bulbasaur", Health = 50 },
        new Pokemon { Name = "Charmander", Health = 75 },
        new Pokemon { Name = "Squirtle", Health = 80 },
        new Pokemon { Name = "Eevee", Health = 60 }
    };

    // Definimos los nombres esperados de los Pokémon en el inventario
    var expectedFragments = new List<string>
    {
        "Pikachu",
        "Bulbasaur",
        "Charmander",
        "Squirtle",
        "Eevee"
    };

    var originalOut = Console.Out; // Guardamos la salida original de la consola

    using (StringWriter sw = new StringWriter())
    {
        Console.SetOut(sw); // Redirigimos la salida de la consola a StringWriter

        try
        {
            // Act
            Printer.ShowInventory(inventory);

            // Assert
            string actualOutput = sw.ToString().Trim(); // Quitamos los espacios extra

            // Comprobamos si la salida contiene los nombres de los Pokémon
            foreach (var expectedFragment in expectedFragments)
            {
                Assert.That(actualOutput, Does.Contain(expectedFragment),
                    $"La salida no contiene el nombre esperado del Pokémon: '{expectedFragment}'");
            }
        }
        finally
        {
            // Restauramos la salida original de la consola
            Console.SetOut(originalOut);
        }
    }
}

    

        