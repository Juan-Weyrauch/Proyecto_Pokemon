/*
using Library.Game.Attacks;
using NUnit.Framework;

namespace LibraryTests
{
    /// <summary>
    /// Test class for attack generation.
    /// </summary>
    [TestFixture]
    public class AttackGeneratorTests
    {
        /// <summary>
        /// Tests that the GenerateRandomAttack method returns exactly four attacks.
        /// </summary>
        [Test]
        public void GenerateRandomAttackCorrectNumberOfAttacksReturnsFourAttacks()
        {
            // Arrange
            string type = "Fire";

            // Act
            List<IAttack> result = AttackGenerator.GenerateRandomAttack(type);

            // Assert
            Assert.That(result.Count, Is.EqualTo(4), "The number of generated attacks should be 4.");
        }
        /// <summary>
        /// Prueba que verifica si el generador de ataques genera exactamente tres ataques del tipo especificado.
        /// </summary>
        [Test]
        public void GenerateRandomAttackThreeAttacksOfSpecifiedTypeReturnsCorrectType()
        {
            // Arrange
            string type = "Water";

            // Act
            List<IAttack> result = AttackGenerator.GenerateRandomAttack(type);

            // Assert
            int countOfType = result.Count(attack => attack.Type == type);
            Assert.That(countOfType, Is.EqualTo(3), "There should be exactly 3 attacks of the specified type.");
        }
        /// <summary>
        /// Tests that GenerateRandomAttack throws an ArgumentException when provided with an invalid type.
        /// </summary>
        [Test]
        public void GenerateRandomAttackOneAttackOfDifferentTypeReturnsDifferentType()
        {
            // Arrange
            string type = "Electric";

            // Act
            List<IAttack> result = AttackGenerator.GenerateRandomAttack(type);

            // Assert
            IAttack randomAttack = result.Last();
            // Verificamos que el tipo del cuarto ataque no sea el mismo que el del Pokémon
            Assert.That(randomAttack.Type, Is.Not.EqualTo(type), "The fourth attack should be of a different type.");
        }
        /// <summary>
        /// Tests that GenerateRandomAttack throws an ArgumentException when provided with an invalid type.
        /// </summary>
        [Test]
        public void GenerateRandomAttackInvalidTypeThrowsArgumentException()
        {
            // Arrange
            string invalidType = "InvalidType";

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => AttackGenerator.GenerateRandomAttack(invalidType));
            Assert.That(ex.Message, Is.EqualTo("Tipo de Pokémon no reconocido."));
        }
        /// <summary>
        /// Tests that GenerateRandomAttack generates valid attacks that are non-empty.
        /// </summary>
        [Test]
        public void GenerateRandomAttackGeneratesValidAttacksReturnsNonEmptyAttacks()
        {
            // Arrange
            string type = "Ghost";

            // Act
            List<IAttack> result = AttackGenerator.GenerateRandomAttack(type);

            // Assert
            foreach (var attack in result)
            {
                // Verificamos que el nombre del ataque no sea vacío ni nulo
                Assert.That(string.IsNullOrEmpty(attack.Name), Is.False, "Attack name should not be null or empty.");
            }
        }
    }
}
*/