namespace Library.Game.Attacks;

/// <summary>
/// 
/// </summary>
public interface IAttack
{
    /// <summary>
    /// Name of the attack
    /// </summary>
    string Name { get; set; }



    /// <summary>
    /// Damage to the attack
    /// </summary>
    int Damage { get; set; }



    /// <summary>
    /// Check if the attack is special or not
    /// </summary>
    int Special { get; set; }


    /// <summary>
    /// Sets the type of the attack.
    /// We use this to check for effectiveness against other Pokémon types.
    /// </summary>
    string Type { get; set; }

    /// <summary>
    /// Determina si el ataque es crítico.
    /// Un ataque es crítico con una probabilidad de 10%.
    /// </summary>
    bool IsCritical();

    /// <summary>
    /// Obtiene o establece la precisión del ataque.
    /// Representa el porcentaje de acierto del ataque (0-100).
    /// </summary>
    int Accuracy { get; set; }
}