using Library.Game.Pokemons;

namespace Library.Game.Attacks;

/// <summary>
/// 
/// </summary>
public interface IAttack
{
    /// <summary>
    /// Name of the attack
    /// </summary>
    string Name { get; set; }
    
    
    
    /// <summary>
    /// Damage to the attack
    /// </summary>
    int Damage { get; set; }
    
    
    
    /// <summary>
    /// Check if the attack is special or not
    /// </summary>
    Estado State { get; set; }
    
    
    /// <summary>
    /// Sets the type of the attack.
    /// We use this to check for effectiveness against other Pokémon types.
    /// </summary>
    string Type { get; set; }
    /// <summary>
    /// Method that let us clone attacks. 
    /// </summary>
    /// <returns></returns>
    public IAttack Clone();
}
