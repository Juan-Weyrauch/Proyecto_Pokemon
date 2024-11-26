using Library.Facade;
using Library.Game.Utilities;
using Library.Game.Pokemons;
using Library.Game.Players;
using Library.Game.Attacks;
using NUnit.Framework;

namespace LibraryTests
{
    [TestFixture]
    public class FacadeTests
    {
        private StringWriter _consoleOutput;
        private StringReader _consoleInput;

        [SetUp]
        public void Setup()
        {
            // Redirigir la salida de consola
            _consoleOutput = new StringWriter();
            Console.SetOut(_consoleOutput);

            // Limpiar jugadores y catálogo antes de cada prueba
            typeof(Player)
                .GetField("_player1", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic)
                ?.SetValue(null, null);
            typeof(Player)
                .GetField("_player2", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic)
                ?.SetValue(null, null);
            Catalogue.GetPokedex()?.Clear();
        }

        [Test]
        public void CreatePlayers_FirstPlayer_ShouldInitializePlayer1()
        {
            // Arrange
            string playerName = "Ash";
            List<IPokemon> playerPokemons = CreateMockPokemonList();
            IPokemon selectedPokemon = playerPokemons[0];

            // Act
            Facade.CreatePlayers(playerName, playerPokemons, selectedPokemon, 0);

            // Assert
            var player1 = Player.Player1;
            Assert.That(player1, Is.Not.Null);
            Assert.That(player1.Name, Is.EqualTo(playerName));
            Assert.That(player1.SelectedPokemon, Is.EqualTo(selectedPokemon));
        }

        [Test]
        public void CreatePlayers_SecondPlayer_ShouldInitializePlayer2()
        {
            // Arrange
            string playerName = "Gary";
            List<IPokemon> playerPokemons = CreateMockPokemonList();
            IPokemon selectedPokemon = playerPokemons[1];

            // Act
            Facade.CreatePlayers(playerName, playerPokemons, selectedPokemon, 1);

            // Assert
            var player2 = Player.Player2;
            Assert.That(player2, Is.Not.Null);
            Assert.That(player2.Name, Is.EqualTo(playerName));
            Assert.That(player2.SelectedPokemon, Is.EqualTo(selectedPokemon));
        }

        [Test]
        public void CreatePlayers_InvalidPlayerIndex_ShouldThrowException()
        {
            // Arrange
            string playerName = "Invalid";
            List<IPokemon> playerPokemons = CreateMockPokemonList();
            IPokemon selectedPokemon = playerPokemons[0];

            // Act & Assert
            // Cambia el tipo de excepción si es diferente en tu implementación
            Assert.Throws<ArgumentException>(() => 
            {
                Facade.CreatePlayers(playerName, playerPokemons, selectedPokemon, 2);
            }, "Debería lanzar una excepción para un índice de jugador inválido");
        }


        private List<IPokemon> CreateMockPokemonList()
        {
            return new List<IPokemon>
            {
                new MockPokemon { Name = "Pikachu" },
                new MockPokemon { Name = "Charizard" },
                new MockPokemon { Name = "Squirtle" },
                new MockPokemon { Name = "Bulbasaur" },
                new MockPokemon { Name = "Charmander" },
                new MockPokemon { Name = "Pidgey" }
            };
        }

        private void MockPrinterAndCalculator()
        {
            // Mockear métodos de Printer
            typeof(Printer).GetMethod("StartPrint",
                    System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public)
                ?.Invoke(null, null);

            // Mockear métodos de Calculator
            typeof(Calculator).GetMethod("ValidateSelectionInGivenRange",
                    System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public)
                ?.Invoke(null, new object[] { 1, 2 });
        }
        private class MockPokemon : IPokemon
        {
            public string Name { get; set; }
            public int Health { get; set; }
            public int InitialHealth { get; set; }
            public int Defense { get; set; }
            public List<IAttack> AtackList { get; set; }
            public string Type { get; set; }
            public int State { get; set; }

            public MockPokemon()
            {
                Health = 100; // Valores por defecto
                InitialHealth = 100;
                Defense = 50;
                AtackList = new List<IAttack>
                {
                    new MockAttack { Name = "Thunderbolt", Damage = 40 },
                    new MockAttack { Name = "Quick Attack", Damage = 20 }
                };
                Type = "Electric";
                State = 0; // Normal
            }

            public IAttack GetAttack(int index)
            {
                return AtackList[index];
            }

            public IPokemon Clone()
            {
                return new MockPokemon
                {
                    Name = this.Name,
                    Health = this.Health,
                    InitialHealth = this.InitialHealth,
                    Defense = this.Defense,
                    AtackList = new List<IAttack>(this.AtackList),
                    Type = this.Type,
                    State = this.State
                };
            }
        }

        private class MockAttack : IAttack
        {
            public string Name { get; set; }
            public int Damage { get; set; }
            public int Special { get; set; }
            public string Type { get; set; }

            public MockAttack()
            {
                Name = "MockAttack";
                Damage = 50;
                Special = 0;
                Type = "Normal";
            }
        }
        
        [TearDown]
        public void Cleanup()
        {
            Console.SetIn(Console.In);
            Console.SetOut(Console.Out);
        }
    }
}

