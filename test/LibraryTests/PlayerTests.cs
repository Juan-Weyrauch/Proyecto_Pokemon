using Library.Game.Players;
using Library.Game.Pokemons;
using Library.Game.Attacks;
using NUnit.Framework;
using System.Collections.Generic;

namespace Library.Tests
{
    /// <summary>
    /// Tests for the Player class.
    /// </summary>
    [TestFixture]
    public class PlayerTests
    {
        private IPokemon mockPokemon1;
        private IPokemon mockPokemon2;
        private IPokemon mockPokemon3;

        [SetUp]
        public void Setup()
        {
            Player.ResetForTesting(); // Restablecer el estado de los jugadores
            mockPokemon1 = new MockPokemon("Charmander");
            mockPokemon2 = new MockPokemon("Bulbasaur");
            mockPokemon3 = new MockPokemon("Squirtle");
        }

        [Test]
        public void InitializePlayer1_CreatesSingletonInstance()
        {
            // Arrange
            var pokemons = new List<IPokemon> { mockPokemon1, mockPokemon2 };

            // Act
            Player.InitializePlayer1("Ash", pokemons, mockPokemon1);
            var player1 = Player.Player1;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(player1.Name, Is.EqualTo("Ash"));
                Assert.That(player1.Pokemons, Is.EquivalentTo(pokemons));
                Assert.That(player1.SelectedPokemon, Is.EqualTo(mockPokemon1));
                Assert.That(player1.Turn, Is.EqualTo(0));
            });
        }

        [Test]
        public void InitializePlayer1_ThrowsExceptionIfAlreadyInitialized()
        {
            // Arrange
            var pokemons = new List<IPokemon> { mockPokemon1, mockPokemon2 };
            Player.InitializePlayer1("Ash", pokemons, mockPokemon1);

            // Act & Assert
            var ex = Assert.Throws<System.InvalidOperationException>(() =>
                Player.InitializePlayer1("Ash", pokemons, mockPokemon1));

            // Additional assertions to verify no unintended state change
            Assert.Multiple(() =>
            {
                Assert.That(ex.Message, Is.EqualTo("Player1 has already been initialized."));
                Assert.That(Player.Player1.Name, Is.EqualTo("Ash"));
                Assert.That(Player.Player1.Pokemons, Is.EquivalentTo(pokemons));
            });
        }

        [Test]
        public void SwitchPokemon_ChangesSelectedPokemon()
        {
            // Arrange
            var pokemons = new List<IPokemon> { mockPokemon1, mockPokemon2 };
            Player.InitializePlayer1("Ash", pokemons, mockPokemon1);
            var player1 = Player.Player1;

            // Act
            player1.SwitchPokemon(1);

            // Assert
            Assert.That(player1.SelectedPokemon, Is.EqualTo(mockPokemon2));
        }

        [Test]
        public void CarryToCementerio_MovesPokemonToCementerio()
        {
            // Arrange
            var pokemons = new List<IPokemon> { mockPokemon1, mockPokemon2 };
            Player.InitializePlayer1("Ash", pokemons, mockPokemon1);
            var player1 = Player.Player1;

            // Act
            player1.CarryToCementerio();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(player1.Cementerio, Contains.Item(mockPokemon1));
                Assert.That(player1.Pokemons, Does.Not.Contain(mockPokemon1));
            });
        }

        [Test]
        public void InitializePlayer2_CreatesSingletonInstance()
        {
            // Arrange
            var pokemons = new List<IPokemon> { mockPokemon1, mockPokemon2 };

            // Act
            Player.InitializePlayer2("Misty", pokemons, mockPokemon2);
            var player2 = Player.Player2;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(player2.Name, Is.EqualTo("Misty"));
                Assert.That(player2.Pokemons, Is.EquivalentTo(pokemons));
                Assert.That(player2.SelectedPokemon, Is.EqualTo(mockPokemon2));
                Assert.That(player2.Turn, Is.EqualTo(0));
            });
        }

        [Test]
        public void InitializePlayer2_ThrowsExceptionIfAlreadyInitialized()
        {
            // Arrange
            var pokemons = new List<IPokemon> { mockPokemon1, mockPokemon2 };
            Player.InitializePlayer2("Misty", pokemons, mockPokemon2);

            // Act & Assert
            var ex = Assert.Throws<System.InvalidOperationException>(() =>
                Player.InitializePlayer2("Misty", pokemons, mockPokemon2));

            // Additional assertions to verify no unintended state change
            Assert.Multiple(() =>
            {
                Assert.That(ex.Message, Is.EqualTo("Player2 has already been initialized."));
                Assert.That(Player.Player2.Name, Is.EqualTo("Misty"));
                Assert.That(Player.Player2.Pokemons, Is.EquivalentTo(pokemons));
            });
        }
    }

    /// <summary>
    /// Mock implementation of IPokemon for testing purposes.
    /// </summary>
    public class MockPokemon : IPokemon
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int Defense { get; set; }
        public string Type { get; set; }
        public int State { get; set; }
        public List<IAttack> AtackList { get; set; }
        public int InitialHealth { get; set; }

        public MockPokemon(string name)
        {
            Name = name;
            Health = 100;
            Defense = 50;
            Type = "Normal";
            State = 0;
            AtackList = new List<IAttack>();
            InitialHealth = 100;
        }

        public IAttack GetAttack(int index) => AtackList[index];
        public IPokemon Clone() => new MockPokemon(Name);
    }
}
