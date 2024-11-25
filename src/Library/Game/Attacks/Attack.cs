using Library.Game.Pokemons;

namespace Library.Game.Attacks;

public class Attack : IAttack
{
    /// <summary>
    /// Obtiene o establece el nombre del ataque.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Obtiene o establece el daño base que causa el ataque.
    /// </summary>
    public int Damage { get; set; }

    /// <summary>
    /// Obtiene o establece el estado que puede infligir el ataque al objetivo.
    /// </summary>
    public int State { get; set; }

    /// <summary>
    /// Obtiene o establece el tipo elemental del ataque (por ejemplo, Fuego, Agua, etc.).
    /// </summary>
    public string Type { get; set; }
    
    
    /// <summary>
    /// Specifies the special effect of the attack, if any.
    /// </summary>
    public SpecialEffect Special { get; set; }
    
    /// <summary>
    /// Obtiene o establece la precisión del ataque, representada como un porcentaje de éxito (0-100).
    /// </summary>
    public int Accuracy { get; set; }

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="Attack"/> con los valores especificados.
    /// </summary>
    public Attack(string name, int damage, SpecialEffect special, string type, int accuracy)
    {
        Name = name;
        Damage = damage;
        Special = special;
        Type = type;
        Accuracy = accuracy;
    }

    /// <summary>
    /// Calcula si el ataque es crítico.
    /// Un ataque es crítico con una probabilidad de 10%.
    /// </summary>
    /// <returns>True si el ataque es crítico, de lo contrario False.</returns>
    public bool IsCritical()
    {
        Random random = new Random();
        return random.Next(1, 11) == 1; // 10% de probabilidad
    }
}
