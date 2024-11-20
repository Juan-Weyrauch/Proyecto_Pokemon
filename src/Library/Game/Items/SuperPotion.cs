namespace Library.Game.Items;

/// <summary>
/// This super potion regenerates 70 points of HP to the Pokémon.
/// </summary>
public class SuperPotion : IPotions
{
    /// <inheritdoc />
    public string Name { get; set; }

    /// <inheritdoc />
    public int RegenValue { get; set; }

    /// <summary>
    /// Constructor for the class SuperPotion.
    /// </summary>
    public SuperPotion()
    {
        Name = "Super Potion";
        RegenValue = 70;
    }
}