using Library.Game.Attacks;

namespace Library.Game.Pokemons;

/// <summary>
/// Pokémon class.
/// </summary>
public class Pokemon : IPokemon
{
    /// <summary>
    /// Name for Pokémon
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Health of the Pokémon
    /// </summary>
    public int Health { get; set; }
    
    /// <summary>
    /// The Pokémon must have a defense status
    /// </summary>
    public int Defense { get; set; }
    
    /// <summary>
    /// Sets the type of the Pokémon
    /// </summary>
    public string Type { get; set; }
    
    /// <summary>
    /// Sets the state of the Pokémon (1,2,3,4).
    /// </summary>
    public int State { get; set; }
    
    
    /// <summary>
    /// A list of all the attacks that the Pokémon has 
    /// </summary>
    public List<IAttack> AtackList { get; }
    

    //who needs to create the Pokémon?

    /// <summary>
    /// Constructor for the class Pokémon.
    /// </summary>
    /// <param name="name"></param>
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
    /// Initial health of the Pokémon.
    /// </summary>
    public int InitialHealth { get; set; }

    /// <summary>
    /// Clones the Pokémon so that there can be  multiple instances of the same.
    /// </summary>
    /// <returns></returns>
    public IPokemon Clone()
    {
        return new Pokemon(this.Name, this.Defense, this.Type, this.AtackList);
    }



}