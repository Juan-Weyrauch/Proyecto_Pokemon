using Library.Interfaces;
using Library.StaticClasses;

namespace Library;

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
    
    public int State { get; set; }
    
    
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
    /// <param name="defense"></param>
    /// <param name="type"></param>
    /// <param name="atacks"></param>
    public Pokemon(string name, int defense, string type, List<IAttack> atacks)
    {
        Name = name;
        Health = 100;
        Defense = defense;
        Type = type;
        AtackList = atacks;
    }
    
    
    
}