using Library.Interfaces;

namespace Library.Classes;

public enum Estado
{
    /// <summary>
    /// The Pokémon is in a normal state.
    /// </summary>
    Normal = 0,
    
    /// <summary>
    /// The Pokémon is burned.
    /// </summary>
    Quemado = 1,
    
    /// <summary>
    /// The Pokémon is poisoned.
    /// </summary>
    Envenenado = 2,
    
    /// <summary>
    /// The Pokémon is paralyzed.
    /// </summary>
    Paralizado = 3,
    
    /// <summary>
    /// The Pokémon is asleep.
    /// </summary>
    Dormido = 4
}

/// <summary>
/// Pokemon class.
/// </summary>
public class Pokemon : IPokemon
{
    /// <summary>
    /// Name for pokemons
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Health of the pokemon
    /// </summary>
    public int Health { get; set; }
    
    /// <summary>
    /// The pokemon must have a defense status
    /// </summary>
    public int Defense { get; set; }
    
    /// <summary>
    /// Sets the type of the pokemon
    /// </summary>
    public string Type { get; set; }
    /// <summary>
    /// Gets or sets the current state of the Pokémon (e.g., Normal, Burned).
    /// </summary>
    public Estado State { get; set; }
    
    /// <summary>
    /// Gets or sets the number of turns the Pokémon remains asleep.
    /// </summary>
    public int TurnosDormido { get; set; }
    
    /// <summary>
    /// A list of all the attacks that the pokemon has 
    /// </summary>
    public List<IAttack> AtackList { get; }
    

    //who needs to create the pokemons?
    
    /// <summary>
    /// Constructor for the class Pokemon
    /// </summary>
    /// <param name="name"></param>
    /// <param name="health"></param>
    /// <param name="initialhealth"></param>
    /// <param name="defense"></param>
    /// <param name="type"></param>
    /// <param name="atacks"></param>
    public Pokemon(string name, int defense, string type, List<IAttack> atacks)
    {
        InitialHealth = 100;
        Name = name;
        Health = 100;
        Defense = defense;
        Type = type;
        AtackList = atacks;
        TurnosDormido = 0;
    }

    /// <summary>
    /// Returns the attack selected by the player
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public IAttack GetAttack(int index)
    {
        return AtackList[index];
    }

    /// <summary>
    /// Initial health of the pokemon.
    /// </summary>
    public int InitialHealth { get; set; }

    public void DecreaseHealth(int valueAfterCalculation)
    {
        Health -= valueAfterCalculation;
        if (Health < 0) Health = 0;
    }

}