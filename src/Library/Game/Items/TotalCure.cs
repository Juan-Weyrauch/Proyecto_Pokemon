using Library.Game.Pokemons;

namespace Library.Game.Items;

/// <summary>
/// This class Creates a potion that allows the player to cure the chosen Pokémon to 100% of HP.
/// </summary>
public class TotalCure : Item
{
    
    
    /// <summary>
    /// This is the constructor of the class that let us cure a pokemon 
    /// </summary>
    public TotalCure()
    {
        Name = "Total Cure";
    }

    
    /// <inheritdoc />


    /// <summary>
    /// The function to use the pokemon and use the total cure potion..
    /// </summary>

    public override void Use(IPokemon pokemon)
    {
        if (pokemon != null)
        {
          
            pokemon.State = 0;
        }
       
    }
    
}