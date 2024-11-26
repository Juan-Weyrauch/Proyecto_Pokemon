using Library.Game.Pokemons;
using Library.Game.Players;
using Library.Game.Utilities;
using Library.Facade;
using NUnit.Framework;
using Library.Game.Attacks;
using System.Collections.Generic;

namespace Library.Tests
{
    [TestFixture]
    public class FacadeTests
    {
        private IPokemon _bulbasaur;
        private IPokemon _charmander;

        [SetUp]
        public void Setup()
        {
            // Crear Pokémon de prueba
            string playerName = $"p1";
            string playerName2 = $"p2";

            // Crear el catálogo de Pokémon
        }

        [Test]
        public void Start_ShouldExit_WhenSelectionIs2_AndContinue_WhenSelectionIs1()
        {
            // --- Test Case 1: Selection 1 (Continue) ---
        
            // Arrange: Simulate user input for selecting "1"
            var input1 = new StringReader("1");
            Console.SetIn(input1);

            // Redirect the console output to capture printed output
            var output1 = new StringWriter();
            Console.SetOut(output1);

            // Act: Call Start method (this should continue the flow)
            Facade.Facade.Start();

            // Assert: Verify that the output contains the expected start message
            string consoleOutput1 = output1.ToString();
            Assert.That(consoleOutput1, Does.Contain("╔════════════════════════════╗"));
            Assert.That(consoleOutput1, Does.Contain("╚════════════════════════════╝"));

            // --- Test Case 2: Selection 2 (Exit) ---
        
            // Arrange: Simulate user input for selecting "2"
            var input2 = new StringReader("2");
            Console.SetIn(input2);

            // Redirect the console output to capture printed output
            var output2 = new StringWriter();
            Console.SetOut(output2);

            // Act: Call Start method (this should trigger exit)
            bool didExit = false;
            try
            {
                Facade.Facade.Start();
            }
            catch (ExitException)
            {
                didExit = true; // Expected behavior is that the program exits
            }

            // Assert: Verify that the exit behavior occurred
            Assert.That(didExit, Is.True);
        }
}
        

        public class ExitException : Exception { } // Esta excepción simula el comportamiento de Environment.Exit(0)

    /*    [Test]
        public void Start_CallsSelections_WhenSelectionIs1()
        {
            // Redirigir entrada simulada (el usuario ingresa "1")
            var input = new StringReader("1\n");
            Console.SetIn(input);

            // Redirigir salida estándar para capturar el texto impreso
            var output = new StringWriter();
            Console.SetOut(output);

            // Ejecutar el método
            //Facade.Facade.Start();

            // Verificar la salida esperada
            string consoleOutput = output.ToString();
            Assert.That(consoleOutput.Contains("Welcome!"), "El mensaje de bienvenida no se imprimió correctamente.");

            // Verificar que el programa continuó (esto requiere que `Facade.Selections` funcione correctamente)
        }
*/
    }