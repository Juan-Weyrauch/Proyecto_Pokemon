namespace Library.Game.Items;

/// <summary>
/// This interface sets the rules for the creation of potion elements.
/// The different kind of potions are:
///     - Revive potion:     (brings back a dead Pokémon with 50% of it's health)
///     - Super potion:      (Gives back 70% of the life to a Pokémon)
///     - Total Cure potion: (Eliminates any effect the Pokémon might have)
/// </summary>

public interface IPotions
{
    /// <summary>
    /// Sets the name of the potion
    /// </summary>
    string Name { get; set; }
    /// <summary>
    /// Sets the regeneration value of the potion
    /// </summary>
    int RegenValue { get; set; }
}