namespace Library.Game.Attacks;

/// <summary>
/// These are the attacks.
/// </summary>
/// <remarks>
/// Primary constructor.
/// </remarks>
/// <param name="name"></param>
/// <param name="damage"></param>
/// <param name="special"></param>
/// <param name="type"></param>
public class Attack(string name, int damage, int special, string type) : IAttack
{
    /// <summary>
    /// Name of the attack.
    /// </summary>
    public string Name { get; set; } = name;

    /// <summary>
    /// Damage to the attack.
    /// </summary>
    public int Damage { get; set; } = damage;

    /// <summary>
    /// Check if the attack is special or not.
    /// </summary>
    public int Special { get; set; } = special;

    /// <summary>
    /// Sets the type of the attack.
    /// We use this to check for effectiveness against other Pokémon types.
    /// </summary>
    public string Type { get; set; } = type;
}