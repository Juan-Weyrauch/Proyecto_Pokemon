using Library.Game.Attacks;
using Library.Game.Players;
using Library.Game.Pokemons;
using Library.Game.Utilities;
using NUnit.Framework;
using System.Collections.Generic;

namespace Library.Tests
{
    [TestFixture]
    public class CalculatorTests
    {
        private IAttack _fireAttack;
        private IAttack _electricAttack;
        private IAttack _waterAttack;
        private IPokemon _bulbasaur;
        private IPokemon _squirtle;
        private IPokemon _electrode;

        // Setup runs before each test
        [SetUp]
            public void Setup()
            {
                // Create example attacks
                _fireAttack = new Attack("Flame Thrower", 90, SpecialEffect.None, "Fire", 100);
                _electricAttack = new Attack("Thunderbolt", 50, SpecialEffect.None, "Electric", 100);
                _waterAttack = new Attack("Water Gun", 40, SpecialEffect.None, "Water", 100);

                // Create example Pokémon
                _bulbasaur = new Pokemon("Bulbasaur", 100, "Plant", new List<IAttack> { _waterAttack });
                _squirtle = new Pokemon("Squirtle", 100, "Water", new List<IAttack> { _waterAttack });
                _electrode = new Pokemon("Electrode", 100, "Electric", new List<IAttack> { _electricAttack });

                // Manually initialize players

                
            }

        // TearDown runs after each test
        [TearDown]
        public void TearDown()
        {
            // Cleanup resources if necessary (no need for null assignment in this case)
        }

        // Test effectiveness with normal damage multiplier
        [Test]
        public void CheckEffectiveness_ShouldReturnCorrectMultiplier_ForNormalEffectiveness()
        {
            // Act
            Player.InitializePlayer1("Juan", new List<IPokemon> { _bulbasaur, _squirtle, _electrode }, _squirtle);
            double effectiveness = Calculator.CheckEffectiveness(_fireAttack, _bulbasaur);

            // Assert: Fire should deal double damage to Plant
            Assert.That(effectiveness, Is.EqualTo(2.0), "Fire should deal double damage to Plant.");
        }

        // Test effectiveness with resistance multiplier
        [Test]
        public void CheckEffectiveness_ShouldReturnCorrectMultiplier_ForResistance()
        {
            // Act
            double effectiveness = Calculator.CheckEffectiveness(_fireAttack, _squirtle);

            // Assert: Fire should deal half damage to Water
            Assert.That(effectiveness, Is.EqualTo(0.5), "Fire should deal half damage to Water.");
        }

        // Test effectiveness with immunity multiplier
        [Test]
        public void CheckEffectiveness_ShouldReturnZero_ForImmunity()
        {
            // Act
            double effectiveness = Calculator.CheckEffectiveness(_electricAttack, _electrode);

            // Assert: Electric should deal no damage to Electric
            Assert.That(effectiveness, Is.EqualTo(0.0), "Electric should deal no damage to Electric.");
        }

        // Test if damage is applied correctly considering effectiveness
        /*[Test]
         public void InfringeDamage_ShouldApplyCorrectDamageWithEffectiveness()
         {
             // Set initial health of Bulbasaur
             _bulbasaur.Health = 100;

             // Act: Apply Fire attack to Bulbasaur (which has a Plant type)
             Calculator.InfringeDamage(_fireAttack, _bulbasaur,_electrode);

             // Assert: Bulbasaur should now have 20 health after taking double damage
             Assert.That(_bulbasaur.Health, Is.EqualTo(20), "Fire attack should reduce Bulbasaur's health to 20.");
         }
 */
        // Test if health does not go below zero
        //   [Test]
        /*    public void InfringeDamage_ShouldNotReduceHealthBelowZero()
            {
                // Create a strong attack with excessive damage
                var strongAttack = new Attack("Fire Blast", 200, SpecialEffect.None, "Fire",100);

                // Set initial health of Bulbasaur
                _bulbasaur.Health = 100;

                // Act: Apply a strong attack to Bulbasaur
                Calculator.InfringeDamage(strongAttack, _bulbasaur,_squirtle);

                // Assert: Health should not drop below zero
                Assert.That(_bulbasaur.Health, Is.EqualTo(0), "Health should not go below zero.");
            }*/

