using Library.Game.Attacks;

namespace Library.Game.Pokemons;

/// <summary>
/// Interface for Pokémon 
/// </summary>
public interface IPokemon
{
    /// <summary>
    /// Name for Pokémon
    /// </summary>
    string Name { get; set; }
    
    /// <summary>
    /// HP points of the Pokémon
    /// </summary>
    int Health { get; set; }
    
    /// <summary>
    /// Initial health of the Pokémon
    /// </summary>
    int InitialHealth { get; set; }
    
    /// <summary>
    /// The Pokémon must have a defense status
    /// </summary>
    int Defense { get; set; }
    
    /// <summary>
    /// Sets the type of the Pokémon.
    /// </summary>
    string Type { get; set; }
    
    
    /// <summary>
    /// A list of all the attacks that the Pokémon has.
    /// </summary>
    List<IAttack> AtackList { get; }
    
    /// <summary>
    /// Sleep state.
    /// </summary>
    public bool IsAsleep { get; }
    /// <summary>
    /// Ammount of turns the Pokémon will be sleeping.
    /// </summary>
    public int SleepTurns { get; }
    /// <summary>
    /// Paralyzed state.
    /// </summary>
    public bool IsParalyzed { get; }
    /// <summary>
    /// Poisoned state.
    /// </summary>
    public bool IsPoisoned { get; }
    /// <summary>
    /// Burnt state.
    /// </summary>
    public bool IsBurned { get; }


    /// <summary>
    /// Returns the attack depending on the index inputted.
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public IAttack GetAttack(int index);

    /// <summary>
    /// Clones the attack so that there can exist multiple instances of the same.
    /// </summary>
    /// <returns></returns>
    public IPokemon Clone();
    
    /// <summary>
    /// Método para reducir la salud del Pokémon.
    /// </summary>
    void DecreaseHealth(int damage); // Método para reducir la salud

    /// <summary>
    /// Applies the desired effect. (should it be a method of Pokémon? idk, it processes its own state)
    /// </summary>
    void ProcessTurnEffects();

    /// <summary>
    /// Resets the status of the Pokémon. (for ex. when a total cure potion is used)
    /// </summary>
    void ResetStatus();
}