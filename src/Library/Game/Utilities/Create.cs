using Library.Game.Attacks;
using Library.Game.Pokemon;

namespace Library.Game.Utilities;

/// <summary>
/// This class is suposed to create all elements needed in the game.
/// These being:
///     - The Pokedex / Pokemon Catalogue
/// </summary>
public static class Create
{
    /// <summary>
    /// This Dictionary holds all of the pokemons
    /// </summary>
    public static readonly Dictionary<int, IPokemon> Pokedex = new();

    
    /// <summary>
    /// This method creates ALL the Pokémon
    /// </summary>
    public static Dictionary<int, IPokemon> CreateCatalogue()
        {
            // Add 20 Pokémon to the catalog
          Pokedex.Add(1, new Pokemon.Pokemon("Venusaur", 23, "Plant", AttackGenerator.GenerateRandomAttack("Plant")));
Pokedex.Add(2, new Pokemon.Pokemon("Blastoise", 28, "Water", AttackGenerator.GenerateRandomAttack("Water")));
Pokedex.Add(3, new Pokemon.Pokemon("Butterfree", 17, "Bug", AttackGenerator.GenerateRandomAttack("Bug")));
Pokedex.Add(4, new Pokemon.Pokemon("Charizard", 26, "Fire", AttackGenerator.GenerateRandomAttack("Fire")));
Pokedex.Add(5, new Pokemon.Pokemon("Pikachu", 11, "Electric", AttackGenerator.GenerateRandomAttack("Electric")));
Pokedex.Add(6, new Pokemon.Pokemon("Gengar", 18, "Ghost", AttackGenerator.GenerateRandomAttack("Ghost")));
Pokedex.Add(7, new Pokemon.Pokemon("Articuno", 23, "Ice", AttackGenerator.GenerateRandomAttack("Ice")));
Pokedex.Add(8, new Pokemon.Pokemon("Machamp", 24, "Fight", AttackGenerator.GenerateRandomAttack("Fight")));
Pokedex.Add(9, new Pokemon.Pokemon("Snorlax", 30, "Normal", AttackGenerator.GenerateRandomAttack("Normal")));
Pokedex.Add(10, new Pokemon.Pokemon("Alakazam", 15, "Psychic", AttackGenerator.GenerateRandomAttack("Psychic")));
Pokedex.Add(11, new Pokemon.Pokemon("Onix", 22, "Rock", AttackGenerator.GenerateRandomAttack("Rock")));
Pokedex.Add(12, new Pokemon.Pokemon("Golem", 17, "Earth", AttackGenerator.GenerateRandomAttack("Earth")));
Pokedex.Add(13, new Pokemon.Pokemon("Nidoking", 12, "Venom", AttackGenerator.GenerateRandomAttack("Venom")));
Pokedex.Add(14, new Pokemon.Pokemon("Pidgeot", 10, "Flying", AttackGenerator.GenerateRandomAttack("Flying")));
Pokedex.Add(15, new Pokemon.Pokemon("Dragonite", 13, "Dragon", AttackGenerator.GenerateRandomAttack("Dragon")));
Pokedex.Add(16, new Pokemon.Pokemon("Vaporeon", 17, "Water", AttackGenerator.GenerateRandomAttack("Water")));
Pokedex.Add(17, new Pokemon.Pokemon("Jolteon", 17, "Electric", AttackGenerator.GenerateRandomAttack("Electric")));
Pokedex.Add(18, new Pokemon.Pokemon("Flareon", 17, "Fire", AttackGenerator.GenerateRandomAttack("Fire")));
Pokedex.Add(19, new Pokemon.Pokemon("Kabutops", 14, "Rock", AttackGenerator.GenerateRandomAttack("Rock")));
Pokedex.Add(20, new Pokemon.Pokemon("Aerodactyl", 18, "Flying", AttackGenerator.GenerateRandomAttack("Flying")));

            return Pokedex;
        }
    
    
}