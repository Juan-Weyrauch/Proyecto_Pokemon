namespace Library.Game.Items;

/// <summary>
/// This class Creates a potion that allows the player to cure the chosen Pokémon to 100% of HP.
/// </summary>
public class TotalCure : IPotions
{
    /// <inheritdoc />
    public string Name { get; set; }

    /// <inheritdoc />
    public int RegenValue { get; set; }

    /// <summary>
    /// Constructor for the class.
    /// </summary>
    public TotalCure()
    {
        Name = "Total Cure";
        RegenValue = 0;
    }
}