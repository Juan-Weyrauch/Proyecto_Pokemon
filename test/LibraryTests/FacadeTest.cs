
using NUnit.Framework;
using Library.Game.Attacks;
using Library.Game.Players;
using Library.Game.Pokemons;
using Library.Facade;

namespace LibraryTests
{
    [TestFixture]
    public class FacadeTests
    {
        private StringWriter consoleOutput;
        private StringReader consoleInput;

        [SetUp]
        public void Setup()
        {
            // Redirigir la salida de la consola para capturar los mensajes
            consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);
        }

        [TearDown]
        public void TearDown()
        {
            // Restaurar las salidas de la consola
            consoleOutput.Dispose();
            Console.SetOut(Console.Out);

            consoleInput?.Dispose();
            Console.SetIn(Console.In);
        }

        [Test]
        public void StartShouldDisplayStartMenuAndExitOnOption2()
        {
            // Simular entrada del usuario seleccionando la opción 2 (Salir)
            consoleInput = new StringReader("2");
            Console.SetIn(consoleInput);

            // Act
            Assert.Throws<InvalidOperationException>(() => Facade.Start());

            // Assert
            string output = consoleOutput.ToString();
            Assert.That(output, Does.Contain("Welcome to Pokemon Battle"));
            Assert.That(output, Does.Contain("1) Start"));
            Assert.That(output, Does.Contain("2) Leave"));
            Assert.That(output, Does.Contain("Thanks for playing!!"));
        }

        [Test]
        public void StartShouldProceedToSelectionsOnOption1()
        {
            // Simular entrada del usuario seleccionando la opción 1 (Continuar)
            consoleInput = new StringReader("1");
            Console.SetIn(consoleInput);

            // Mockear `Selections` para evitar lógica compleja
            Assert.DoesNotThrow(() => Facade.Start());

            // Assert
            string output = consoleOutput.ToString();
            Assert.That(output, Does.Contain("Welcome to Pokemon Battle"));
            Assert.That(output, Does.Contain("1) Start"));
            Assert.That(output, Does.Not.Contain("Thanks for playing!!"));
        }

        [Test]
        public void CreatePlayersShouldInitializeBothPlayers()
        {
            // Arrange
            string playerName1 = "Ash";
            string playerName2 = "Misty";
            
            // Crear una lista de Pokémon (requiere los argumentos correctos para el constructor)
            List<IPokemon> pokemons = new List<IPokemon>
            {
                new Pokemon("Pikachu", 5, "Electric", new List<IAttack>()),  // Asegúrate de pasar los argumentos correctos
                new Pokemon("Charizard", 10, "Fire", new List<IAttack>())
            };
            IPokemon selectedPokemon = pokemons[0];

            // Act
            // Inicializar jugadores a través de la función de la fachada (ya que es privada, solo probamos indirectamente)
            Player.InitializePlayer1(playerName1, pokemons, selectedPokemon);
            Player.InitializePlayer2(playerName2, pokemons, selectedPokemon);

            // Assert
            Assert.That(Player.Player1.Name, Is.EqualTo(playerName1));
            Assert.That(Player.Player1.SelectedPokemon.Name, Is.EqualTo("Pikachu"));
            Assert.That(Player.Player2.Name, Is.EqualTo(playerName2));
            Assert.That(Player.Player2.SelectedPokemon.Name, Is.EqualTo("Pikachu"));
        }
    }
}
