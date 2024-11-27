using System.Data;
using Library.Game.Attacks;
using Library.Game.Items;
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
    private static readonly Random Random = new();
// Table of attacks.
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
                    "Dragon",
                    (["Dragon", "Ice"],
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
            throw new NoNullAllowedException("AttackList or Pokemon was null.");
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
        return Random.Next(1, 2);
        //Returns a random number to set a starter player.
    }

    /// <summary>
    /// This method checks if the player has any active Pokémon.
    /// </summary>
    /// <param name="player"></param>
    /// <returns>True if the player has Pokémon, False if not</returns>
    public static bool HasActivePokemon(IPlayer player)
    {
        ArgumentNullException.ThrowIfNull(player);
        ArgumentNullException.ThrowIfNull(player.Pokemons);

        if (player.Pokemons.Count == 0)
        {
            return false;
        }
        return true;
    }


    // ================================== DO DAMAGE / INFRINGE DAMAGE SECTION ==================================

    /// <summary>
    /// This class is responsible for:
    ///     1) Determining the effectiveness of the attack used.
    /// </summary>
    /// <param name="attack"></param>
    /// <param name="receiver"></param>
    /// <param name="attacker"></param>
    public static void InfringeDamage(IAttack attack, IPokemon receiver, IPokemon attacker)
    {
        ArgumentNullException.ThrowIfNull(attack);
        ArgumentNullException.ThrowIfNull(receiver);
        ArgumentNullException.ThrowIfNull(attacker);
        
        bool isCritical = attack.IsCritical();
        int rawDamage = isCritical ? (int)(attack.Damage * 1.2) : attack.Damage;

        // Calcular efectividad y daño
        double effectiveness = CheckEffectiveness(attack, receiver);
        int adjustedDamage = (int)(rawDamage * effectiveness);
        int actualDamage = Math.Max(adjustedDamage - receiver.Defense, 0);

        // Aplicar daño
        receiver.DecreaseHealth(actualDamage);

        // Llamada a Printer.AttackSummary
        Printer.AttackSummary(attacker, attack, receiver, actualDamage, isCritical);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    public static bool StateValidation(IPlayer player)
    {
        IPokemon pokemon = player.SelectedPokemon;

        switch (pokemon.State)
        {
            case SpecialEffect.None:
                return true; // The Pokémon can act normally.

            case SpecialEffect.Sleep:
                pokemon.ProcessTurnEffects(); // Decrease sleep turns.
                
                return false;

            case SpecialEffect.Paralyze:
                bool canAttack = Random.Next(0, 2) == 0; // 50% chance to act.
                if (!canAttack)
                {
                    
                    return false;
                }

                return canAttack;

            // these two cases are handled on the Pokémon class.
            case SpecialEffect.Poison:
            case SpecialEffect.Burn:
                // These effects reduce health but do not prevent actions.
                return true;
        }

        return true; // Default case (should not be reached).
    }
    public static string FuncionCalcularChances(IPlayer player1, IPlayer player2)
    {
        int contadorP1 = 10; //  Comienza con 10 para poder utilizar el Foreach Y eliminar ese 10. r
        int contadorP2 = 10;
        
        foreach (var pokemon in player1.Pokemons)
        {
            if (pokemon.Health > 0)
            {
                contadorP1 += 10;
            }

            if (pokemon.State != SpecialEffect.None)
            {
                contadorP1 -= 10; // Si uno no es diferente se le restan 10. 
                break;
            }
        }
        foreach (var item in player1.Items)
        {
            contadorP1 += 5 * (item.Count); // Son 6 items * 5 = 30, que es lo maximo.
        }
        foreach (IPokemon pokemon in player2.Pokemons)
        
        {
            if (pokemon.Health > 0)
            {
                contadorP2 += 10;
            }

            if (pokemon.State != SpecialEffect.None)
            {
                contadorP2 -= 10; // Si uno  es diferente se le restan 10. 
                break;
            }
        }
        foreach (var item in player2.Items)
        {
            contadorP2 += 5 * (item.Count); // Son 6 items * 5 = 30, que es lo maximo.
        }

        if (contadorP1 > contadorP2)
        {
            return $"P1 gano con {contadorP1} y P2 tiene {contadorP2}";
        }
        
        if(contadorP1 < contadorP2)
        {
            return $"P2 gano con {contadorP2} y P2 tiene {contadorP1}";
        }
        else
        {
            return "Ha sido Empate";
        }



        // Lista simplemente para recorrer con mas facilidad.

    }
}