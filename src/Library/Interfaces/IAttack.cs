namespace Library.Interfaces;

public interface IAttack
{
    /// <summary>
    /// Name of the attack
    /// </summary>
    string Name { get; set; }
    
    
    
    /// <summary>
    /// Damage of the attack
    /// </summary>
    int Damage { get; set; }
    
    
    
    /// <summary>
    /// Check if the attack is a special one or not
    /// </summary>
    int Special { get; set; }
}