using Library.Classes;
using Library.Interfaces;

namespace Library.StaticClasses;

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
            Pokedex.Add(1,  new Pokemon("Venusaur",    83, "Planta",      AttackGenerator.GenerateRandomAttack("Plant")));
            Pokedex.Add(2,  new Pokemon("Blastoise",  100, "Water",       AttackGenerator.GenerateRandomAttack("Water")));
            Pokedex.Add(3,  new Pokemon("Butterfree",  60, "Bug",           AttackGenerator.GenerateRandomAttack("Bug")));
            Pokedex.Add(4,  new Pokemon("Charizard",   78, "Fire",         AttackGenerator.GenerateRandomAttack("Fire")));
            Pokedex.Add(5,  new Pokemon("Pikachu",     40, "Electric", AttackGenerator.GenerateRandomAttack("Electric")));
            Pokedex.Add(6,  new Pokemon("Gengar",      65, "Ghost",       AttackGenerator.GenerateRandomAttack("Ghost")));
            Pokedex.Add(7,  new Pokemon("Articuno",    85, "Ice",           AttackGenerator.GenerateRandomAttack("Ice")));
            Pokedex.Add(8,  new Pokemon("Machamp",     90, "Fight",       AttackGenerator.GenerateRandomAttack("Fight")));
            Pokedex.Add(9,  new Pokemon("Snorlax",    110, "Normal",     AttackGenerator.GenerateRandomAttack("Normal")));
            Pokedex.Add(10, new Pokemon("Alakazam",   55, "Psychic",    AttackGenerator.GenerateRandomAttack("Psychic")));
            Pokedex.Add(11, new Pokemon("Onix",      160, "Rock",          AttackGenerator.GenerateRandomAttack("Rock")));
            Pokedex.Add(12, new Pokemon("Golem",     130, "Earth",        AttackGenerator.GenerateRandomAttack("Earth")));
            Pokedex.Add(13, new Pokemon("Nidoking",   87, "Venom",        AttackGenerator.GenerateRandomAttack("Venom")));
            Pokedex.Add(14, new Pokemon("Pidgeot",    75, "Flying",      AttackGenerator.GenerateRandomAttack("Flying")));
            Pokedex.Add(15, new Pokemon("Dragonite",  95, "Dragon",      AttackGenerator.GenerateRandomAttack("Dragon")));
            Pokedex.Add(16, new Pokemon("Vaporeon",   60, "Water",        AttackGenerator.GenerateRandomAttack("Water")));
            Pokedex.Add(17, new Pokemon("Jolteon",    60, "Electric",  AttackGenerator.GenerateRandomAttack("Electric")));
            Pokedex.Add(18, new Pokemon("Flareon",    60, "Fire",          AttackGenerator.GenerateRandomAttack("Fire")));
            Pokedex.Add(19, new Pokemon("Kabutops",  105, "Rock",          AttackGenerator.GenerateRandomAttack("Rock")));
            Pokedex.Add(20, new Pokemon("Aerodactyl", 65, "Flying",      AttackGenerator.GenerateRandomAttack("Flying")));

            return Pokedex;
        }
    
    
}