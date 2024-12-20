using Library.Game.Players;
using Library.Game.Pokemons;

namespace Library.Game.Items;
/// <summary>
/// This super potion regenerates 70 points of HP to the Pokémon.
/// </summary>
public class SuperPotion : Item
{

    /// <summary>
    /// Constructor of the Potion
    /// </summary>
    public SuperPotion()
    {
        Name = "Super Potion";
    }
    
   


    /// <summary>
    /// The constructor for the class.
    /// </summary>

    public override void Use(IPlayer player, int index)
    {
        if (player != null)
        {
            IPokemon pokemon = player.Pokemons[index];
            pokemon.Health = pokemon.Health + 70;
            if (pokemon.Health > 100)
            {
                pokemon.Health = 100;
            }
        }
       
    }
    
}