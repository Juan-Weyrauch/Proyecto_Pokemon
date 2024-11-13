using System.Globalization;
using Library.Interfaces;

namespace Library.StaticClasses;

/// <summary>
/// This class has the responsabilities of making calculations, these being:
///     - Index in range
///     - Approving of inputs
///     - Damage calculations
/// </summary>
public static class Calculator
{
// Table of attacks.
    private static readonly Dictionary<string, (List<string> Weaknesses, List<string> Resistances, List<string> Immunities)> EffectivenessTable = 
        new Dictionary<string, (List<string>, List<string>, List<string>)>
        {
            { "Agua",(      new List<string> { "Eléctrico", "Planta" },                          new List<string> { "Agua", "Fuego", "Hielo" },                                    new List<string>()) },
            { "Bicho",(     new List<string> { "Fuego", "Roca", "Volador", "Veneno" },           new List<string> { "Lucha", "Planta", "Tierra" },                                 new List<string>()) },
            { "Dragón",(    new List<string> { "Dragón", "Hielo" },                              new List<string> { "Agua", "Eléctrico", "Fuego", "Planta" },                      new List<string>()) },
            { "Eléctrico",( new List<string> { "Tierra" },                                       new List<string> { "Volador" },                                                   new List<string> { "Eléctrico" }) },
            { "Fantasma",(  new List<string> { "Fantasma" },                                     new List<string> { "Veneno", "Lucha" },                                           new List<string> { "Normal" }) },
            { "Fuego",(     new List<string> { "Agua", "Roca", "Tierra" },                       new List<string> { "Bicho", "Fuego", "Planta" },                                  new List<string>()) },
            { "Hielo",(     new List<string> { "Fuego", "Lucha", "Roca" },                       new List<string> { "Hielo" },                                                     new List<string>()) },
            { "Lucha",(     new List<string> { "Psíquico", "Volador", "Bicho", "Roca" },         new List<string>(),                                                               new List<string>()) },
            { "Normal",(    new List<string> { "Lucha" },                                        new List<string>(),                                                               new List<string> { "Fantasma" }) },
            { "Planta",(    new List<string> { "Bicho", "Fuego", "Hielo", "Veneno", "Volador" }, new List<string> { "Agua", "Eléctrico", "Planta", "Tierra" },                     new List<string>()) },
            { "Psíquico",(  new List<string> { "Bicho", "Lucha", "Fantasma" },                   new List<string>(),                                                               new List<string>()) },
            { "Roca",(      new List<string> { "Agua", "Lucha", "Planta", "Tierra" },            new List<string> { "Fuego", "Normal", "Veneno", "Volador" },                      new List<string>()) },
            { "Tierra",(    new List<string> { "Agua", "Hielo", "Planta" },                      new List<string> { "Eléctrico" },                                                 new List<string>()) },
            { "Veneno",(    new List<string> { "Psíquico", "Tierra" },                           new List<string> { "Bicho", "Planta", "Veneno" },                                 new List<string>()) },
            { "Volador",(   new List<string> { "Eléctrico", "Hielo", "Roca" },                   new List<string> { "Bicho", "Lucha", "Planta" },                                  new List<string>()) }
        };

    /// <summary>
    /// Method that returns the numeric percentage of boost (or not) that the attack has on the opponent. 
    /// </summary>
    /// <param name="attackType"></param>
    /// <param name="pokemonType"></param>
    /// <returns></returns>
    public static double GetEffectivenessMultiplier(string attackType, string pokemonType)
    {
        if (!EffectivenessTable.ContainsKey(attackType)) return 1.0;
        
        //using var because: (yes, thanks).
        var (weaknesses, resistances, immunities) = EffectivenessTable[attackType];

        if (immunities.Contains(pokemonType)) return 0.0;
        if (weaknesses.Contains(pokemonType)) return 2.0;
        if (resistances.Contains(pokemonType)) return 0.5;

        return 1.0; // Daño normal si no hay modificaciones
    }
    
    /// <summary>
    /// Checks for effectiness in the attack recieved.
    /// It compares the attack type to the Pokémon type and returns a double value for the effectiveness.
    /// </summary>
    /// <param name="attack"></param>
    /// <param name="pokemon"></param>
    /// <returns></returns>
    public static double CheckEffectiveness(IAttack attack, IPokemon pokemon)
    {
        return GetEffectivenessMultiplier(attack.Type, pokemon.Type);
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



    public static int FirstTurnSelection()
    {
        //Always starts the player 1? Should it be random?
        Random random = new Random();
        return random.Next(1, 2);
        //Returns a random number to set a starter player.
    }

    /// <summary>
    /// This class is responsible for:
    ///     1) Accessing the players Pokémon,
    ///     2) Determining the effectiveness of the attack used,
    ///     3) Calling the 'InfringeDamage' method to effectuate the damage.
    /// </summary>
    /// <param name="attack"></param>
    /// <param name="rival"></param>
    public static int Attack(IAttack attack, IPokemon rival)
    {
        //We've got the attack and the rival Pokémon, now we check for effectiveness (yes, again, the first one was for display of the Attacks)
        double efectiveness = CheckEffectiveness(attack, rival);
        if (efectiveness == 0.0)
        {
            Printer.Efectiveness(0);
        }
        else if (efectiveness == 0.5)
        {
            Printer.Efectiveness(1);
        }
        else if (efectiveness == 2.0)
        {
            Printer.Efectiveness(2);
        }
    }
}