using System.Data;
using Library.Game.Attacks;
using Library.Game.Players;
using Library.Game.Pokemons;

namespace Library.Game.Utilities;

/// <summary>
/// This class has the responsibilities of making calculations, these being:
///     - Index in range
///     - Approving of inputs
///     - Damage calculations
/// </summary>
public static class Calculator
{
// Table of attacks
    private static readonly
        Dictionary<string, (List<string> Weaknesses, List<string> Resistances, List<string> Immunities)>
        EffectivenessTable =
            new Dictionary<string, (List<string>, List<string>, List<string>)>
            {
                {
                    "Water",
                    (["Electric", "Plant"], ["Water", "Fire", "Ice"],
                        [])
                },
                {
                    "Bug",
                    (["Fire", "Rock", "Flying", "Venom"],
                        ["Fight", "Plant", "Earth"], [])
                },
                {
                    "Dragón",
                    (["Dragón", "Ice"],
                        ["Water", "Electric", "Fire", "Plant"], [])
                },
                {
                    "Electric",
                    (["Earth"], ["Flying"], ["Electric"])
                },
                {
                    "Ghost",
                    (["Ghost"], ["Venom", "Fight"],
                        ["Normal"])
                },
                {
                    "Fire",
                    (["Water", "Rock", "Earth"], ["Bug", "Fire", "Plant"],
                        [])
                },
                {
                    "Ice",
                    (["Fire", "Fight", "Rock"], ["Ice"], [])
                },
                {
                    "Fight",
                    (["Psychic", "Flying", "Bug", "Rock"], [],
                        [])
                },
                { "Normal", (["Fight"], [], ["Ghost"]) },
                {
                    "Plant",
                    (["Bug", "Fire", "Ice", "Venom", "Flying"],
                        ["Water", "Electric", "Plant", "Earth"], [])
                },
                {
                    "Psychic",
                    (["Bug", "Fight", "Ghost"], [], [])
                },
                {
                    "Rock",
                    (["Water", "Fight", "Plant", "Earth"],
                        ["Fire", "Normal", "Venom", "Flying"], [])
                },
                {
                    "Earth",
                    (["Water", "Ice", "Plant"], ["Electric"],
                        [])
                },
                {
                    "Venom",
                    (["Psychic", "Earth"], ["Bug", "Plant", "Venom"],
                        [])
                },
                {
                    "Flying",
                    (["Electric", "Ice", "Rock"], ["Bug", "Fight", "Plant"],
                        [])
                }
            };

    /// <summary>
    /// Method that returns the numeric percentage of boost (or not) that the attack has on the opponent. 
    /// </summary>
    /// <param name="attackType"></param>
    /// <param name="pokemonType"></param>
    /// <returns></returns>
    private static double GetEffectivenessMultiplier(string attackType, string pokemonType)
    {
        if (!EffectivenessTable.ContainsKey(attackType)) return 1.0;

        //using var because: (yes, thanks).
        var (weaknesses, resistances, immunities) = EffectivenessTable[pokemonType];

        if (immunities.Contains(attackType)) return 0.0;
        if (weaknesses.Contains(attackType)) return 2.0;
        if (resistances.Contains(attackType)) return 0.5;

        return 1.0; // Daño normal si no hay modificaciones
    }

    /// <summary>
    /// Checks for effectiveness in the attack received.
    /// It compares the attack type to the Pokémon type and returns a double value for the effectiveness.
    /// </summary>
    /// <param name="attack"></param>
    /// <param name="pokemon"></param>
    /// <returns></returns>
    public static double CheckEffectiveness(IAttack attack, IPokemon pokemon)
    {
        if (attack != null && pokemon != null)
        {
            return (GetEffectivenessMultiplier(attack.Type, pokemon.Type));
        }
        else
        {
            throw new NoNullAllowedException("Attack or Pokemon was null.");
        }

    }

    /// <summary>
    /// Function to validate that a number is in between two given values and checks if the input is an integer.
    /// This function also reads the number inputted, this is so that the 'asking'
    /// and the 'validation' can happen in the same line.
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static int ValidateSelectionInGivenRange(int min, int max)
    {

        int number = 0;
        bool isValid = false;

        // Loop until we get a valid integer within the range
        while (!isValid)
        {
            //Printer.IndexOutOfRange(min, max);
            string input = Console.ReadLine();

            // Check if input is a valid integer
            if (int.TryParse(input, out number))
            {
                // Check if the integer is within the specified range
                if (number >= min && number <= max)
                {
                    isValid = true; // Input is valid, exit the loop
                }
                else
                {
                    Printer.IndexOutOfRange(min, max); // Print range error message
                }
            }
            else
            {
                Console.WriteLine("Please enter a numeric value!");
            }
        }

        return number;
    }

    /// <summary>
    /// Sets randomly the first player.
    /// </summary>
    /// <returns>integer 1 or 2</returns>
    public static int FirstTurnSelection()
    {
        //Always starts the player 1? Should it be random?
        Random random = new Random();
        return random.Next(1, 2);
        //Returns a random number to set a starter player.
    }

    /// <summary>
    /// This method checks if the player has any active Pokémon.
    /// </summary>
    /// <param name="player"></param>
    /// <returns>True if the player has Pokémon, False if not</returns>
    public static bool HasActivePokemon(IPlayer player)
    {
        if (player == null) return false;   
        
        if (player.Pokemons.Count == 0)
        {
            return false;
        }
        else return true;

    }


    // ================================== DO DAMAGE / INFRINGE DAMAGE SECTION ==================================

    /// <summary>
    /// This class is responsible for:
    ///     1) Determining the effectiveness of the attack used.
    /// </summary>
    /// <param name="attack"></param>
    /// <param name="rival"></param>
    public static void InfringeDamage(IAttack attack, IPokemon rival)
    {
        //We've got the attack and the rival Pokémon, now we check for effectiveness (yes, again, the first one was for display of the Attacks)
        double effectiveness = CheckEffectiveness(attack, rival);
        int damage = (int)(attack.Damage * effectiveness); // By Typecasting the variable,

        // we ensure we don't get a double.


        DoDamage(damage, rival);

        // Display the effectiveness to the user
        Printer.Effectiveness((int)effectiveness, attack);

    }

    /// <summary>
    /// This method calculates the damage and applies it to the rival's Pokémon.
    /// </summary>
    /// <param name="damage">The amount of damage inflicted.</param>
    /// <param name="pokemon">The Pokémon receiving the damage.</param>
    private static void DoDamage(int damage, IPokemon pokemon)
    {
        int actualDamage = Math.Max(damage - pokemon.Defense, 0); // Ensure no negative damage
        pokemon.Health = Math.Max(pokemon.Health - actualDamage, 0); // Ensure health doesn't go below 0
        
    }
}
