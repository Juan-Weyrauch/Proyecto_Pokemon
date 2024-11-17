namespace Library.Interfaces;

public interface IPlayer
{
    /// <summary>
    /// Sets the name of the player
    /// </summary>
    string Name { get; }
    
    
    /// <summary>
    /// A list containing all the players Pokémon.
    /// </summary>
    List<IPokemon> Pokemons { get; }

    List<IPokemon> Cementerio { get; }



    /// <summary>
    /// A list containing all the players potions.
    /// This list gets updated along the Battle to remove Items that have already been used.
    /// </summary>
    List<IPotions> Potions { get; }
    
    
    
    
    /// <summary>
    /// IPokemon type to determine the Selected Pokémon of the player.
    /// </summary>
    IPokemon SelectedPokemon { get; } 
    
    
    
    /// <summary>
    /// Integer used for cooldown in the attacks of the Pokémon.
    /// </summary>
    int Turn { get; }
    
    
    
    /// <summary>
    /// Methid that lets the user use an Item on the selected Pokémon (or on any, we'll see.) 
    /// </summary>
    /// <param name="itemChoice"></param>
    void UseItem(int itemChoice); 
    
    
    
    /// <summary>
    /// Changes the Selected Pokemon for a new IPokemon recieved 
    /// </summary>
    /// <param name="newPokemonIndex"></param>
    void SwitchPokemon(int pokemonChoice);

    public void CarryToCementerio();
}