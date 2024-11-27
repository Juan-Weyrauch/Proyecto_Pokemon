using NUnit.Framework;
using System.Collections.Generic;
using Library.Game.Attacks;
using Library.Game.Items;
using Library.Game.Players;
using Library.Game.Pokemons;

namespace Library.Tests
{
    // Mock implementations for testing
    public class MockPokemon : IPokemon
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int InitialHealth { get; set; }
        public int Defense { get; set; }
        public string Type { get; set; }
        public List<IAttack> AtackList { get; }
        public int SleepTurns { get; }
        public SpecialEffect State { get; set; }
        public IAttack GetAttack(int index)
        {
            throw new NotImplementedException();
        }

        public IPokemon Clone()
        {
            throw new NotImplementedException();
        }

        public void DecreaseHealth(int damage)
        {
            throw new NotImplementedException();
        }

        public void ProcessTurnEffects()
        {
            throw new NotImplementedException();
        }

        public bool StatusReset { get; private set; }

        public void ResetStatus()
        {
            StatusReset = true;
        }

        public void ApplyStatusEffect(SpecialEffect effect)
        {
            throw new NotImplementedException();
        }
    }

    public class MockPlayer : IPlayer
    {
        public string Name { get; }
        public List<IPokemon> Pokemons { get; } = new List<IPokemon>();
        public List<IPokemon> Cementerio { get; } = new List<IPokemon>();
        public List<List<Item>> Items { get; }
        public IPokemon SelectedPokemon { get; }
        public int Turn { get; }
        public void SwitchPokemon(int pokemonChoice)
        {
            throw new NotImplementedException();
        }

        public void CarryToCementerio()
        {
            throw new NotImplementedException();
        }

        public void CarryToTeam(IPokemon pokemon)
        {
            Pokemons.Add(pokemon);
            Cementerio.Remove(pokemon);
        }

        public Item GetItem(int itemListIndex)
        {
            throw new NotImplementedException();
        }

        public void RemoveItem(int itemListIndex)
        {
            throw new NotImplementedException();
        }

        public int ReturnItemCount()
        {
            throw new NotImplementedException();
        }

        public int ReturnPokemonsCount()
        {
            throw new NotImplementedException();
        }
    }

    [TestFixture]
    public class ItemsTests
    {
        [Test]
        public void SuperPotion_ShouldHaveCorrectProperties()
        {
            // Arrange
            var superPotion = new SuperPotion();

            // Assert
            Assert.That(superPotion.Name, Is.EqualTo("Super Potion"));
        }

        [Test]
        public void SuperPotion_Use_ShouldHealPokemon()
        {
            // Arrange
            var superPotion = new SuperPotion();
            var mockPlayer = new MockPlayer();
            var mockPokemon = new MockPokemon
            {
                Health = 50,
                InitialHealth = 100
            };
            mockPlayer.Pokemons.Add(mockPokemon);

            // Act
            superPotion.Use(mockPlayer, 0);

            // Assert
            Assert.That(mockPokemon.Health, Is.EqualTo(100)); // Health capped at 100
        }

        [Test]
        public void SuperPotion_Use_ShouldNotHealBeyondMaxHealth()
        {
            // Arrange
            var superPotion = new SuperPotion();
            var mockPlayer = new MockPlayer();
            var mockPokemon = new MockPokemon
            {
                Health = 95,
                InitialHealth = 100
            };
            mockPlayer.Pokemons.Add(mockPokemon);

            // Act
            superPotion.Use(mockPlayer, 0);

            // Assert
            Assert.That(mockPokemon.Health, Is.EqualTo(100)); // Capped at max health
        }

        [Test]
        public void SuperPotion_Use_ShouldNotThrowExceptionWithNullPlayer()
        {
            // Arrange
            var superPotion = new SuperPotion();

            // Act & Assert
            Assert.DoesNotThrow(() => superPotion.Use(null, 0));
        }

        [Test]
        public void RevivePotion_ShouldHaveCorrectProperties()
        {
            // Arrange
            var revivePotion = new RevivePotion();

            // Assert
            Assert.That(revivePotion.Name, Is.EqualTo("Revive"));
        }

        [Test]
        public void RevivePotion_Use_ShouldReviveFaintedPokemon()
        {
            // Arrange
            var revivePotion = new RevivePotion();
            var mockPlayer = new MockPlayer();
            var mockPokemon = new MockPokemon
            {
                Health = 0,
                InitialHealth = 100
            };
            mockPlayer.Cementerio.Add(mockPokemon);

            // Act
            revivePotion.Use(mockPlayer, 0);

            // Assert
            Assert.That(mockPokemon.Health, Is.EqualTo(50)); // Half of InitialHealth
            Assert.That(mockPlayer.Pokemons, Contains.Item(mockPokemon));
            Assert.That(mockPlayer.Cementerio, Does.Not.Contain(mockPokemon));
        }

        [Test]
        public void RevivePotion_Use_ShouldNotReviveHealthyPokemon()
        {
            // Arrange
            var revivePotion = new RevivePotion();
            var mockPlayer = new MockPlayer();
            var mockPokemon = new MockPokemon
            {
                Health = 10,
                InitialHealth = 100
            };
            mockPlayer.Cementerio.Add(mockPokemon);

            // Act
            revivePotion.Use(mockPlayer, 0);

            // Assert
            Assert.That(mockPokemon.Health, Is.EqualTo(10)); // Unchanged health
            Assert.That(mockPlayer.Pokemons, Does.Not.Contain(mockPokemon));
            Assert.That(mockPlayer.Cementerio, Contains.Item(mockPokemon));
        }

        [Test]
        public void RevivePotion_Use_ShouldHandleInvalidIndexGracefully()
        {
            // Arrange
            var revivePotion = new RevivePotion();
            var mockPlayer = new MockPlayer();

            // Act & Assert
            Assert.DoesNotThrow(() => revivePotion.Use(mockPlayer, 0)); // No Pokémon at index 0
        }

        [Test]
        public void RevivePotion_Use_ShouldNotThrowExceptionWithNullPlayer()
        {
            // Arrange
            var revivePotion = new RevivePotion();

            // Act & Assert
            Assert.DoesNotThrow(() => revivePotion.Use(null, 0));
        }

        [Test]
        public void SuperPotion_Use_ShouldHandleInvalidIndexGracefully()
        {
            // Arrange
            var superPotion = new SuperPotion();
            var mockPlayer = new MockPlayer();

            // Act & Assert
            Assert.DoesNotThrow(() => superPotion.Use(mockPlayer, 0)); // No Pokémon at index 0
        }
    }
}