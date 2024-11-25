using Library.Game.Players;
using Library.Game.Pokemons;

namespace Library.Game.Items;

/// <summary>
/// This class Creates a potion that allows the player to cure the chosen Pokémon to 100% of HP.
/// </summary>
public class TotalCure : Item
{
    
    
    /// <summary>
    /// This is the constructor of the class that let us cure a Pokémon 
    /// </summary>
    public TotalCure()
    {
        Name = "Total Cure";
    }

    /// <summary>
    /// The function to use the Pokémon and use the total cure potion.
    /// </summary>
    public override void Use(IPlayer player, int index)
    {
        if (player != null)
        {
            IPokemon pokemon = player.Pokemons[index];
            pokemon?.ResetStatus();
        }
    }
}