        // Test random player selection for the first turn
        [Test]
        public void FirstTurnSelection_ShouldReturnRandomFirstPlayer()
        {
            // Act: Select the first player randomly
            int result = Calculator.FirstTurnSelection();

            // Assert: The result should be either 1 or 2
            Assert.That(result, Is.InRange(1, 2), "First player should be either 1 or 2.");
        }

        // Test if player has active Pokémon
        [Test]
        public void HasActivePokemon_ShouldReturnTrueIfPlayerHasActivePokemon()
        {
            // Act: Check if player has active Pokémon
            bool result = Calculator.HasActivePokemon(Player.Player1);

            // Assert: Player 1 should have active Pokémon
            Assert.That(result, Is.True, "Player 1 should have active Pokémon.");
        }

        // Test if player has no active Pokémon
        [Test]
        public void HasActivePokemon_ShouldReturnFalseIfPlayerHasNoActivePokemon()
        {
            // Create an empty team for player 2
            var emptyTeam = new List<IPokemon>();

            // Initialize player 2 with no active Pokémon
            Player.InitializePlayer2("Misty", emptyTeam, null);

            // Act: Check if player has active Pokémon
            bool result = Calculator.HasActivePokemon(Player.Player2);

            // Assert: Player 2 should not have active Pokémon
            Assert.That(result, Is.False, "Player 2 should not have active Pokémon.");
        }
        [Test]
        public void ValidateSelectionInGivenRange_ShouldReturnValidNumber_WithinRange()
        {
            // Arrange: Usar una entrada válida ("5")
            var mockInput = "5";  // El valor que simularemos como entrada
            int min = 1;
            int max = 10;

            // Redirigir la entrada estándar de Console para que use el mockInput
            var reader = new StringReader(mockInput);
            Console.SetIn(reader);

            // Act: Llamar al método que validará la entrada
            int result = Calculator.ValidateSelectionInGivenRange(min, max);

            // Assert: El resultado debe ser igual al valor de entrada ("5")
            Assert.That(result, Is.EqualTo(5), "El número debería estar dentro del rango especificado.");
        }


        // Test ValidateSelectionInGivenRange for input less than the min value
        [Test]
        public void ValidateSelectionInGivenRange_ShouldPromptForInput_IfLessThanMin()
        {
            // Arrange: Using input less than the min range
            string mockInput = "0\n5"; // Primero una entrada inválida (0), luego una válida (5)
            int min = 1;
            int max = 10;

            // Redirigir la entrada estándar para simular la entrada del usuario
            var reader = new StringReader(mockInput);
            Console.SetIn(reader);

            // Act: Validate input less than min (should retry until valid input is provided)
            int result = Calculator.ValidateSelectionInGivenRange(min, max);

            // Assert: The result should be valid and within the range
            Assert.That(result, Is.InRange(min, max), "The number should be within the given range.");
        }

        // Test ValidateSelectionInGivenRange for input greater than the max value
        
       
        [Test]
        
        public void ValidateSelectionInGivenRange_ShouldPromptForInput_IfGreaterThanMax()
        {
            // Arrange: Usar entrada fuera del rango, luego una válida
            var mockInput = "11\n5"; // Primero un valor fuera de rango ("11"), luego un valor válido ("5")
            int min = 1;
            int max = 10;

            // Redirigir Console.ReadLine() para que devuelva los valores que simulamos
            var reader = new StringReader(mockInput);
            Console.SetIn(reader);

            // Act: Llamar al método para validar que el valor se corrige después del primer intento fallido
            int result = Calculator.ValidateSelectionInGivenRange(min, max);

            // Assert: El resultado debe ser un valor dentro del rango válido
            Assert.That(result, Is.InRange(min, max), "El número debería estar dentro del rango especificado.");
        }

