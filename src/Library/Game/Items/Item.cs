using Library.Game.Pokemons;

namespace Library.Game.Items;



/// <summary>
/// Class abstract that let us create the items using inheritance. 
/// </summary>
public abstract class Item
{
    /// <summary>
    /// Constructor.
    /// </summary>
    protected Item()
    {
    }

    /// <summary>
    /// El nombre del item.
    /// </summary>

    public string Name { get; protected set; } // The setter is protected, so it can be set in derived classes.

   
    /// <summary>
    /// Aplica el efecto del objeto en el Pokémon especificado.
    /// </summary>
    /// <param name="pokemon">El Pokémon en el que se usará el objeto.</param>
    public abstract void Use(IPokemon pokemon);
}