using Library.Game.Utilities;
using Library.Game.Pokemons;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using Library.Game.Attacks;


namespace Library.Tests
{
    [TestFixture]
    public class PrinterTests
    {
        private StringWriter _stringWriter;

        [SetUp]
        public void Setup()
        {
            // Redirigir la salida estándar de la consola a un StringWriter
            _stringWriter = new StringWriter();
            Console.SetOut(_stringWriter);
        }

        [TearDown]
        public void TearDown()
        {
            // Restaurar la salida estándar de la consola
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true });
            _stringWriter.Dispose();
        }

        [Test]
        public void StartPrint_ShouldDisplayWelcomeMessage()
        {
            // Act
            Printer.StartPrint();

            // Assert
            string output = _stringWriter.ToString();
            Assert.That(output, Does.Contain("Welcome to Pokemon Battle"), "Expected welcome message to be printed.");
        }

        [Test]
        public void EndPrint_ShouldDisplayThankYouMessage()
        {
            // Act
            Printer.EndPrint();

            // Assert
            string output = _stringWriter.ToString();
            Assert.That(output, Does.Contain("Thanks for playing!!"), "Expected thank you message to be printed.");
        }

        [Test]
        public void DisplayWinner_ShouldShowWinnerName()
        {
            // Act
            Printer.DisplayWinner("Ash");

            // Assert
            string output = _stringWriter.ToString();
            Assert.That(output, Does.Contain("The winner is Ash!!"), "Expected winner's name to be printed.");
        }


        [Test]
        public void YourTurn_ShouldDisplayPlayerTurnMessage()
        {
            // Act
            Printer.YourTurn("Ash");

            // Assert
            string output = _stringWriter.ToString();
            Assert.That(output, Does.Contain("Your turn Player Ash"), "Expected player's turn message to be printed.");
        }

        [Test]
        public void NameSelection_ShouldPromptForName()
        {
            // Act
            Printer.NameSelection();

            // Assert
            string output = _stringWriter.ToString();
            Assert.That(output, Does.Contain("Enter your name"), "Expected prompt for player name.");
        }

        [Test]
        public void IndexOutOfRange_ShouldDisplayErrorMessage()
        {
            // Act
            Printer.IndexOutOfRange(1, 20);

            // Assert
            string output = _stringWriter.ToString();
            Assert.That(output, Does.Contain("El valor debe ser mayor que 1"), "Expected error message to specify lower limit.");
            Assert.That(output, Does.Contain("y menor que 20"), "Expected error message to specify upper limit.");
        }

        [Test]
        public void ShowSelectedPokemon_ShouldDisplayPokemonDetails()
        {
            // Arrange
            var pokemon = new Pokemon("Pikachu", 35, "Electric", new List<IAttack>());
            pokemon.InitialHealth = 100;

            // Act
            Printer.ShowSelectedPokemon(pokemon, "Ash");

            // Assert
            string output = _stringWriter.ToString();
            Assert.That(output, Does.Contain("This is your pokemon Ash!"), "Expected player's name in message.");
            Assert.That(output, Does.Contain("Name: Pikachu"), "Expected Pokémon's name in message.");
            Assert.That(output, Does.Contain("Life: 35/100"), "Expected Pokémon's health details in message.");
        }
    }
}
