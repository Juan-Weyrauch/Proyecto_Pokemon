using System.Reflection;
using Library.Game.Attacks;
using Library.Game.Pokemons;
using NUnit.Framework;

namespace LibraryTests.Game.Player
{
    /// <summary>
    /// Tests for the Player class.
    /// </summary>
    [TestFixture]
    public class PlayerTests
    {
        private IPokemon _mockPokemon1;
        private IPokemon _mockPokemon2;

        /// <summary>
        /// Sets up the test environment by initializing mock Pokémon and resetting the player state.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            //Player.ResetForTesting(); // Restablecer el estado de los jugadores
            _mockPokemon1 = new MockPokemon("Charmander");
            _mockPokemon2 = new MockPokemon("Bulbasaur");
        }

        /// <summary>
        /// Tests the initialization of player 1 and checks that the singleton instance is created correctly.
        /// </summary>
        [Test]
        public void InitializePlayer1CreatesSingletonInstance()
        {
            // Arrange
            var pokemons = new List<IPokemon> { _mockPokemon1, _mockPokemon2 };

            // Act

            Library.Game.Players.Player.InitializePlayer1("Ash", pokemons, _mockPokemon1);
            var player1 = Library.Game.Players.Player.Player1;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(player1.Name, Is.EqualTo("Ash"));
                Assert.That(player1.Pokemons, Is.EquivalentTo(pokemons));
                Assert.That(player1.SelectedPokemon, Is.EqualTo(_mockPokemon1));
                Assert.That(player1.Turn, Is.EqualTo(0));
            });
        }

        /// <summary>
        /// Tests that an exception is thrown when attempting to initialize player 1 again.
        /// </summary>
        [Test]
        public void InitializePlayer1ThrowsExceptionIfAlreadyInitialized()
        {
            // Arrange
            var pokemons = new List<IPokemon> { _mockPokemon1, _mockPokemon2 };
            Library.Game.Players.Player.InitializePlayer1("Ash", pokemons, _mockPokemon1);

            // Act & Assert
            var ex = Assert.Throws<System.InvalidOperationException>(() =>
                Library.Game.Players.Player.InitializePlayer1("Ash", pokemons, _mockPokemon1));

            // Additional assertions to verify no unintended state change
            Assert.Multiple(() =>
            {
                Assert.That(ex.Message, Is.EqualTo("Player1 has already been initialized."));
                Assert.That(Library.Game.Players.Player.Player1.Name, Is.EqualTo("Ash"));
                Assert.That(Library.Game.Players.Player.Player1.Pokemons, Is.EquivalentTo(pokemons));
            });
        }

        [Test]
        public void Player2Getter_ShouldThrowInvalidOperationException_WhenPlayer2IsNull()
        {
            // Usamos Reflection para forzar que _player2 sea null
            var playerType = typeof(Library.Game.Players.Player);
            var player2Field = playerType.GetField("_player2", BindingFlags.NonPublic | BindingFlags.Static);

            // Aseguramos que _player2 sea null
            player2Field.SetValue(null, null);

            // Act & Assert
            var ex = Assert.Throws<InvalidOperationException>(() =>
            {
                var player = Library.Game.Players.Player.Player2; // Esto debería disparar la excepción
            });

            // Verificamos que el mensaje de la excepción sea correcto
            Assert.That(ex.Message, Is.EqualTo("Player2 has not been initialized."));
        }

        /// <summary>
        /// Tests that the selected Pokémon is correctly switched for player 1.
        /// </summary>
        [Test]
        public void SwitchPokemonChangesSelectedPokemon()
        {
            // Arrange
            var pokemons = new List<IPokemon> { _mockPokemon1, _mockPokemon2 };
            Library.Game.Players.Player.InitializePlayer1("Ash", pokemons, _mockPokemon1);
            var player1 = Library.Game.Players.Player.Player1;

            // Act
            player1.SwitchPokemon(0); // Usando índice 0 para el primer Pokémon

            // Assert
            Assert.That(player1.SelectedPokemon, Is.EqualTo(pokemons[0]));
        }
    

        [Test]
            public void CarryToCementerioMovesPokemonToCementerio()
            {
                // Arrange
                var pokemons = new List<IPokemon> { _mockPokemon1, _mockPokemon2 };
                Library.Game.Players.Player.InitializePlayer1("Ash", pokemons, _mockPokemon1);
                var player1 = Library.Game.Players.Player.Player1;

                // Act
                player1.CarryToCementerio();

                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(player1.Cementerio, Contains.Item(_mockPokemon1));
                    Assert.That(player1.Pokemons, Does.Not.Contain(_mockPokemon1));
                });
            }
        

        /// <summary>
        /// Tests the initialization of player 2 and checks that the singleton instance is created correctly.
        /// </summary>
        [Test]
        public void InitializePlayer2CreatesSingletonInstance()
        {
            // Arrange
            var pokemons = new List<IPokemon> { _mockPokemon1, _mockPokemon2 };

            // Act
            Library.Game.Players.Player.InitializePlayer2("Misty", pokemons, _mockPokemon2);
            var player2 = Library.Game.Players.Player.Player2;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(player2.Name, Is.EqualTo("Misty"));
                Assert.That(player2.Pokemons, Is.EquivalentTo(pokemons));
                Assert.That(player2.SelectedPokemon, Is.EqualTo(_mockPokemon2));
                Assert.That(player2.Turn, Is.EqualTo(0));
            });
        }
        /// <summary>
        /// Tests that an exception is thrown when attempting to initialize player 2 again.
        /// </summary>

        [Test]
        public void InitializePlayer2ThrowsExceptionIfAlreadyInitialized()
        {
            // Arrange
            var pokemons = new List<IPokemon> { _mockPokemon1, _mockPokemon2 };
            Library.Game.Players.Player.InitializePlayer2("Misty", pokemons, _mockPokemon2);

            // Act & Assert
            var ex = Assert.Throws<System.InvalidOperationException>(() =>
                Library.Game.Players.Player.InitializePlayer2("Misty", pokemons, _mockPokemon2));

            // Additional assertions to verify no unintended state change
            Assert.Multiple(() =>
            {
                Assert.That(ex.Message, Is.EqualTo("Player2 has already been initialized."));
                Assert.That(Library.Game.Players.Player.Player2.Name, Is.EqualTo("Misty"));
                Assert.That(Library.Game.Players.Player.Player2.Pokemons, Is.EquivalentTo(pokemons));
            });
        }

        [Test]
        public void ReturnPokemonCountIsCorerrect()
        {
            var pokemons = new List<IPokemon> { _mockPokemon1, _mockPokemon2 };
            Library.Game.Players.Player.InitializePlayer2("Misty", pokemons, _mockPokemon2);
            // Act & Assert
            var player2 = Library.Game.Players.Player.Player2;
            Assert.That(player2.ReturnPokemonsCount(), Is.EquivalentTo(pokemons));
        }

        [Test]
        public void ReturnItemCountIsCorrect()
        {
            var pokemons = new List<IPokemon> { _mockPokemon1, _mockPokemon2 };
            Library.Game.Players.Player.InitializePlayer2("Misty", pokemons, _mockPokemon2);
            var player2 = Library.Game.Players.Player.Player2;
            Assert.That(player2.ReturnItemCount(), Is.EquivalentTo(pokemons));
        }

    }

    /// <summary>
    /// Mock implementation of <see cref="IPokemon"/> for testing purposes.
    /// </summary>
    public class MockPokemon : IPokemon
    {
        private SpecialEffect _state;

        /// <summary>
        /// Gets or sets the name of the Pokémon.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the current health of the Pokémon.
        /// </summary>
        public int Health { get; set; }

        /// <summary>
        /// Gets or sets the defense value of the Pokémon.
        /// </summary>
        public int Defense { get; set; }

        /// <summary>
        /// Gets or sets the type of the Pokémon (e.g., "Fire", "Water").
        /// </summary>
        public string Type { get; set; }

        public int SleepTurns { get; }

        SpecialEffect IPokemon.State
        {
            get => _state;
            set => _state = value;
        }

        /// <summary>
        /// Gets or sets the current state of the Pokémon (e.g., "Normal", "Poisoned").
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// Gets or sets the list of attacks available to the Pokémon.
        /// </summary>
        public List<IAttack> AtackList { get; }

        /// <summary>
        /// Gets or sets the initial health value of the Pokémon (when it was first created).
        /// </summary>
        public int InitialHealth { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MockPokemon"/> class with the specified name.
        /// </summary>
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
        /// <summary>
        /// Retrieves the attack at the specified index.
        /// </summary>
        public IAttack GetAttack(int index) => AtackList[index];
        /// <summary>
        /// Creates a clone of this <see cref="MockPokemon"/>.
        /// </summary>
        public IPokemon Clone() => new MockPokemon(Name);

        public void DecreaseHealth(int damage)
        {
            throw new NotImplementedException();
        }

        public void ProcessTurnEffects()
        {
            throw new NotImplementedException();
        }

        public void ResetStatus()
        {
            throw new NotImplementedException();
        }

        public void ApplyStatusEffect(SpecialEffect effect)
        {
            throw new NotImplementedException();
        }
    }
    
    
}
