
using Library.Game.Pokemons;
using NUnit.Framework;

using Library.Game.Attacks;//using NUnit.Framework;

using Moq;

using Library.Game.Items;

namespace Library.Game.Utilities.Tests
{
    [TestFixture]
    internal sealed class PrinterTests
    {
        private StringWriter _consoleOutput;

        [SetUp]
        public void Setup()
        {
            // Redirect console output to a StringWriter for verification
            _consoleOutput = new StringWriter();
            Console.SetOut(_consoleOutput);
        }

        [TearDown]
        public void TearDown()
        {
            // Reset console output and dispose of StringWriter
            _consoleOutput.Dispose();
            Console.SetOut(Console.Out); // Reset to default
        }


        // ========================= Start and End Game Messages =====================
        [Test]
        public void StartPrint_ShouldDisplayWelcomeMessage()
        {
            // Arrange
            Printer.StartPrint();

            // Assert
            string expected = "╔═══════════════════════════════════════╗\n" +
                              "║       Welcome to Pokemon Battle       ║\n" +
                              "╚═══════════════════════════════════════╝\n";
            Assert.That(_consoleOutput.ToString(), Does.Contain(expected));
        }

        [Test]
        public void EndPrint_ShouldDisplayThankYouMessage()
        {
            // Arrange
            Printer.EndPrint();

            // Assert
            string expected = "╔════════════════════════════╗\n" +
                              "║    Thanks for playing!!    ║\n" +
                              "╚════════════════════════════╝\n";
            Assert.That(_consoleOutput.ToString(), Does.Contain(expected));
        }

        
        // =================================== Dynamic Box Display ===============================
        [Test]
        public void DisplayWinner_ShouldFormatBoxWithWinnerName()
        {
            string winner = "Ash";
            using var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            Printer.DisplayWinner(winner);

            string expected = $"The winner is {winner}!!";
            Assert.That(consoleOutput.ToString(), Does.Contain(expected));
        }

        [Test]
        public void IndexOutOfRange_ShouldDisplayMinMaxMessage()
        {
            int min = 1, max = 10;
            using var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            Printer.IndexOutOfRange(min, max);

            string expected = $"El valor debe ser mayor que {min}\ny menor que {max}";
            Assert.That(consoleOutput.ToString(), Does.Contain(expected));
        }

        // ================== Name Selection and Turn Notifications =========================
        [Test]
        public void NameSelection_ShouldPromptForName()
        {
            using var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            Printer.NameSelection();

            string expected = "Enter your name";
            Assert.That(consoleOutput.ToString(), Does.Contain(expected));
        }

        [Test]
        public void YourTurn_ShouldDisplayTurnMessage()
        {
            string playerName = "Misty";
            using var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            Printer.YourTurn(playerName);

            string expected = $"Your turn Player {playerName}";
            Assert.That(consoleOutput.ToString(), Does.Contain(expected));
        }

        
        //============================= Pokémon Catalogue and Inventory ===============================
        [Test]
        public void ShowCatalogue_ShouldDisplayPokemonInBoxes()
        {
            var pokedex = new Dictionary<int, IPokemon>
            {
                { 1, Mock.Of<IPokemon>(p => p.Name == "Pikachu" && p.Health == 100) },
                { 2, Mock.Of<IPokemon>(p => p.Name == "Charmander" && p.Health == 80) }
            };

            using var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            Printer.ShowCatalogue(pokedex);

            Assert.That(consoleOutput.ToString(), Does.Contain("Name: Pikachu"));
            Assert.That(consoleOutput.ToString(), Does.Contain("Name: Charmander"));
        }

        [Test]
        public void ShowInventory_ShouldDisplayPokemonInventory()
        {
            var inventory = new List<IPokemon>
            {
                Mock.Of<IPokemon>(p => p.Name == "Bulbasaur" && p.Health == 70),
                Mock.Of<IPokemon>(p => p.Name == "Squirtle" && p.Health == 85)
            };

            using var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            Printer.ShowInventory(inventory);

            Assert.That(consoleOutput.ToString(), Does.Contain("Name: Bulbasaur"));
            Assert.That(consoleOutput.ToString(), Does.Contain("Name: Squirtle"));
        }

        // ================================== Attack Displays ==========================================
        [Test]
        public void ShowAttacks_ShouldDisplayAttackDetails()
        {
            var attacks = new List<IAttack>
            {
                Mock.Of<IAttack>(a => a.Name == "Shadow Ball" && a.Damage == 50 && a.Type == "Ghost")
            };

            var attacker = Mock.Of<IPokemon>(p => p.Name == "Gengar" && p.AtackList == attacks);
            var receiver = Mock.Of<IPokemon>();

            Printer.ShowAttacks(attacker, receiver);

            Assert.That(_consoleOutput.ToString(), Does.Contain("Shadow Ball"));
            Assert.That(_consoleOutput.ToString(), Does.Contain("Damage: 50"));
            Assert.That(_consoleOutput.ToString(), Does.Contain("Type: Ghost"));
        }

        // ================================ Battle Messages ============================================
        [Test]
        public void AttackSummary_ShouldDisplayDamageDetails()
        {
            var attacker = Mock.Of<IPokemon>(p => p.Name == "Pikachu");
            var receiver = Mock.Of<IPokemon>(p => p.Name == "Snorlax" && p.Health == 50);
            var attack = Mock.Of<IAttack>(a => a.Name == "Thunderbolt");

            using var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            Printer.AttackSummary(attacker, attack, receiver, 20, critical: true);

            Assert.That(consoleOutput.ToString(), Does.Contain("Pikachu used Thunderbolt!"));
            Assert.That(consoleOutput.ToString(), Does.Contain("It dealt 20 damage."));
            Assert.That(consoleOutput.ToString(), Does.Contain("Snorlax has 50 HP remaining."));
            Assert.That(consoleOutput.ToString(), Does.Contain("critical hit!"));
        }

        // ================================ Status Effects ======================================
        [Test]
        public void CantAttackBecauseOfStatus_ShouldDisplayStatusReason()
        {
            var pokemon = Mock.Of<IPokemon>(p => p.Name == "Charmander");

            using var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            Printer.CantAttackBecauseOfStatus(pokemon);

            Assert.That(consoleOutput.ToString(), Does.Contain("Charmander can't attack!"));
            Assert.That(consoleOutput.ToString(), Does.Contain("Reason: It is Paralyzed."));
        }

        // ===================================== Miscellaneous ===========================================
        [Test]
        public void PressToContinue_ShouldPauseWithMessage()
        {
            Console.SetIn(new StringReader("\n"));

            Printer.PressToContinue();

            Assert.That(_consoleOutput.ToString(), Does.Contain("Press any key to continue!"));
        }

    }
}