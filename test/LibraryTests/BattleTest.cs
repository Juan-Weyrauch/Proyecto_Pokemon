using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System;
using Library.Game.Attacks;
using Library.Game.Items;
using Library.Game.Players;
using Library.Game.Pokemons;
using Library.Facade;

namespace Library.Tests
{
    [TestFixture]
    public class BattleTests
    {
        private StringWriter _stringWriter;
        private TextWriter _originalOutput;

        [SetUp]
        public void Setup()
        {
            // Redirect Console output to prevent "handle is invalid" error
            _originalOutput = Console.Out;
            _stringWriter = new StringWriter();
            Console.SetOut(_stringWriter);

            // Ensure Player singleton instances are reset before each test
            ResetPlayerSingletons();
        }

        [TearDown]
        public void TearDown()
        {
            // Reset the Player singletons after each test
            ResetPlayerSingletons();

            // Restore original Console output
            Console.SetOut(_originalOutput);
        }

        private void ResetPlayerSingletons()
        {
            // Reset Player1 and Player2 singletons using reflection to set them to null
            typeof(Player).GetField("_player1", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic)?.SetValue(null, null);
            typeof(Player).GetField("_player2", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic)?.SetValue(null, null);
        }

        [TestCase("Charmander", 10, "Fire", "Squirtle", 12, "Water")]
        [TestCase("Pikachu", 15, "Electric", "Bulbasaur", 12, "Grass")]
        public void UseItem_UsesCorrectItemOnPokemon(string name1, int hp1, string type1, string name2, int hp2, string type2)
        {
            // Arrange: Create Pokémon instances
            var pokemon1 = new Pokemon(name1, hp1, type1, new List<IAttack>());
            var pokemon2 = new Pokemon(name2, hp2, type2, new List<IAttack>());

            // Initialize singleton players with the test Pokémon
            Player.InitializePlayer1("Ash", new List<IPokemon> { pokemon1 }, pokemon1);
            Player.InitializePlayer2("Misty", new List<IPokemon> { pokemon2 }, pokemon2);

            var player1 = Player.Player1;
            var player2 = Player.Player2;

            // Arrange: Add item to player's inventory and simulate damage
            var potion = new SuperPotion();
            player1.Items[2].Add(potion); // Add potion to player's inventory
            pokemon1.DecreaseHealth(50); // Simulate damage

            // Act: Use potion on Pokémon
            potion.Use(player1, 0); // Use potion on the first Pokémon

            // Assert: Verify that Pokémon's health is restored
            Assert.That(pokemon1.Health, Is.EqualTo(100)); // Health should be restored
            Assert.That(_stringWriter.ToString(), Does.Not.Contain("Invalid handle")); // Console output should not contain errors
        }

        [TestCase("Charmander", 10, "Fire", "Squirtle", 12, "Water")]
        [TestCase("Pikachu", 15, "Electric", "Bulbasaur", 12, "Grass")]
        public void ForceSwitchPokemon_SwitchesPokemonOnDefeat(string name1, int hp1, string type1, string name2, int hp2, string type2)
        {
            // Arrange: Create Pokémon instances
            var pokemon1 = new Pokemon(name1, hp1, type1, new List<IAttack>());
            var pokemon2 = new Pokemon(name2, hp2, type2, new List<IAttack>());

            // Initialize singleton players with the test Pokémon
            Player.InitializePlayer1("Ash", new List<IPokemon> { pokemon1 }, pokemon1);
            Player.InitializePlayer2("Misty", new List<IPokemon> { pokemon2 }, pokemon2);

            var player1 = Player.Player1;
            var player2 = Player.Player2;

            // Arrange: Simulate fainted Pokémon and add new Pokémon to the player
            pokemon1.DecreaseHealth(100); // Simulate fainted Pokémon
            var newPokemon = new Pokemon("Bulbasaur", 12, "Grass", new List<IAttack>());
            player1.Pokemons.Add(newPokemon);

            // Act: Force switch to the second Pokémon
            player1.SwitchPokemon(1); // Force switch to the second Pokémon

            // Assert: Verify that the selected Pokémon has been switched
            Assert.That(player1.SelectedPokemon, Is.Not.SameAs(pokemon1)); // Ensure switch occurred
            Assert.That(player1.SelectedPokemon, Is.SameAs(newPokemon)); // Verify new Pokémon
        }

