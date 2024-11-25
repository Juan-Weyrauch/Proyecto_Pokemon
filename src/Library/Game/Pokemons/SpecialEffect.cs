namespace Library.Game.Pokemons;

/// <summary>
/// Enum 'Class' to contain the constants, these being the states that the pokemon can be in.
/// </summary>
public enum SpecialEffect
{
    /// <summary>
    /// The Pokémon has no effect.
    /// </summary>
    None,
    
    /// <summary>
    /// The Pokémon is asleep.
    /// When asleep, it can lose between 1 and 4 turns randomly.
    /// </summary>
    Sleep,
    
    /// <summary>
    /// The Pokémon is paralyzed.
    /// When paralyzed, it can or can't attack randomly.
    /// </summary>
    Paralyze,
    
    /// <summary>
    /// The Pokémon is infected (potion).
    /// When poisoned, it loses 5% of its total HP per turn.
    /// </summary>
    Poison,
    /// <summary>
    /// The Pokémon is burnt.
    /// When burnt, it loses 10% of its total HP per turn.
    /// </summary>
    Burn
}