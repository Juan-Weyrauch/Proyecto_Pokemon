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
    // Tabla de efectividad de tipos
    private static readonly Dictionary<string, (List<string> Weaknesses, List<string> Resistances, List<string> Immunities)> EffectivenessTable =
        new Dictionary<string, (List<string>, List<string>, List<string>)>
        {
            { "Bug", (["Fire", "Rock", "Flying"], ["Grass", "Fighting", "Ground"], []) },
            { "Dragon", (["Dragon", "Ice", "Fairy"], ["Electric", "Fire", "Water", "Grass"], []) },
            { "Electric", (["Ground"], ["Flying", "Steel", "Electric"], []) },
            { "Fire", (["Water", "Rock", "Ground"], ["Bug", "Grass", "Ice", "Steel"], []) },
            { "Flying", (["Electric", "Ice", "Rock"], ["Bug", "Fighting", "Grass"], []) },
            { "Ghost", (["Ghost", "Dark"], ["Poison", "Bug"], ["Normal", "Fighting"]) },
            { "Grass", (["Fire", "Ice", "Poison", "Flying", "Bug"], ["Water", "Electric", "Grass", "Ground"], []) },
            { "Ground", (["Water", "Grass", "Ice"], ["Poison", "Rock"], ["Electric"]) },
            { "Ice", (["Fire", "Fighting", "Rock", "Steel"], ["Ice"], []) },
            { "Normal", (["Fighting"], [], ["Ghost"]) },
            { "Poison", (["Ground", "Psychic"], ["Grass", "Fighting", "Poison", "Bug", "Fairy"], []) },
            { "Psychic", (["Bug", "Ghost", "Dark"], ["Fighting", "Psychic"], []) },
            { "Rock", (["Water", "Grass", "Fighting", "Ground", "Steel"], ["Normal", "Fire", "Poison", "Flying"], []) },
            { "Steel", (["Fire", "Fighting", "Ground"], ["Normal", "Grass", "Ice", "Flying", "Psychic", "Bug", "Rock", "Dragon", "Steel", "Fairy"], []) },
            { "Water", (["Electric", "Grass"], ["Fire", "Water", "Ice", "Steel"], []) }
        };


    /// <summary>
    /// This method calculates the damage and applies it to the rival's Pokémon.
    /// </summary>
    /// <param name="attack">The attack being used.</param>
    /// <param name="receiver">The Pokémon receiving the attack.</param>
    public static void InfringeDamage(IAttack attack, IPokemon receiver, IPokemon attacker)
    {
        ArgumentNullException.ThrowIfNull(attack);
        ArgumentNullException.ThrowIfNull(receiver);
        ArgumentNullException.ThrowIfNull(attacker);

        // Obtener la efectividad del ataque contra el tipo del receptor
        double effectiveness = CheckEffectiveness(attack, receiver);

        // Calcular el daño total
        int rawDamage = attack.Damage;
        int adjustedDamage = (int)(rawDamage * effectiveness);
        int actualDamage = Math.Max(adjustedDamage - receiver.Defense, 0);

        // Aplicar daño al receptor
        receiver.Health = Math.Max(receiver.Health - actualDamage, 0);

        // Mostrar un resumen del ataque
        Printer.AttackSummary(attacker, attack, receiver, actualDamage);
    }



    /// <summary>
    /// Checks the effectiveness of an attack against a Pokémon's type.
    /// </summary>
    /// <param name="attack">The attack being used.</param>
    /// <param name="pokemon">The Pokémon receiving the attack.</param>
    /// <returns>A multiplier for the damage based on effectiveness.</returns>
    public static double CheckEffectiveness(IAttack attack, IPokemon pokemon)
    {
        ArgumentNullException.ThrowIfNull(attack);
        ArgumentNullException.ThrowIfNull(pokemon);

        if (!EffectivenessTable.ContainsKey(attack.Type))
        {
            Console.WriteLine($"Warning: Type '{attack.Type}' not found in EffectivenessTable. Defaulting to 1.0.");
            return 1.0; // Si el tipo no está definido, devuelve daño normal.
        }

        var (weaknesses, resistances, immunities) = EffectivenessTable[pokemon.Type];

        if (immunities.Contains(attack.Type))
            return 0.0;
        if (weaknesses.Contains(attack.Type))
            return 2.0;
        if (resistances.Contains(attack.Type))
            return 0.5;

        return 1.0; // Daño normal si no hay modificaciones.
    }


    /// <summary>
    /// Validates that a number is within a given range and ensures the input is an integer.
    /// </summary>
    /// <param name="min">Minimum value in range.</param>
    /// <param name="max">Maximum value in range.</param>
    /// <returns>The validated integer input.</returns>
    public static int ValidateSelectionInGivenRange(int min, int max)
    {
        int number = 0;
        bool isValid = false;

        while (!isValid)
        {
            string input = Console.ReadLine();

            if (int.TryParse(input, out number) && number >= min && number <= max)
            {
                isValid = true;
            }
            else
            {
                Console.WriteLine($"Please enter a number between {min} and {max}.");
            }
        }

        return number;
    }

    /// <summary>
    /// Checks if a player has any active Pokémon remaining.
    /// </summary>
    /// <param name="player">The player being checked.</param>
    /// <returns>True if the player has active Pokémon, false otherwise.</returns>
    public static bool HasActivePokemon(IPlayer player)
    {
        ArgumentNullException.ThrowIfNull(player);

        return player.Pokemons.Any(pokemon => pokemon.Health > 0);
    }
}
