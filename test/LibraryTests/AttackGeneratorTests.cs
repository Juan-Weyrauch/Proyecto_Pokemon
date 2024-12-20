﻿using Library.Game.Attacks;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.Tests
{
    [TestFixture]
    public class AttackGeneratorTests
    {
        [Test]
        public void GenerateRandomAttack_CorrectNumberOfAttacks_ReturnsFourAttacks()
        {
            // Arrange
            string type = "Fire";

            // Act
            List<IAttack> result = AttackGenerator.GenerateRandomAttack(type);

            // Assert
            Assert.That(result.Count, Is.EqualTo(4), "The number of generated attacks should be 4.");
        }

        [Test]
        public void GenerateRandomAttack_ThreeAttacksOfSpecifiedType_ReturnsCorrectType()
        {
            // Arrange
            string type = "Water";

            // Act
            List<IAttack> result = AttackGenerator.GenerateRandomAttack(type);

            // Assert
            int countOfType = result.Count(attack => attack.Type == type);
            Assert.That(countOfType, Is.EqualTo(3), "There should be exactly 3 attacks of the specified type.");
        }

        [Test]
        public void GenerateRandomAttack_OneAttackOfDifferentType_ReturnsDifferentType()
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

        [Test]
        public void GenerateRandomAttack_InvalidType_ThrowsArgumentException()
        {
            // Arrange
            string invalidType = "InvalidType";

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => AttackGenerator.GenerateRandomAttack(invalidType));
            Assert.That(ex.Message, Is.EqualTo("Tipo de Pokémon no reconocido."));
        }

        [Test]
        public void GenerateRandomAttack_GeneratesValidAttacks_ReturnsNonEmptyAttacks()
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
