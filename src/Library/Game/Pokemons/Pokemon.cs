using Library.Game.Attacks;

namespace Library.Game.Pokemons;

/// <summary>
/// Pokémon class.
/// </summary>
public class Pokemon : IPokemon
{
    /// <summary>
    /// Name for Pokémon
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Health of the Pokémon
    /// </summary>
    public int Health { get; set; }

    /// <summary>
    /// The Pokémon must have a defense status
    /// </summary>
    public int Defense { get; set; }

    /// <summary>
    /// Sets the type of the Pokémon
    /// </summary>
    public string Type { get; set; }
    
    /// <summary>
    /// A list of all the attacks that the Pokémon has 
    /// </summary>
    public List<IAttack> AtackList { get; }

    /// <summary>
    /// Initial health of the Pokémon.
    /// </summary>
    public int InitialHealth { get; set; }

    /// <inheritdoc />
    public bool IsAsleep { get; private set; }

    /// <inheritdoc />
    public int SleepTurns { get; private set; }

    /// <inheritdoc />
    public bool IsParalyzed { get; private set; }

    /// <inheritdoc />
    public bool IsPoisoned { get; private set; }

    /// <inheritdoc />
    public bool IsBurned { get; private set; }


    /// <summary>
    /// Constructor for the class Pokémon.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="defense"></param>
    /// <param name="type"></param>
    /// <param name="atacks"></param>
    public Pokemon(string name, int defense, string type, List<IAttack> atacks)
    {
        InitialHealth = 100;
        Name = name;
        Health = 100;
        Defense = defense;
        Type = type;
        AtackList = atacks;
    }

    /// <summary>
    /// Returns the attack selected by the player
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public IAttack GetAttack(int index)
    {
        return AtackList[index];
    }

    /// <summary>
    /// Clones the Pokémon so that there can be  multiple instances of the same.
    /// </summary>
    /// <returns></returns>
    public IPokemon Clone()
    {
        return new Pokemon(this.Name, this.Defense, this.Type, this.AtackList);
    }

    /// <summary>
    /// Disminuye la salud del Pokémon por la cantidad de daño recibido.
    /// Si la salud se vuelve negativa, se ajusta a 0.
    /// </summary>
    public void DecreaseHealth(int damage)
    {
        Health -= damage;
        if (Health < 0) Health = 0; // Asegurarse de que la salud no sea negativa.
    }

    /// <summary>
    /// Checks if the Pokémon has any effect applied, if so, it returns, if not, it applies the effect.
    /// </summary>
    /// <param name="effect"></param>
    public void ApplyStatusEffect(SpecialEffect effect)
    {
        if (IsAsleep || IsParalyzed || IsPoisoned || IsBurned) return; // Already affected by a status effect
        switch (effect)
        {
            case SpecialEffect.Sleep:
                IsAsleep = true;
                SleepTurns = new Random().Next(1, 5);
                break;
            case SpecialEffect.Paralyze:
                IsParalyzed = true;
                break;
            case SpecialEffect.Poison:
                IsPoisoned = true;
                break;
            case SpecialEffect.Burn:
                IsBurned = true;
                break;
        }
    }

    /// <summary>
    /// Applies the desired effect. (should it be a method of Pokémon? idk, it processes its own state)
    /// </summary>
    public void ProcessTurnEffects()
    {
        if (IsAsleep)
        {
            SleepTurns--;
            if (SleepTurns <= 0) IsAsleep = false;
        }

        if (IsPoisoned)
        {
            Health -= (int)(0.05 * InitialHealth); // 5% of total HP
        }

        if (IsBurned)
        {
            Health -= (int)(0.10 * InitialHealth); // 10% of total HP
        }
    }

    /// <summary>
    /// Resets the status of the Pokémon. (for ex. when a total cure potion is used)
    /// </summary>
    public void ResetStatus()
    {
        IsAsleep = false;
        IsParalyzed = false;
        IsPoisoned = false;
        IsBurned = false;
    }

    public SpecialEffect GetState()
    {
        return SpecialEffect.Sleep;
    }
    
}