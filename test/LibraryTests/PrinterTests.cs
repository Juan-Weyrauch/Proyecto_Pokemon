using Library.Game.Attacks;
using Library.Game.Players;
using Library.Game.Pokemons;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using Library.Game.Items;
using Library.Game.Utilities;

namespace Library.Tests
{
    [TestFixture]
    public class PrinterTests
    {
        private StringWriter _stringWriter;
        private TextWriter _originalOutput;

        [SetUp]
        public void Setup()
        {
            // Redirect console output to StringWriter
            _originalOutput = Console.Out;
            _stringWriter = new StringWriter();
            Console.SetOut(_stringWriter);
        }

        [TearDown]
        public void TearDown()
        {
            // Restore original console output
            Console.SetOut(_originalOutput);
            _stringWriter.Dispose();
        }

        // Test double for Attack
        // Test double for Attack
        private class TestAttack : IAttack
        {
            public string Name { get; set; } = "TestAttack";
            public int Damage { get; set; } = 40;
            public int Special { get; set; } = 0;
            public string Type { get; set; } = "Normal";

            public TestAttack()
            {
                Name = "TestAttack";
                Damage = 40;
                Special = 0;
                Type = "Normal";
            }

            public override string ToString()
            {
                return Name;
            }
        }
        // Test double for Player
        private class TestPlayer : IPlayer
        {
            public string Name { get; set; }
            public List<IPokemon> Pokemons { get; }
            public List<IPokemon> Cementerio { get; }
            public List<IPotions> Potions { get; }
            public IPokemon SelectedPokemon { get; set; }
            public int Turn { get; }
            public void UseItem(int itemChoice)
            {
                throw new NotImplementedException();
            }

            public void SwitchPokemon(int pokemonChoice)
            {
                throw new NotImplementedException();
            }

            public void CarryToCementerio()
            {
                throw new NotImplementedException();
            }

            public List<IPokemon> Inventory { get; set; }

            public TestPlayer(string name = "TestPlayer")
            {
                Name = name;
                var attacks = new List<IAttack> { new TestAttack() };
                SelectedPokemon = new Pokemon(name + "Pokemon", 10, "Normal", attacks);
                Inventory = new List<IPokemon> { SelectedPokemon };
            }
        }

        [Test]
        public void AskForPokemon_ShouldPrintCorrectMessage()
        {
            // Act
            Printer.AskForPokemon(1, "Ash");

            // Assert
            string output = _stringWriter.ToString();
            Assert.That(output, Does.Contain("Ash! Pick your Pokemon N°1: "), 
                "Expected message to include player name and Pokemon number.");
        }

        [Test]
        public void ShowInventory_WithValidInventory_ShouldDisplayPokemonDetails()
        {
            // Arrange
            var attacks = new List<IAttack> { new TestAttack() };
            var inventory = new List<IPokemon>
            {
                new Pokemon("Pikachu", 20, "Electric", attacks),
                new Pokemon("Charmander", 30, "Fire", attacks)
            };

            // Act
            Printer.ShowInventory(inventory);

            // Assert
            string output = _stringWriter.ToString();
            Assert.That(output, Does.Contain("Your Team"), "Expected inventory header.");
            Assert.That(output, Does.Contain("Name: Pikachu"), "Expected first Pokemon name.");
            Assert.That(output, Does.Contain("Name: Charmander"), "Expected second Pokemon name.");
        }

        [Test]
        public void ShowAttacks_ShouldDisplayAttackDetails()
        {
            // Arrange
            var attacks = new List<IAttack> { new TestAttack() };
            var attacker = new Pokemon("Pikachu", 20, "Electric", attacks);
            var receiver = new Pokemon("Opponent", 15, "Normal", attacks);

            // Act
            Printer.ShowAttacks(attacker, receiver);

            // Assert
            string output = _stringWriter.ToString();
            Assert.That(output, Does.Contain("Attacks of Pikachu"), "Expected header with Pokemon name.");
            Assert.That(output, Does.Contain("Name: TestAttack"), "Expected attack name.");
            Assert.That(output, Does.Contain("Damage: 40"), "Expected attack damage.");
            Assert.That(output, Does.Contain("Type: Normal"), "Expected attack type.");
        }

