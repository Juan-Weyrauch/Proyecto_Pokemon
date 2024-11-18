using Library.Interfaces;

namespace Library.Classes;

public enum Estado
{
    /// <summary>
    /// The Pokémon is in a normal state.
    /// </summary>
    Normal = 0,
    
    /// <summary>
    /// The Pokémon is burned.
    /// </summary>
    Quemado = 1,
    
    /// <summary>
    /// The Pokémon is poisoned.
    /// </summary>
    Envenenado = 2,
    
    /// <summary>
    /// The Pokémon is paralyzed.
    /// </summary>
    Paralizado = 3,
    
    /// <summary>
    /// The Pokémon is asleep.
    /// </summary>
    Dormido = 4
}
/// <summary>
/// Pokemon class.
/// </summary>
public class Pokemon : IPokemon
{
    /// <summary>
    /// Name for pokemons
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Health of the pokemon
    /// </summary>
    public int Health { get; set; }
    
    /// <summary>
    /// The pokemon must have a defense status
    /// </summary>
    public int Defense { get; set; }
    
    /// <summary>
    /// The Pokémon's type.
    /// </summary>
    public string Type { get; set; }
    
    /// <summary>
    /// The Pokémon's current state (e.g., Normal, Burned).
    /// </summary>
    public Estado State { get; set; }
    
    /// <summary>
    /// The number of turns the Pokémon remains asleep.
    /// </summary>
    public int TurnosDormido { get; set; }
    
    /// <summary>
    /// A list of all the attacks that the Pokémon has.
    /// </summary>
    public List<IAttack> Attacks { get; }

    //who needs to create the pokemons?
    
    /// <summary>
    /// Initial health of the Pokémon.
    /// </summary>
    public int InitialHealth { get; set; }
    
    public bool EnBatalla { get; set; }

    /// <summary>
    /// Constructor for the class Pokémon.
    /// </summary>
    public Pokemon(string name, int defense, string type, List<IAttack> attacks)
    {
        InitialHealth = 100;
        Name = name;
        Health = 100;
        Defense = defense;
        Type = type;
        Attacks = attacks;
        TurnosDormido = 0;
    }

    /// <summary>
    /// Creates a deep clone of the Pokémon.
    /// </summary>
    /// <returns>A new instance of the Pokémon with cloned properties.</returns>
    public IPokemon Clone()
    {
        return new Pokemon(Name, Defense, Type, Attacks.Select(attack => attack.Clone()).ToList())
        {
            Health = this.Health,
            InitialHealth = this.InitialHealth,
            State = this.State,
            TurnosDormido = this.TurnosDormido,
            EnBatalla = this.EnBatalla
        };
    }
    

    /// <summary>
    /// Returns the attack selected by the player.
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public IAttack GetAttack(int index)
    {
        return Attacks[index];
    }

    public void DecreaseHealth(int valueAfterCalculation)
    {
        Health -= valueAfterCalculation;
        if (Health < 0) Health = 0;
    }

/*  public IPokemon Clone()
 {
    return new Pokemon(this.Name, this.Defense, this.Type, this.AtackList);
 }
 */

 public void CambiarEstado(int nuevoEstado)
 {
     State = (Estado)nuevoEstado;
 }
}