        [TestCase("Charmander", 10, "Fire", "Squirtle", 12, "Water")]
        [TestCase("Pikachu", 15, "Electric", "Bulbasaur", 12, "Grass")]
        public void VoluntarySwitchPokemon_SuccessfulSwitch(string name1, int hp1, string type1, string name2, int hp2, string type2)
        {
            // Arrange: Create Pokémon instances
            var pokemon1 = new Pokemon(name1, hp1, type1, new List<IAttack>());
            var pokemon2 = new Pokemon(name2, hp2, type2, new List<IAttack>());

            // Initialize singleton players with the test Pokémon
            Player.InitializePlayer1("Ash", new List<IPokemon> { pokemon1 }, pokemon1);
            Player.InitializePlayer2("Misty", new List<IPokemon> { pokemon2 }, pokemon2);

            var player1 = Player.Player1;
            var player2 = Player.Player2;

            // Arrange: Add a new Pokémon to the player
            var newPokemon = new Pokemon("Pikachu", 15, "Electric", new List<IAttack>());
            player1.Pokemons.Add(newPokemon);

            // Act: Switch to the second Pokémon
            player1.SwitchPokemon(1); // Switch to the second Pokémon

            // Assert: Verify that the switch happened
            Assert.That(player1.SelectedPokemon, Is.SameAs(newPokemon)); // Verify switch
        }

        [TestCase("Charmander", 10, "Fire", "Squirtle", 12, "Water")]
        [TestCase("Pikachu", 15, "Electric", "Bulbasaur", 12, "Grass")]
        public void AttackHitsWhenAccuracyIsHighEnough(string name1, int hp1, string type1, string name2, int hp2, string type2)
        {
            // Arrange: Create Pokémon instances
            var pokemon1 = new Pokemon(name1, hp1, type1, new List<IAttack>());
            var pokemon2 = new Pokemon(name2, hp2, type2, new List<IAttack>());

            // Initialize singleton players with the test Pokémon
            Player.InitializePlayer1("Ash", new List<IPokemon> { pokemon1 }, pokemon1);
            Player.InitializePlayer2("Misty", new List<IPokemon> { pokemon2 }, pokemon2);

            var player1 = Player.Player1;
            var player2 = Player.Player2;

            // Arrange: Add attack to Pokémon
            var attack = new Attack("Flamethrower", 40, SpecialEffect.None, "Fire", 100);
            pokemon1.AtackList.Add(attack);

            // Act: Perform attack
            var selectedAttack = pokemon1.GetAttack(0);
            pokemon2.DecreaseHealth(selectedAttack.Damage);

            // Assert: Verify that the attack dealt damage
            Assert.That(pokemon2.Health, Is.LessThan(100)); // Health should decrease
        }

        [TestCase("Charmander", 10, "Fire", "Squirtle", 12, "Water")]
        [TestCase("Pikachu", 15, "Electric", "Bulbasaur", 12, "Grass")]
        public void ProcessTurnEffectsForPlayersProcessesSelectedPokemonEffects(string name1, int hp1, string type1, string name2, int hp2, string type2)
        {
            // Arrange: Create Pokémon instances
            var pokemon1 = new Pokemon(name1, hp1, type1, new List<IAttack>());
            var pokemon2 = new Pokemon(name2, hp2, type2, new List<IAttack>());

            // Initialize singleton players with the test Pokémon
            Player.InitializePlayer1("Ash", new List<IPokemon> { pokemon1 }, pokemon1);
            Player.InitializePlayer2("Misty", new List<IPokemon> { pokemon2 }, pokemon2);

            var player1 = Player.Player1;
            var player2 = Player.Player2;

            // Act: Apply turn effects to Pokémon
            pokemon1.ProcessTurnEffects();

            // Assert: Verify that Pokémon’s health did not change (since no status effects were applied)
            Assert.That(pokemon1.Health, Is.EqualTo(100)); // Health should remain unchanged
        }
    }
}
