using Library.Game.Items;
using Library.Game.Pokemons;

namespace Library.Game.Players;

/// <summary>
/// Interface for the player class. Argument for this to exist?? null, I think.
/// </summary>
public interface  
{
    /// <summary>
    /// Sets the name of the player
    /// </summary>
    string Name { get; }
    
    
    /// <summary>
    /// A list containing all the players Pokémon.
    /// </summary>
    List<IPokemon> Pokemons { get; }

    /// <summary>
    /// A list of all the Pokémon that are out of battle.
    /// </summary>
    List<IPokemon> Cementerio { get; }



    /// <summary>
    /// A list containing all the player potions.
    /// This list gets updated along the Battle to remove Items that have already been used.
    /// </summary>
    List<Item> Potions { get; }
    
    
    
    
    /// <summary>
    /// IPokemon type to determine the Selected Pokémon of the player.
    /// </summary>
    IPokemon SelectedPokemon { get; } 
    
    
    
    /// <summary>
    /// Integer used for cooldown in the attacks of the Pokémon.
    /// </summary>
    int Turn { get; }
    
    
    
    /// <summary>
    /// Method that lets the user use an Item on the selected Pokémon (or on any, we'll see.) 
    /// </summary>
    /// <param name="itemChoice"></param>
    void UseItem(int itemChoice);


    /// <summary>
    /// Changes the Selected Pokémon for a new IPokemon received.
    /// </summary>
    void SwitchPokemon(int pokemonChoice);

    /// <summary>
    /// changes the dead Pokémon to the cemetery list of dead Pokémon.
    /// </summary>
    public void CarryToCementerio();
}