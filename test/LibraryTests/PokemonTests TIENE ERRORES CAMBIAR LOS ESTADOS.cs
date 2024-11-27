using Library.Game.Pokemons;
using Library.Game.Attacks;
using NUnit.Framework;

namespace LibraryTests
{
    /// <summary>
    /// Tests for the <see cref="Pokemon"/> class.
    /// </summary>
    [TestFixture]
    public class PokemonTests
    {
        /// <summary>
        /// Verifies that the <see cref="Pokemon"/> constructor assigns properties correctly.
        /// </summary>
        [Test]
        public void PokemonCreationAssignsPropertiesCorrectly()
        {
            // Arrange
            string expectedName = "Pikachu";
            int expectedDefense = 50;
            string expectedType = "Electric";
            var attacks = new List<IAttack>
            {
                new Attack("Thunderbolt", 90, SpecialEffect.None, "Electric", 100),
                new Attack("Quick Attack", 40, SpecialEffect.None, "Normal", 95)
            };

            // Act
            var pokemon = new Pokemon(expectedName, expectedDefense, expectedType, attacks);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(pokemon.Name, Is.EqualTo(expectedName));
                Assert.That(pokemon.Defense, Is.EqualTo(expectedDefense));
                Assert.That(pokemon.Type, Is.EqualTo(expectedType));
                Assert.That(pokemon.AtackList, Is.EqualTo(attacks));
                Assert.That(pokemon.InitialHealth, Is.EqualTo(100));
                Assert.That(pokemon.Health, Is.EqualTo(100));
            });
        }

        /// <summary>
        /// Tests for the <see cref="Pokemon"/> class.
        /// </summary>
        [TestFixture]
        public class _PokemonTests
        {
            /// <summary>
            /// Verifies that <see cref="Pokemon.GetAttack"/> returns the correct attack based on the index.
            /// </summary>
            [Test]
            public void PokemonGetAttackReturnsCorrectAttack()
            {
                // Arrange
                var attacks = new List<IAttack>
                {
                    new Attack("Thunderbolt", 90, SpecialEffect.None, "Electric", 100),
                    new Attack("Quick Attack", 40, SpecialEffect.None, "Normal", 95)
                };
                var pokemon = new Pokemon("Charmander", 40, "Fire", attacks);

                // Act
                var attack = pokemon.GetAttack(1);

                // Assert
                Assert.That(attack.Name, Is.EqualTo("Quick Attack"));
                Assert.That(attack.Accuracy, Is.EqualTo(95));
            }

            /// <summary>
            /// Verifies that <see cref="Pokemon.Clone"/> creates an identical copy of the Pokémon.
            /// </summary>
            [Test]
            public void PokemonCloneCreatesIdenticalPokemon()
            {
                // Arrange
                var attacks = new List<IAttack>
                {
                    new Attack("Water Gun", 40, SpecialEffect.None, "Water", 100)
                };
                var originalPokemon = new Pokemon("Squirtle", 45, "Water", attacks);

                // Act
                var clonedPokemon = originalPokemon.Clone();

                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(clonedPokemon.Name, Is.EqualTo(originalPokemon.Name));
                    Assert.That(clonedPokemon.Defense, Is.EqualTo(originalPokemon.Defense));
                    Assert.That(clonedPokemon.Type, Is.EqualTo(originalPokemon.Type));
                    Assert.That(clonedPokemon.AtackList, Is.EqualTo(originalPokemon.AtackList));
                    Assert.That(clonedPokemon.InitialHealth, Is.EqualTo(originalPokemon.InitialHealth));
                    Assert.That(clonedPokemon.Health, Is.EqualTo(originalPokemon.Health));
                });
            }

            /// <summary>
            /// Verifies that <see cref="Pokemon.DecreaseHealth"/> decreases health correctly.
            /// </summary>
            [Test]
            public void DecreaseHealthReducesHealthCorrectly()
            {
                // Arrange
                var pokemon = new Pokemon("Bulbasaur", 50, "Grass", new List<IAttack>());
                pokemon.Health = 80;

                // Act
                pokemon.DecreaseHealth(30);

                // Assert
                Assert.That(pokemon.Health, Is.EqualTo(50), "Health should decrease by 30.");
            }

            /// <summary>
            /// Verifies that <see cref="Pokemon.DecreaseHealth"/> does not allow negative health.
            /// </summary>
            [Test]
            public void DecreaseHealthDoesNotAllowNegativeHealth()
            {
                // Arrange
                var pokemon = new Pokemon("Charmander", 40, "Fire", new List<IAttack>());
                pokemon.Health = 20;

                // Act
                pokemon.DecreaseHealth(50);

                // Assert
                Assert.That(pokemon.Health, Is.EqualTo(0), "Health should not go below 0.");
            }

            /// <summary>
            /// Verifies that <see cref="Pokemon.ApplyStatusEffect"/> sets the state correctly.
            /// </summary>
            [Test]
            public void ApplyStatusEffectSetsStateCorrectly()
            {
                // Arrange
                var pokemon = new Pokemon("Squirtle", 30, "Water", new List<IAttack>());

                // Act
                pokemon.ApplyStatusEffect(SpecialEffect.Sleep);

                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(pokemon.State, Is.EqualTo(SpecialEffect.Sleep), "State should be set to Sleep.");
                    Assert.That(pokemon.SleepTurns, Is.InRange(1, 5), "SleepTurns should be between 1 and 5.");
                });
            }

            /// <summary>
            /// Verifies that <see cref="Pokemon.ApplyStatusEffect"/> does not overwrite existing state.
            /// </summary>
            [Test]
            public void ApplyStatusEffectDoesNotOverwriteExistingState()
            {
                // Arrange
                var pokemon = new Pokemon("Snorlax", 50, "Normal", new List<IAttack>());
                pokemon.ApplyStatusEffect(SpecialEffect.Poison);

                // Act
                pokemon.ApplyStatusEffect(SpecialEffect.Sleep);

                // Assert
                Assert.That(pokemon.State, Is.EqualTo(SpecialEffect.Poison),
                    "State should remain Poison, not change to Sleep.");
            }

            /// <summary>
            /// Verifies that <see cref="Pokemon.ProcessTurnEffects"/> decreases health for Poison effect.
            /// </summary>
            [Test]
            public void ProcessTurnEffectsHandlesPoisonCorrectly()
            {
                // Arrange
                var pokemon = new Pokemon("Zubat", 30, "Poison", new List<IAttack>());
                pokemon.ApplyStatusEffect(SpecialEffect.Poison);
                pokemon.Health = 100;

                // Act
                pokemon.ProcessTurnEffects();

                // Assert
                Assert.That(pokemon.Health, Is.EqualTo(95), "Poison should reduce health by 5% of InitialHealth.");
            }

            /// <summary>
            /// Verifies that <see cref="Pokemon.ProcessTurnEffects"/> decreases health for Burn effect.
            /// </summary>
            [Test]
            public void ProcessTurnEffectsHandlesBurnCorrectly()
            {
                // Arrange
                var pokemon = new Pokemon("Charmander", 30, "Fire", new List<IAttack>());
                pokemon.ApplyStatusEffect(SpecialEffect.Burn);
                pokemon.Health = 100;

                // Act
                pokemon.ProcessTurnEffects();

                // Assert
                Assert.That(pokemon.Health, Is.EqualTo(90), "Burn should reduce health by 10% of InitialHealth.");
            }

            /// <summary>
            /// Verifies that <see cref="Pokemon.ResetStatus"/> resets all effects and sleep turns.
            /// </summary>
            [Test]
            public void ResetStatusClearsAllEffectsAndSleepTurns()
            {
                // Arrange
                var pokemon = new Pokemon("Mewtwo", 100, "Psychic", new List<IAttack>());
                pokemon.ApplyStatusEffect(SpecialEffect.Sleep);

                // Act
                pokemon.ResetStatus();

                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(pokemon.State, Is.EqualTo(SpecialEffect.None), "State should reset to None.");
                    Assert.That(pokemon.SleepTurns, Is.EqualTo(0), "SleepTurns should reset to 0.");
                });
            }
        }
    }
}
