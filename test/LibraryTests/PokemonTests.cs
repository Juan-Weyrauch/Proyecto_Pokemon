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
                new Attack("Thunderbolt", 90, 1, "Electric"),
                new Attack("Quick Attack", 40, 0, "Normal")
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
        /// Verifies that <see cref="Pokemon.GetAttack"/> returns the correct attack based on the index.
        /// </summary>
        [Test]
        public void PokemonGetAttackReturnsCorrectAttack()
        {
            // Arrange
            var attacks = new List<IAttack>
            {
                new Attack("Thunderbolt", 90, 1, "Electric"),
                new Attack("Quick Attack", 40, 0, "Normal")
            };
            var pokemon = new Pokemon("Charmander", 40, "Fire", attacks);

            // Act
            var attack = pokemon.GetAttack(1);

            // Assert
            Assert.That(attack.Name, Is.EqualTo("Quick Attack"));
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
                new Attack("Water Gun", 40, 0, "Water")
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
        /// Verifies that setting properties on a <see cref="Pokemon"/> modifies the values correctly.
        /// </summary>
        [Test]
        public void PokemonSetPropertiesModifiesValuesCorrectly()
        {
            // Arrange
            var pokemon = new Pokemon("Bulbasaur", 50, "Grass", new List<IAttack>());

            // Act
            pokemon.Name = "Ivysaur";
            pokemon.Health = 80;
            pokemon.Defense = 60;
            pokemon.Type = "Grass/Poison";

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(pokemon.Name, Is.EqualTo("Ivysaur"));
                Assert.That(pokemon.Health, Is.EqualTo(80));
                Assert.That(pokemon.Defense, Is.EqualTo(60));
                Assert.That(pokemon.Type, Is.EqualTo("Grass/Poison"));
            });
        }
    }
}
