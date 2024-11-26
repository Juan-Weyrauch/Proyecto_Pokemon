
using NUnit.Framework;
using System;
using System.IO;

using Library.Game.Utilities; // Asegúrate de que este es el espacio de nombres correcto

namespace Library.Tests
{
    [TestFixture]
    public class PrinterTests
    {
        private StringWriter _stringWriter;
        private TextWriter _originalConsoleOutput;

        [SetUp]
        public void Setup()
        {
            // Captura la salida de consola
            _stringWriter = new StringWriter();
            _originalConsoleOutput = Console.Out;
            Console.SetOut(_stringWriter); // Redirige la salida de consola
        }

        [TearDown]
        public void TearDown()
        {
            // Restaura la salida original de la consola después de cada prueba
            Console.SetOut(_originalConsoleOutput);
        }

        [Test]
        public void StartPrint_ShouldPrintStartMessage()
        {
            // Act: Llama al método que deseas probar
            Printer.StartPrint();

            // Assert: Verifica el contenido de la salida
            var result = _stringWriter.ToString();
            Assert.That(result, Does.Contain("Welcome to Pokemon Battle"));
            Assert.That(result, Does.Contain("1) Start"));
            Assert.That(result, Does.Contain("2) Leave"));
        }

        [Test]
        public void EndPrint_ShouldPrintEndMessage()
        {
            // Act
            Printer.EndPrint();

            // Assert
            var result = _stringWriter.ToString();
            Assert.That(result, Does.Contain("Thanks for playing!!"));
        }

        [Test]
        public void DisplayWinner_ShouldPrintWinnerMessage()
        {
            // Act
            Printer.DisplayWinner("Ash");

            // Assert
            var result = _stringWriter.ToString();
            Assert.That(result, Does.Contain("The winner is Ash!!"));
        }

        [Test]
        public void IndexOutOfRange_ShouldPrintIndexErrorMessage()
        {
            // Act
            Printer.IndexOutOfRange(1, 10);

            // Assert
            var result = _stringWriter.ToString();
            Assert.That(result, Does.Contain("El valor debe ser mayor que 1"));
            Assert.That(result, Does.Contain("y menor que 10"));
        }

        [Test]
        public void NameSelection_ShouldPromptForName()
        {
            // Act
            Printer.NameSelection();

            // Assert
            var result = _stringWriter.ToString();
            Assert.That(result, Does.Contain("Enter your name"));
        }
    }
}
