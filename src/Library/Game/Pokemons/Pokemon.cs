using Library.Game.Attacks;
using Library.Game.Utilities;

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
    public SpecialEffect State { get; set; }

    /// <inheritdoc />
    public int SleepTurns { get; private set; }
    


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
        State = SpecialEffect.None;
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
        if (State != SpecialEffect.None) return; // Already affected by a status
        State = effect;

        switch (effect)
        {
            case SpecialEffect.Sleep:
                SleepTurns = new Random().Next(1, 5);
                break;
            case SpecialEffect.Poison:
            case SpecialEffect.Burn:
            case SpecialEffect.Paralyze:
                // Apply other effects
                break;
        }
    }

    /// <summary>
    /// Applies the desired effect. (should it be a method of Pokémon? idk, it processes its own state)
    /// </summary>
    public void ProcessTurnEffects()
    {
        switch (State)
        {
            case SpecialEffect.Sleep:
                SleepTurns--;
                if (SleepTurns <= 0) State = SpecialEffect.None;
                break;
            case SpecialEffect.Poison:
                int auxHealth = Health;
                Health -= (int)(0.05 * InitialHealth);
                if (Health < 0) Health = 0;
                auxHealth -= Health;
                
                Printer.DisplayEffect(this.Name, this.State, auxHealth);
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                break;
            case SpecialEffect.Burn:
                auxHealth = Health;
                Health -= (int)(0.10 * InitialHealth);
                if (Health < 0) Health = 0;
                auxHealth -= Health;

                Printer.DisplayEffect(this.Name, this.State, auxHealth);
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                break;
            // Add cases for other states if needed
        }
    }

    /// <summary>
    /// Resets the status of the Pokémon. (for ex. when a total cure potion is used)
    /// </summary>
    public void ResetStatus()
    {
        State = SpecialEffect.None;
        SleepTurns = 0;
    }
}