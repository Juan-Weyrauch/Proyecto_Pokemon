using Library.Game.Attacks;
using NUnit.Framework;

namespace LibraryTests
{
    /// <summary>
    /// Tests for the Attack class.
    /// </summary>
    [TestFixture]
    public class AttackTests
    {
        /// <summary>
        /// Verifica que la creación de un ataque asigna las propiedades correctamente.
        /// </summary>
        [Test]
        public void AttackCreationAssignsPropertiesCorrectly()
        {
            // Arrange
            string expectedName = "Thunderbolt";
            int expectedDamage = 90;
            int expectedSpecial = 1;
            string expectedType = "Electric";

            // Act
            IAttack attack = new Attack(expectedName, expectedDamage, expectedSpecial, expectedType);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(attack.Name, Is.EqualTo(expectedName));
                Assert.That(attack.Damage, Is.EqualTo(expectedDamage));
                Assert.That(attack.Special, Is.EqualTo(expectedSpecial));
                Assert.That(attack.Type, Is.EqualTo(expectedType));
            });
        }
        /// <summary>
        /// Verifies that setting properties on an <see cref="Attack"/> modifies the values correctly.
        /// </summary>
        [Test]
        public void AttackSetPropertiesModifiesValuesCorrectly()
        {
            // Arrange
            var attack = new Attack("Quick Attack", 40, 0, "Normal");

            // Act
            attack.Name = "Hyper Beam";
            attack.Damage = 150;
            attack.Special = 1;
            attack.Type = "Normal";

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(attack.Name, Is.EqualTo("Hyper Beam"));
                Assert.That(attack.Damage, Is.EqualTo(150));
                Assert.That(attack.Special, Is.EqualTo(1));
                Assert.That(attack.Type, Is.EqualTo("Normal"));
            });
        }
    }
}