        // Test ValidateSelectionInGivenRange for invalid (non-numeric) input
        [Test]
        public void ValidateSelectionInGivenRange_ShouldPromptForInput_IfNotANumber()
        {
            // Arrange: Using invalid (non-numeric) input
            var mockInput = "abc\n5";
            int min = 1;
            int max = 10;
            var reader = new StringReader(mockInput);
            Console.SetIn(reader);
            // Act: Validate non-numeric input (should prompt user again)
            int result = Calculator.ValidateSelectionInGivenRange(min, max);

            // Assert: The result should be a valid number within the range
            Assert.That(result, Is.InRange(min, max), "The number should be within the given range.");
        }
        [Test]
        public void InfringeDamage_ShouldCalculateDamageCorrectly_WhenNoDefense()
        {
            // Arrange
            _bulbasaur.Health = 100;
            _bulbasaur.Defense = 0;
    
            // Prepare to simulate console input of "F"
           
            Console.SetIn(new StringReader("F"));
            // Act
            Calculator.InfringeDamage(_fireAttack, _bulbasaur, _electrode);
            Console.SetIn(Console.In);
            // Assert
            Assert.That(_bulbasaur.Health, Is.EqualTo(0), "Bulbasaur's health should be reduced to 0");
        }


[Test]
public void InfringeDamage_ShouldReduceDamageBasedOnDefense()
{
    // Arrange
    _bulbasaur.Health = 100;
    _bulbasaur.Defense = 20;
    
    // Fire attack against Bulbasaur (Plant type)
    // Raw damage: 90
    // Effectiveness: 2.0
    // Adjusted damage: 90 * 2.0 = 180
    // Actual damage: 180 - 20 = 160

    // Act
    Calculator.InfringeDamage(_fireAttack, _bulbasaur, _electrode);

    // Assert
    Assert.That(_bulbasaur.Health, Is.EqualTo(0), "Bulbasaur's health should be reduced to 0");
}

[Test]
public void InfringeDamage_ShouldNotReduceHealthBelowZero()
{
    // Arrange
    _bulbasaur.Health = 20;
    _bulbasaur.Defense = 10;
    
    // Strong attack
    var strongAttack = new Attack("Mega Blast", 200, SpecialEffect.None, "Fire", 100);

    // Act
    Calculator.InfringeDamage(strongAttack, _bulbasaur, _electrode);

    // Assert
    Assert.That(_bulbasaur.Health, Is.EqualTo(0), "Health should not go below zero");
}


[Test]
public void InfringeDamage_ShouldThrowArgumentNullException_WhenAttackIsNull()
{
    var ex = Assert.Throws<ArgumentNullException>(() => 
        Calculator.InfringeDamage(null, _bulbasaur, _electrode));
    Assert.That(ex.ParamName, Is.EqualTo("attack"));
}

[Test]
public void InfringeDamage_ShouldThrowArgumentNullException_WhenReceiverIsNull()
{
    var ex = Assert.Throws<ArgumentNullException>(() => 
        Calculator.InfringeDamage(_fireAttack, null, _electrode));
    Assert.That(ex.ParamName, Is.EqualTo("receiver"));
}

[Test]
public void InfringeDamage_ShouldThrowArgumentNullException_WhenAttackerIsNull()
{
    var ex = Assert.Throws<ArgumentNullException>(() => 
        Calculator.InfringeDamage(_fireAttack, _bulbasaur, null));
    Assert.That(ex.ParamName, Is.EqualTo("attacker"));
}
[Test]
public void InfringeDamage_ShouldCalculateDamageCorrectly_WithResistance()
{
    // Arrange
    _squirtle.Health = 100;
    _squirtle.Defense = 10;
    
    // Fire attack against Squirtle (Water type) with 0.5 effectiveness
    // Raw damage: 90
    // Effectiveness: 0.5
    // Adjusted damage: 90 * 0.5 = 45
    // Actual damage: 45 - 10 = 35

    // Act
    Calculator.InfringeDamage(_fireAttack, _squirtle, _electrode);

    // Assert
    Assert.That(_squirtle.Health, Is.EqualTo(65), "Squirtle's health should be reduced by actual damage");
}
        // Test GetEffectivenessMultiplier for various scenarios
        }
    
}
    