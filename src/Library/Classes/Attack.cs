using Library.Interfaces;

namespace Library.Classes;

/// <summary>
/// This are the attacks.
/// </summary>
public class Attack : IAttack
{
    /// <summary>
    /// Name of the attack.
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Damage of the attack.
    /// </summary>
    public int Damage { get; set; }
    
    /// <summary>
    /// Check if the attack is a special one or not.
    /// </summary>
    public int Special { get; set; }

    /// <summary>
    /// Class constructor.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="damage"></param>
    /// <param name="special"></param>
    public Attack(string name, int damage, int special)
    {
        Name = name;
        Damage = damage;
        Special = special;
    }
}