        [Test]
        public void ShowTurnInfo_ShouldDisplayPlayerAndPokemonInfo()
        {
            // Arrange
            var player = new TestPlayer("Ash");
            var pokemon = player.SelectedPokemon;

            // Act
            Printer.ShowTurnInfo(player, pokemon);

            // Assert
            string output = _stringWriter.ToString();
            Assert.That(output, Does.Contain("Ash's turn!"), "Expected player turn message.");
            Assert.That(output, Does.Contain($"{pokemon.Name} Health: {pokemon.Health}/{pokemon.InitialHealth}"), "Expected Pokemon health info.");
            Assert.That(output, Does.Contain("What would you like to do?"), "Expected action prompt.");
            Assert.That(output, Does.Contain("1. Attack"), "Expected attack option.");
            Assert.That(output, Does.Contain("2. Use Item"), "Expected item option.");
            Assert.That(output, Does.Contain("3. Switch Pokémon"), "Expected switch Pokemon option.");
        }

        [Test]
        public void Effectiveness_ShouldPrintCorrectMessageForDifferentValues()
        {
            // Test cases for each effectiveness value
            var testCases = new[]
            {
                new { Value = 0, ExpectedMessage = "Attack was ineffective! X0 Damage!" },
                new { Value = 1, ExpectedMessage = "Attack was used! x1 Damage!" },
                new { Value = 2, ExpectedMessage = "Attack was effective! X2 Damage!" },
                new { Value = 3, ExpectedMessage = "Attack was slightly ineffective! X0.5 Damage!" }
            };

            foreach (var testCase in testCases)
            {
                // Arrange
                var attack = new TestAttack();

                // Reset StringWriter for each test
                _stringWriter = new StringWriter();
                Console.SetOut(_stringWriter);

                // Act
                Printer.Effectiveness(testCase.Value, attack);

                // Assert
                string output = _stringWriter.ToString().Trim();
                Assert.That(output, Does.Contain(testCase.ExpectedMessage), 
                    $"Expected correct message for effectiveness value {testCase.Value}");
            }
        }

        [Test]
        public void ForceSwitchMessage_ShouldDisplayDefeatMessage()
        {
            // Arrange
            var player = new TestPlayer("Ash");

            // Act
            Printer.ForceSwitchMessage(player);

            // Assert
            string output = _stringWriter.ToString();
            Assert.That(output, Does.Contain($"Ash your pokemon {player.SelectedPokemon.Name} Has been defeated!"), "Expected defeat message.");
            Assert.That(output, Does.Contain("Please pick another one from your list!"), "Expected pick another Pokemon prompt.");
        }

        [Test]
        public void SwitchQuestion_ShouldDisplaySwitchPrompt()
        {
            // Arrange
            var player = new TestPlayer("Ash");

            // Act
            Printer.SwitchQuestion(player);

            // Assert
            string output = _stringWriter.ToString();
            Assert.That(output, Does.Contain($"Ash want to change your pokemon {player.SelectedPokemon.Name}"), "Expected switch prompt.");
            Assert.That(output, Does.Contain("1) Yes 2) No"), "Expected yes/no options.");
        }

        [Test]
        public void SwitchConfirmation_WhenOptionZero_ShouldDisplayConfirmationMessage()
        {
            // Arrange
            var player = new TestPlayer("Ash");

            // Act
            Printer.SwitchConfirmation(player, 0);

            // Assert
            string output = _stringWriter.ToString();
            Assert.That(output, Does.Contain("Ash your selected pokemon  Has been changed!"), "Expected change confirmation.");
            Assert.That(output, Does.Contain($"now is {player.SelectedPokemon.Name}"), "Expected new Pokemon name.");
        }

        [Test]
        public void CancelSwitchMessage_ShouldPrintCancellationMessage()
        {
            // Act
            Printer.CancelSwitchMessage();

            // Assert
            string output = _stringWriter.ToString();
            Assert.That(output, Does.Contain("Has decidido no cambiar de Pokémon."), "Expected cancellation message.");
            Assert.That(output, Does.Contain("Presiona cualquier tecla para continuar..."), "Expected continuation prompt.");
        }
        
    }
}