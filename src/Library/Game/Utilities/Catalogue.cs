using Library.Game.Pokemons;

namespace Library.Game.Utilities;

/// <summary>
/// This class receives a created Pokémon and adds it to its catalog.
/// </summary>
public static class Catalogue
{
    /// <summary>
    /// Contains all the Pokémon.
    /// </summary>
    private static Dictionary<int, IPokemon> _pokedex = [];

    /// <summary>
    /// Creates the catalog, initializing all Pokémon.
    /// </summary>
    public static void CreateCatalogue()
    {
        _pokedex = Create.CreateCatalogue();
    }

    /// <summary>
    /// Returns the complete catalogue
    /// </summary>
    /// <returns></returns>
    public static Dictionary<int, IPokemon> GetPokedex()
    {
        return _pokedex;
    }

    /// <summary>
    /// Returns the Pokémon that the user picked so that it can be added to its inventory.
    /// </summary>
    /// <param name="pokemonId"></param>
    /// <returns></returns>
    public static IPokemon GetPokemon(int pokemonId)
    {
        return _pokedex[pokemonId];
    }
}


