namespace Library.Game.Items;

/// <summary>
/// This is a potion item, it allows the player to revive a dead pokemon.
/// </summary>
public class RevivePotion : IPotions
{
    /// <inheritdoc />
    public string Name { get; set; }

    /// <inheritdoc />
    public int RegenValue { get; set; }

    /// <summary>
    /// The constructor for the class.
    /// </summary>
    public RevivePotion()
    {
        Name = "Revive Potion";
        RegenValue = 50;
    }
    
}