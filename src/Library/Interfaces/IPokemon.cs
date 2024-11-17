using Library.Classes;
using Library.Interfaces;

namespace Library;

/// <summary>
/// Interface for Pokemons 
/// </summary>
public interface IPokemon
{
    /// <summary>
    /// Name for pokemons
    /// </summary>
    string Name { get; set; }
    
    /// <summary>
    /// Health of the pokemon
    /// </summary>
    int Health { get; set; }
    
    /// <summary>
    /// Initialhealth of the pokemon
    /// </summary>
    int InitialHealth { get; set; }
    
    /// <summary>
    /// The pokemon must have a defense status
    /// </summary>
    int Defense { get; set; }
    
    /// <summary>
    /// Sets the type of the pokemon
    /// </summary>
    string Type { get; set; }
    
    /// <summary>
    /// Sets the state of the pokemon
    /// </summary>
    public Estado State { get; set; }

    
    
    /// <summary>
    /// A list of all the attacks that the pokemon has 
    /// </summary>
    List<IAttack> AtackList { get; }


    public IAttack GetAttack(int index);

    int TurnosDormido { get; set; }

}