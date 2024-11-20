/*
using System.Collections.Generic;
using Library.Interfaces;
using Library.StaticClasses;
using NUnit.Framework;

namespace Library.Tests
{
    [TestFixture]
    public class CalculatorTests
    {
        // Fake implementation of IPokemon for testing
        private class FakePokemon : IPokemon
        {
            public int Health { get; set; }
            public string Name { get; set; }
            public string Type { get; set; }
            public int Defense { get; set; }
        }

        // Fake implementation of IAttack for testing
        private class FakeAttack : IAttack
        {
            public string Type { get; set; }
            public int Damage { get; set; }
        }

        [Test]
        public void GetEffectivenessMultiplier_Weakness_Returns2x()
        {
            // Arrange
            string attackType = "Electric";
            string pokemonType = "Water";

            // Act
            double result = Calculator.GetEffectivenessMultiplier(attackType, pokemonType);

            // Assert
            Assert.That(result, Is.EqualTo(2.0));
        }

        [Test]
        public void GetEffectivenessMultiplier_Resistance_Returns0Point5x()
        {
            // Arrange
            string attackType = "Fire";
            string pokemonType = "Water";

            // Act
            double result = Calculator.GetEffectivenessMultiplier(attackType, pokemonType);

            // Assert
            Assert.That(result, Is.EqualTo(0.5));
        }

        [Test]
        public void GetEffectivenessMultiplier_Immunity_Returns0x()
        {
            // Arrange
            string attackType = "Ghost";
            string pokemonType = "Normal";

            // Act
            double result = Calculator.GetEffectivenessMultiplier(attackType, pokemonType);

            // Assert
            Assert.That(result, Is.EqualTo(0.0));
        }

        [Test]
        public void GetEffectivenessMultiplier_NoEffectiveness_Returns1x()
        {
            // Arrange
            string attackType = "Fire";
            string pokemonType = "Plant";

            // Act
            double result = Calculator.GetEffectivenessMultiplier(attackType, pokemonType);

            // Assert
            Assert.That(result, Is.EqualTo(1.0));
        }

        [Test]
        public void CheckEffectiveness_ValidAttackAndPokemon_ReturnsCorrectMultiplier()
        {
            // Arrange
            var attack = new FakeAttack { Type = "Electric", Damage = 50 };
            var pokemon = new FakePokemon { Type = "Water" };

            // Act
            double result = Calculator.CheckEffectiveness(attack, pokemon);

            // Assert
            Assert.That(result, Is.EqualTo(2.0));
        }

        [Test]
        public void InfringeDamage_Weakness_DamageAppliedCorrectly()
        {
            // Arrange
            var attack = new FakeAttack { Type = "Electric", Damage = 50 };
            var rival = new FakePokemon { Health = 100, Defense = 10, Type = "Water" };

            // Act
            Calculator.InfringeDamage(attack, rival);

            // Assert
            Assert.That(rival.Health, Is.EqualTo(10)); // (50*2 - 10) = 90 damage, 100 - 90 = 10 health
        }

        [Test]
        public void InfringeDamage_Resistance_DamageAppliedCorrectly()
        {
            // Arrange
            var attack = new FakeAttack { Type = "Fire", Damage = 50 };
            var rival = new FakePokemon { Health = 100, Defense = 10, Type = "Water" };

            // Act
            Calculator.InfringeDamage(attack, rival);

            // Assert
            Assert.That(rival.Health, Is.EqualTo(85)); // (50*0.5 - 10) = 15 damage, 100 - 15 = 85 health
        }

        [Test]
        public void InfringeDamage_Immunity_NoDamageApplied()
        {
            // Arrange
            var attack = new FakeAttack { Type = "Ghost", Damage = 50 };
            var rival = new FakePokemon { Health = 100, Defense = 10, Type = "Normal" };

            // Act
            Calculator.InfringeDamage(attack, rival);

            // Assert
            Assert.That(rival.Health, Is.EqualTo(100)); // No damage applied
        }
    }
}
*/