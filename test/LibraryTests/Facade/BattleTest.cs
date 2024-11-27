using Library.Facade;
using Library.Game.Attacks;
using Library.Game.Players;
using Library.Game.Pokemons;
using Library.Game.Utilities;
using Library.Tests;
using Library.Tests.Facade;
using NUnit.Framework;

namespace LibraryTests.Facade
{
    /// <summary>
    /// Tests for Battle class
    /// </summary>
    [TestFixture]
    public class BattleTests
    {
        private IPokemon _mockPokemon1;
        private IPokemon _mockPokemon2;
        private StringWriter _stringWriter;
        private TextWriter _originalOutput;

        [SetUp]
        public void Setup()
        {
            // Redirect Console output to prevent "handle is invalid" error
            _originalOutput = Console.Out;
            _stringWriter = new StringWriter();
            Console.SetOut(_stringWriter);
            _mockPokemon1 = new MockPokemon("Charmander");
            _mockPokemon2 = new MockPokemon("Bulbasaur");
            
        }

        [TearDown]
        public void TearDown()
        {
            // Restore original Console output
            Console.SetOut(_originalOutput);
        }

        [Test]
        public void DisplayAdvantagesTestIfPlayer2IsNull()
        {
            Console.SetOut(_originalOutput);
            var battle = new MockBattle();
            
            var pokemons = new List<IPokemon> { _mockPokemon1, _mockPokemon2 };
            Player.InitializePlayer1("Ash", pokemons, _mockPokemon1);
            Player.InitializePlayer2(null, null, null);
            Player player1 = Player.Player1;
            Player player2 = Player.Player2;
            var ex = Assert.Catch<Exception>(() => battle.DisplayAdvantages(player1, player2));
            if (ex != null) Assert.That(ex.Message, Does.Contain("rival"));
        }
        
        [Test]
        public void DisplayAdvantagesTestIfPlayer1IsNull()
        {
            Console.SetOut(_originalOutput);
            var battle = new MockBattle();
            
            var pokemons = new List<IPokemon> { _mockPokemon1, _mockPokemon2 };
            Player.InitializePlayer1(null, null, null);
            Player.InitializePlayer2("Ash", pokemons, _mockPokemon1);
            Player player1 = Player.Player1;
            Player player2 = Player.Player2;
            var ex = Assert.Catch<Exception>(() => battle.DisplayAdvantages(player1, player2));
            if (ex != null) Assert.That(ex.Message, Does.Contain("player"));
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
}

namespace Library.Tests.Facade
{
    /// <summary>
    /// 
    /// </summary>
    public class MockBattle
    {
        /// <summary>
        /// Calculates and displays the advantage of each player.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="rival"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void DisplayAdvantages(IPlayer player, IPlayer rival)
        {
            if (player == null || rival == null)
            {
                throw new ArgumentNullException(player == null ? "player" : "rival");
                return;
            }
        
            // 1) Create a method that returns the advantage taking into account: the item count of each player.
            // 2) Create a method that returns the advantage taking into account: the Pokémon count of each player.
            // this can be done by calling the returnAdvantage in the Calculator class. 
            int advantagePlayer1 = Calculator.ReturnAdvantage(player); // store P1 advantage.
            int advantagePlayer2 = Calculator.ReturnAdvantage(rival); // store P2 advantage.
        
            // 3) now we print it to the user./*
            Console.Clear();
            Printer.Advantage(advantagePlayer1, player.Name);
            Printer.Advantage(advantagePlayer2, rival.Name);
            Printer.PressToContinue(); // wait for player input to make sure they see them.
        }
    }
}
