using Library.Game.Attacks;
using Library.Game.Pokemons;

using Library.Game.Attacks;
using NUnit.Framework;

namespace Library.Tests
{
    /// <summary>
    /// Tests for the Attack class.
    /// </summary>
    [TestFixture]
    public class AttackTests
    {
        [Test]
        public void Attack_Creation_AssignsPropertiesCorrectly()
        {
            // Arrange
            string expectedName = "Thunderbolt";
            int expectedDamage = 90;
            SpecialEffect expectedSpecial = SpecialEffect.Paralyze;
            string expectedType = "Electric";
            int expectedAccuracy = 95;

            // Act
            var attack = new Attack(expectedName, expectedDamage, expectedSpecial, expectedType, expectedAccuracy);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(attack.Name, Is.EqualTo(expectedName));
                Assert.That(attack.Damage, Is.EqualTo(expectedDamage));
                Assert.That(attack.Special, Is.EqualTo(expectedSpecial));
                Assert.That(attack.Type, Is.EqualTo(expectedType));
                Assert.That(attack.Accuracy, Is.EqualTo(expectedAccuracy));
            });
        }

        [Test]
        public void Attack_SetProperties_ModifiesValuesCorrectly()
        {
            // Arrange
            var attack = new Attack("Quick Attack", 40, SpecialEffect.None, "Normal", 100);

            // Act
            attack.Name = "Hyper Beam";
            attack.Damage = 150;
            attack.Special = SpecialEffect.Burn;
            attack.Type = "Normal";
            attack.Accuracy = 75;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(attack.Name, Is.EqualTo("Hyper Beam"));
                Assert.That(attack.Damage, Is.EqualTo(150));
                Assert.That(attack.Special, Is.EqualTo(SpecialEffect.Burn));
                Assert.That(attack.Type, Is.EqualTo("Normal"));
                Assert.That(attack.Accuracy, Is.EqualTo(75));
            });
        }

        [Test]
        public void Attack_IsCritical_ReturnsTrue10PercentOfTheTime()
        {
            // Arrange
            var attack = new Attack("Quick Attack", 40, SpecialEffect.None, "Normal", 100);
            int criticalCount = 0;
            const int totalTrials = 1000;

            // Act
            for (int i = 0; i < totalTrials; i++)
            {
                if (attack.IsCritical())
                {
                    criticalCount++;
                }
            }

            // Calculate critical rate
            double criticalRate = (double)criticalCount / totalTrials;

            // Assert
            Assert.That(criticalRate, Is.InRange(0.086, 0.11), "Critical rate is not within the expected range.");
        }
    }
}