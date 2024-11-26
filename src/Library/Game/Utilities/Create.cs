using Library.Game.Attacks;
using Library.Game.Pokemons;

namespace Library.Game.Utilities;

/// <summary>
/// This class is suposed to create all elements needed in the game.
/// These are:
///     - The Pokédex / Pokémon Catalogue
/// </summary>
public static class Create
{
    /// <summary>
    /// This Dictionary holds all the Pokémon.
    /// </summary>
    private static readonly Dictionary<int, IPokemon> Pokedex = [];


    /// <summary>
    /// This method creates ALL the Pokémon
    /// </summary>
    public static Dictionary<int, IPokemon> CreateCatalogue()
    {
        // Add 30 Pokémon to the catalog with second numbers (levels) between 0 and 15
        Pokedex.Add(1, new Pokemon("Venusaur", 12, "Plant", AttackGenerator.GenerateRandomAttack("Plant")));
        Pokedex.Add(2, new Pokemon("Blastoise", 14, "Water", AttackGenerator.GenerateRandomAttack("Water")));
        Pokedex.Add(3, new Pokemon("Butterfree", 7, "Bug", AttackGenerator.GenerateRandomAttack("Bug")));
        Pokedex.Add(4, new Pokemon("Charizard", 13, "Fire", AttackGenerator.GenerateRandomAttack("Fire")));
        Pokedex.Add(5, new Pokemon("Pikachu", 11, "Electric", AttackGenerator.GenerateRandomAttack("Electric")));
        Pokedex.Add(6, new Pokemon("Gengar", 10, "Ghost", AttackGenerator.GenerateRandomAttack("Ghost")));
        Pokedex.Add(7, new Pokemon("Articuno", 15, "Ice", AttackGenerator.GenerateRandomAttack("Ice")));
        Pokedex.Add(8, new Pokemon("Machamp", 9, "Fight", AttackGenerator.GenerateRandomAttack("Fight")));
        Pokedex.Add(9, new Pokemon("Snorlax", 12, "Normal", AttackGenerator.GenerateRandomAttack("Normal")));
        Pokedex.Add(10, new Pokemon("Alakazam", 15, "Psychic", AttackGenerator.GenerateRandomAttack("Psychic")));
        Pokedex.Add(11, new Pokemon("Onix", 8, "Rock", AttackGenerator.GenerateRandomAttack("Rock")));
        Pokedex.Add(12, new Pokemon("Golem", 10, "Earth", AttackGenerator.GenerateRandomAttack("Earth")));
        Pokedex.Add(13, new Pokemon("Nidoking", 11, "Venom", AttackGenerator.GenerateRandomAttack("Venom")));
        Pokedex.Add(14, new Pokemon("Pidgeot", 10, "Flying", AttackGenerator.GenerateRandomAttack("Flying")));
        Pokedex.Add(15, new Pokemon("Dragonite", 13, "Dragon", AttackGenerator.GenerateRandomAttack("Dragon")));
        Pokedex.Add(16, new Pokemon("Vaporeon", 14, "Water", AttackGenerator.GenerateRandomAttack("Water")));
        Pokedex.Add(17, new Pokemon("Jolteon", 12, "Electric", AttackGenerator.GenerateRandomAttack("Electric")));
        Pokedex.Add(18, new Pokemon("Flareon", 13, "Fire", AttackGenerator.GenerateRandomAttack("Fire")));
        Pokedex.Add(19, new Pokemon("Kabutops", 10, "Rock", AttackGenerator.GenerateRandomAttack("Rock")));
        Pokedex.Add(20, new Pokemon("Aerodactyl", 15, "Flying", AttackGenerator.GenerateRandomAttack("Flying")));
        Pokedex.Add(21, new Pokemon("Tyranitar", 14, "Rock", AttackGenerator.GenerateRandomAttack("Rock")));
        Pokedex.Add(22, new Pokemon("Scizor", 12, "Bug", AttackGenerator.GenerateRandomAttack("Bug")));
        Pokedex.Add(23, new Pokemon("Salamence", 11, "Dragon", AttackGenerator.GenerateRandomAttack("Dragon")));
        Pokedex.Add(24, new Pokemon("Metagross", 15, "Psychic", AttackGenerator.GenerateRandomAttack("Psychic")));
        Pokedex.Add(25, new Pokemon("Garchomp", 10, "Earth", AttackGenerator.GenerateRandomAttack("Earth")));
        Pokedex.Add(26, new Pokemon("Lucario", 13, "Fight", AttackGenerator.GenerateRandomAttack("Fight")));
        Pokedex.Add(27, new Pokemon("Togekiss", 9, "Flying", AttackGenerator.GenerateRandomAttack("Flying")));
        Pokedex.Add(28, new Pokemon("Greninja", 14, "Water", AttackGenerator.GenerateRandomAttack("Water")));
        Pokedex.Add(29, new Pokemon("Aegislash", 12, "Ghost", AttackGenerator.GenerateRandomAttack("Ghost")));
        Pokedex.Add(30, new Pokemon("Dragapult", 15, "Dragon", AttackGenerator.GenerateRandomAttack("Dragon")));

        return Pokedex;
    }
}