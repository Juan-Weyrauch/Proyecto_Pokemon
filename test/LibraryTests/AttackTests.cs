using Library.Game.Attacks;

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
            int expectedSpecial = 1;
            string expectedType = "Electric";

            // Act
            IAttack attack = new Attack(expectedName, expectedDamage, expectedSpecial, expectedType);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(attack.Name, Is.EqualTo(expectedName), "Attack name is not correctly assigned.");
                Assert.That(attack.Damage, Is.EqualTo(expectedDamage), "Attack damage is not correctly assigned.");
                Assert.That(attack.Special, Is.EqualTo(expectedSpecial), "Attack special property is not correctly assigned.");
                Assert.That(attack.Type, Is.EqualTo(expectedType), "Attack type is not correctly assigned.");
            });
        }

        [Test]
        public void Attack_SetProperties_ModifiesValuesCorrectly()
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
                Assert.That(attack.Name, Is.EqualTo("Hyper Beam"), "Attack name was not updated correctly.");
                Assert.That(attack.Damage, Is.EqualTo(150), "Attack damage was not updated correctly.");
                Assert.That(attack.Special, Is.EqualTo(1), "Attack special property was not updated correctly.");
                Assert.That(attack.Type, Is.EqualTo("Normal"), "Attack type was not updated correctly.");
            });
        }

        [Test]
        public void Attack_Getters_ShouldReturnCorrectValues()
        {
            // Arrange
            IAttack attack = new Attack("Thunderbolt", 90, 1, "Electric");

            // Act & Assert
            Assert.Multiple(() =>
            {
                Assert.That(attack.Name, Is.EqualTo("Thunderbolt"), "Getter for Name did not return correct value.");
                Assert.That(attack.Damage, Is.EqualTo(90), "Getter for Damage did not return correct value.");
                Assert.That(attack.Special, Is.EqualTo(1), "Getter for Special did not return correct value.");
                Assert.That(attack.Type, Is.EqualTo("Electric"), "Getter for Type did not return correct value.");
            });
        }

        [Test]
        public void Attack_Setters_ShouldChangeValuesCorrectly()
        {
            // Arrange
            IAttack attack = new Attack("Quick Attack", 40, 0, "Normal");

            // Act
            attack.Name = "Hyper Beam";
            attack.Damage = 150;
            attack.Special = 1;
            attack.Type = "Normal";

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(attack.Name, Is.EqualTo("Hyper Beam"), "Setter for Name did not update the value correctly.");
                Assert.That(attack.Damage, Is.EqualTo(150), "Setter for Damage did not update the value correctly.");
                Assert.That(attack.Special, Is.EqualTo(1), "Setter for Special did not update the value correctly.");
                Assert.That(attack.Type, Is.EqualTo("Normal"), "Setter for Type did not update the value correctly.");
            });
        }

        [Test]
        public void Attack_Creation_WithSpecialProperty_AssignsCorrectly()
        {
            // Arrange
            string expectedName = "Flamethrower";
            int expectedDamage = 95;
            int expectedSpecial = 2;  // Assume special attack
            string expectedType = "Fire";

            // Act
            IAttack attack = new Attack(expectedName, expectedDamage, expectedSpecial, expectedType);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(attack.Name, Is.EqualTo(expectedName), "Attack name is not correctly assigned.");
                Assert.That(attack.Damage, Is.EqualTo(expectedDamage), "Attack damage is not correctly assigned.");
                Assert.That(attack.Special, Is.EqualTo(expectedSpecial), "Attack special property is not correctly assigned.");
                Assert.That(attack.Type, Is.EqualTo(expectedType), "Attack type is not correctly assigned.");
            });
        }

        [Test]
        public void Attack_Creation_WithDefaultValues_AssignsDefaults()
        {
            // Arrange
            string expectedName = "Tackle";
            int expectedDamage = 40;
            int expectedSpecial = 0;  // Normal attack
            string expectedType = "Normal";

            // Act
            IAttack attack = new Attack(expectedName, expectedDamage, expectedSpecial, expectedType);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(attack.Name, Is.EqualTo(expectedName), "Attack name is not defaulted correctly.");
                Assert.That(attack.Damage, Is.EqualTo(expectedDamage), "Attack damage is not defaulted correctly.");
                Assert.That(attack.Special, Is.EqualTo(expectedSpecial), "Attack special property is not defaulted correctly.");
                Assert.That(attack.Type, Is.EqualTo(expectedType), "Attack type is not defaulted correctly.");
            });
        }
    }
}