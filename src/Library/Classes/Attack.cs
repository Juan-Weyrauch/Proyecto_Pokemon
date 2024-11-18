using Library.Interfaces;
using Library.StaticClasses;

namespace Library.Classes;

/// <summary>
/// These are the attacks.
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
    /// Sets the type of the attack.
    /// We use this to check for effectiveness against other Pokémon types.
    /// </summary>
    public string Type { get; set; }

    /// <summary>
    /// Class constructor.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="damage"></param>
    /// <param name="special"></param>
    /// <param name="type"></param>
    public Attack(string name, int damage, int special, string type)
    {
        Name = name;
        Damage = damage;
        Special = special;
        Type = type;
    }
    
    public IAttack Clone()
    {
        return new Attack(Name, Damage, Special, Type);
    }
}