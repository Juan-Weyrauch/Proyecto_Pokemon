using Library.Game.Attacks;
using Library.Game.Players;
using Library.Game.Pokemons;
using Library.Game.Utilities;
using NUnit.Framework;
using System.Collections.Generic;

namespace Library.Tests
{
    [TestFixture]
    public class CalculatorTests
    {
        private IAttack _fireAttack;
        private IAttack _electricAttack;
        private IAttack _waterAttack;
        private IPokemon _bulbasaur;
        private IPokemon _squirtle;
        private IPokemon _electrode;

        // Setup runs before each test
        [SetUp]
        public void Setup()
        {
            // Create example attacks
            _fireAttack = new Attack("Flame Thrower", 90, 1, "Fire");
            _electricAttack = new Attack("Thunderbolt", 50, 3, "Electric");
            _waterAttack = new Attack("Water Gun", 40, 0, "Water");

            // Create example Pokémon
            _bulbasaur = new Pokemon("Bulbasaur", 100, "Plant", new List<IAttack> { _waterAttack });
            _squirtle = new Pokemon("Squirtle", 100, "Water", new List<IAttack> { _waterAttack });
            _electrode = new Pokemon("Electrode", 100, "Electric", new List<IAttack> { _electricAttack });

            // Manually initialize players
            
        }

        // TearDown runs after each test
        [TearDown]
        public void TearDown()
        {
            // Cleanup resources if necessary (no need for null assignment in this case)
        }

        // Test effectiveness with normal damage multiplier
        [Test]
        public void CheckEffectiveness_ShouldReturnCorrectMultiplier_ForNormalEffectiveness()
        {
            // Act
            Player.InitializePlayer1("Juan", new List<IPokemon> { _bulbasaur, _squirtle, _electrode }, _squirtle);
            double effectiveness = Calculator.CheckEffectiveness(_fireAttack, _bulbasaur);

            // Assert: Fire should deal double damage to Plant
            Assert.That(effectiveness, Is.EqualTo(2.0), "Fire should deal double damage to Plant.");
        }

        // Test effectiveness with resistance multiplier
        [Test]
        public void CheckEffectiveness_ShouldReturnCorrectMultiplier_ForResistance()
        {
            // Act
            double effectiveness = Calculator.CheckEffectiveness(_fireAttack, _squirtle);

            // Assert: Fire should deal half damage to Water
            Assert.That(effectiveness, Is.EqualTo(0.5), "Fire should deal half damage to Water.");
        }

        // Test effectiveness with immunity multiplier
        [Test]
        public void CheckEffectiveness_ShouldReturnZero_ForImmunity()
        {
            // Act
            double effectiveness = Calculator.CheckEffectiveness(_electricAttack, _electrode);

            // Assert: Electric should deal no damage to Electric
            Assert.That(effectiveness, Is.EqualTo(0.0), "Electric should deal no damage to Electric.");
        }

        // Test if damage is applied correctly considering effectiveness
        [Test]
        public void InfringeDamage_ShouldApplyCorrectDamageWithEffectiveness()
        {
            // Set initial health of Bulbasaur
            _bulbasaur.Health = 100;

            // Act: Apply Fire attack to Bulbasaur (which has a Plant type)
            Calculator.InfringeDamage(_fireAttack, _bulbasaur);

            // Assert: Bulbasaur should now have 20 health after taking double damage
            Assert.That(_bulbasaur.Health, Is.EqualTo(20), "Fire attack should reduce Bulbasaur's health to 20.");
        }

        // Test if health does not go below zero
        [Test]
        public void InfringeDamage_ShouldNotReduceHealthBelowZero()
        {
            // Create a strong attack with excessive damage
            var strongAttack = new Attack("Fire Blast", 200, 1, "Fire");

            // Set initial health of Bulbasaur
            _bulbasaur.Health = 100;

            // Act: Apply a strong attack to Bulbasaur
            Calculator.InfringeDamage(strongAttack, _bulbasaur);

            // Assert: Health should not drop below zero
            Assert.That(_bulbasaur.Health, Is.EqualTo(0), "Health should not go below zero.");
        }

        // Test random player selection for the first turn
        [Test]
        public void FirstTurnSelection_ShouldReturnRandomFirstPlayer()
        {
            // Act: Select the first player randomly
            int result = Calculator.FirstTurnSelection();

            // Assert: The result should be either 1 or 2
            Assert.That(result, Is.InRange(1, 2), "First player should be either 1 or 2.");
        }

        // Test if player has active Pokémon
        [Test]
        public void HasActivePokemon_ShouldReturnTrueIfPlayerHasActivePokemon()
        {
            // Act: Check if player has active Pokémon
            bool result = Calculator.HasActivePokemon(Player.Player1);

            // Assert: Player 1 should have active Pokémon
            Assert.That(result, Is.True, "Player 1 should have active Pokémon.");
        }

        // Test if player has no active Pokémon
        [Test]
        public void HasActivePokemon_ShouldReturnFalseIfPlayerHasNoActivePokemon()
        {
            // Create an empty team for player 2
            var emptyTeam = new List<IPokemon>();

            // Initialize player 2 with no active Pokémon
            Player.InitializePlayer2("Misty", emptyTeam, null);

            // Act: Check if player has active Pokémon
            bool result = Calculator.HasActivePokemon(Player.Player2);

            // Assert: Player 2 should not have active Pokémon
            Assert.That(result, Is.False, "Player 2 should not have active Pokémon.");
        }
    }
}

