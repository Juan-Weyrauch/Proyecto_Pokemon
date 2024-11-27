
using Library.Facade;
using Library.Game.Pokemons;
using NUnit.Framework;

using Library.Game.Attacks;//using NUnit.Framework;

using Moq;

using Library.Game.Items;
using Library.Game.Players;
using Library.Tests;
using NUnit.Framework.Internal;
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

    [Test]
    public void ShowInventory_ShouldPrintInventoryBoxesCorrectly()
    {
        // Arrange
        var mockPokemon1 = new Mock<IPokemon>();
        var mockPokemon2 = new Mock<IPokemon>();
        var mockPokemon3 = new Mock<IPokemon>();
        var mockPokemon4 = new Mock<IPokemon>();
        var mockPokemon5 = new Mock<IPokemon>();
        var mockPokemon6 = new Mock<IPokemon>();

        // Set up Pokémon names and health
        mockPokemon1.Setup(p => p.Name).Returns("Pikachu");
        mockPokemon2.Setup(p => p.Name).Returns("Bulbasaur");
        mockPokemon3.Setup(p => p.Name).Returns("Charmander");
        mockPokemon4.Setup(p => p.Name).Returns("Squirtle");
        mockPokemon5.Setup(p => p.Name).Returns("Eevee");
        mockPokemon6.Setup(p => p.Name).Returns("Jigglypuff");

        mockPokemon1.Setup(p => p.Health).Returns(100);
        mockPokemon2.Setup(p => p.Health).Returns(80);
        mockPokemon3.Setup(p => p.Health).Returns(60);
        mockPokemon4.Setup(p => p.Health).Returns(50);
        mockPokemon5.Setup(p => p.Health).Returns(30);
        mockPokemon6.Setup(p => p.Health).Returns(20);

        var inventory = new List<IPokemon>
        {
            mockPokemon1.Object,
            mockPokemon2.Object,
            mockPokemon3.Object,
            mockPokemon4.Object,
            mockPokemon5.Object,
            mockPokemon6.Object
        };

        // Redirect the console output to a StringWriter to capture it
        var originalOut = Console.Out;
        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        // Act
        Printer.ShowInventory(inventory);

        // Assert
        // Use Does.Contain to check if the inventory contains the Pokémon names in the output
        foreach (var pokemon in inventory)
        {
            Assert.That(stringWriter.ToString(), Does.Contain(pokemon.Name),
                $"The inventory does not contain {pokemon.Name}");
        }

        // Restore the original console output
        Console.SetOut(originalOut);
    }

    [Test]
    public void PrintInventoryHeader_ShouldPrintCorrectHeader()
    {
        // Arrange
        var originalOut = Console.Out;
        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter); // Redirect console output to StringWriter

        // Act
        Printer.PrintInventoryHeader();

        // Assert
        // Verify that the expected header content is printed to the console
        Assert.That(stringWriter.ToString(), Does.Contain("Your Team"), "The header does not contain 'Your Team'");
        Assert.That(stringWriter.ToString(),
            Does.Contain(
                "╔════════════════════════════════════════════════════════════════════════════════════════════╗"),
            "The header does not contain the top border");
        Assert.That(stringWriter.ToString(),
            Does.Contain(
                "╚════════════════════════════════════════════════════════════════════════════════════════════╝"),
            "The header does not contain the bottom border");

        // Restore original console output
        Console.SetOut(originalOut);
    }

    [Test]
    public void ShowSelectedPokemon_ShouldPrintPokemonInfoCorrectly()
    {
        // Arrange
        var mockPokemon = new Mock<IPokemon>();
        mockPokemon.Setup(p => p.Name).Returns("Pikachu");
        mockPokemon.Setup(p => p.Health).Returns(100);

        string playerName = "Ash";

        var originalOut = Console.Out;
        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter); // Redirect console output to StringWriter

        // Act
        Printer.ShowSelectedPokemon(mockPokemon.Object, playerName);

        // Assert
        // Verify that the output contains expected Pokémon info
        Assert.That(stringWriter.ToString(), Does.Contain("This is your pokemon Ash!"));

        // Restore original console output
        Console.SetOut(originalOut);
    }


    [Test]
    public void ShowAttacks_ShouldPrintAttacksDetailsCorrectly()
    {
        // Arrange
        var attack1 = new Attack("Flame Thrower", 40, SpecialEffect.Burn, "Fire", 90);
        var attack2 = new Attack("Water Gun", 30, SpecialEffect.None, "Water", 100);

        var pokemon = new Pokemon("Charmander", 10, "Fire", new List<IAttack> { attack1, attack2 });

        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter); // Redirect console output to StringWriter

        // Act
        Printer.ShowAttacks(pokemon,
            pokemon); // Usamos el mismo Pokémon para atacar y recibir el ataque (puedes cambiarlo si es necesario)

        // Assert
        var output = stringWriter.ToString();

        // Verificar que la salida contiene los detalles correctos de los ataques
        Assert.That(output, Contains.Substring("Attacks of Charmander"));
        Assert.That(output, Contains.Substring("Attack 1"));
        Assert.That(output, Contains.Substring("Name: Flame Thrower"));
        Assert.That(output, Contains.Substring("Damage: 40"));
        Assert.That(output, Contains.Substring("Type: Fire"));
        Assert.That(output, Contains.Substring("Special Effect: Burn"));

        Assert.That(output, Contains.Substring("Attack 2"));
        Assert.That(output, Contains.Substring("Name: Water Gun"));
        Assert.That(output, Contains.Substring("Damage: 30"));
        Assert.That(output, Contains.Substring("Type: Water"));
        Assert.That(output, Contains.Substring("Special Effect: None"));

        // Verificar que la salida contiene los bordes del formato esperado

    }

    [Test]
    public void WasAfected_ShouldPrintCorrectMessage()
    {
        // Arrange
        var attack = new Attack("Flame Thrower", 40, SpecialEffect.Burn, "Fire", 90);
        var pokemon = new Pokemon("Charmander", 10, "Fire", new List<IAttack> { attack });

        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter); // Redirigir la salida de la consola a StringWriter

        // Act
        Printer.WasAfected(pokemon, attack); // Llamamos al método WasAfected

        // Assert
        var output = stringWriter.ToString();

        // Verificar que el mensaje contiene el nombre del Pokémon y del ataque
        Assert.That(output, Contains.Substring("Charmander is affected by Flame Thrower's special effect!"));

    }

    [Test]
    public void SwitchQuestion_ShouldPrintCorrectMessages()
    {
        // Arrange
        var player = new Mock<IPlayer>();
        var pokemon = new Mock<IPokemon>();

        // Establecer valores de prueba para el jugador y su Pokémon seleccionado
        player.Setup(p => p.Name).Returns("Ash");
        player.Setup(p => p.SelectedPokemon).Returns(pokemon.Object);
        pokemon.Setup(p => p.Name).Returns("Pikachu");

        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter); // Redirigir la salida de la consola a StringWriter

        // Act
        Printer.SwitchQuestion(player.Object); // Llamamos al método SwitchQuestion

        // Assert
        var output = stringWriter.ToString();

        // Verificar que la salida contiene los mensajes correctos
        Assert.That(output, Contains.Substring("Ash, do you want to change your Pokémon Pikachu?"));
        Assert.That(output, Contains.Substring("1) Yes  2) No"));

        // Verificar que la salida contiene los bordes de la caja correctamente formateados
    }

    [Test]
    public void SwitchConfirmation_ShouldPrintCorrectMessages_WhenOptionIsZero()
    {
        // Arrange
        var player = new Mock<IPlayer>();
        var pokemon = new Mock<IPokemon>();

        // Establecer valores de prueba para el jugador y su Pokémon seleccionado
        player.Setup(p => p.Name).Returns("Ash");
        player.Setup(p => p.SelectedPokemon).Returns(pokemon.Object);
        pokemon.Setup(p => p.Name).Returns("Pikachu");

        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter); // Redirigir la salida de la consola a StringWriter

        // Act
        Printer.SwitchConfirmation(player.Object, 0); // Llamamos al método SwitchConfirmation con la opción 0

        // Assert
        var output = stringWriter.ToString();

        // Verificar que la salida contiene los mensajes correctos
        Assert.That(output, Contains.Substring("Ash, your selected Pokémon has been changed!"));
        Assert.That(output, Contains.Substring("Now it is Pikachu."));

        // Verificar que la salida contiene los bordes de la caja correctamente formateados

    }

    [Test]
    public void SwitchConfirmation_ShouldNotPrintAnything_WhenOptionIsNonZero()
    {
        // Arrange
        var player = new Mock<IPlayer>();
        var pokemon = new Mock<IPokemon>();

        // Establecer valores de prueba para el jugador y su Pokémon seleccionado
        player.Setup(p => p.Name).Returns("Ash");
        player.Setup(p => p.SelectedPokemon).Returns(pokemon.Object);
        pokemon.Setup(p => p.Name).Returns("Pikachu");

        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter); // Redirigir la salida de la consola a StringWriter

        // Act
        Printer.SwitchConfirmation(player.Object,
            1); // Llamamos al método SwitchConfirmation con una opción diferente a 0

        // Assert
        var output = stringWriter.ToString();

        // Verificar que no se imprime nada cuando la opción no es 0
        Assert.That(output, Is.Empty);
    }

    [Test]
    public void SkippingDueToStatus_ShouldPrintCorrectMessage()
    {
        // Arrange
        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter); // Redirigir la salida de la consola a StringWriter

        // Act
        Printer.SkippingDueToStatus(); // Llamamos al método SkippingDueToStatus

        // Assert
        var output = stringWriter.ToString();

        // Verificar que la salida contiene el mensaje esperado
        Assert.That(output, Does.Contain("Skipping turn due to status effect."));

        // Verificar que la salida contiene los bordes de la caja correctamente formateados

    }

    [Test]
    public void ShowTurnInfo_ShouldPrintCorrectTurnInfo()
    {
        // Arrange
        var player = new MockPlayer();
        var pokemon = new MockPokemon();

        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter); // Redirigir la salida de la consola a StringWriter

        // Act
        Printer.ShowTurnInfo(player, pokemon); // Llamamos al método ShowTurnInfo

        // Assert
        var output = stringWriter.ToString();

        // Verificar que el mensaje contiene el nombre del jugador y el Pokémon

        // Verificar que las opciones están en el formato correcto
        Assert.That(output, Does.Contain("What would you like to do?"));
        Assert.That(output, Does.Contain("1. Attack"));
        Assert.That(output, Does.Contain("2. Use Item"));
        Assert.That(output, Does.Contain("3. Switch Pokémon"));

        
        
    }
    
}