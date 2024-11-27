using Library.Game.Items;
using Library.Game.Pokemons;

namespace Library.Game.Players
{
    /// <summary>
    /// Interface for the player class. This defines the methods and properties
    /// a player object should implement to interact with Pokémon and battle mechanics.
    /// </summary>
    public interface IPlayer
    {
        /// <summary>
        /// Gets the name of the player.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// A list containing all the player's Pokémon.
        /// </summary>
        List<IPokemon> Pokemons { get; }

        /// <summary>
        /// A list of all the Pokémon that are out of battle (i.e., "dead" or not active).
        /// </summary>
        List<IPokemon> Cementerio { get; }

        /// <summary>
        /// A list containing all the player's items (potions, revival items, etc.).
        /// This list is updated during the battle as items are used or removed.
        /// </summary>
        List<List<Item>> Items { get; }

        /// <summary>
        /// The current selected Pokémon of the player.
        /// </summary>
        IPokemon SelectedPokemon { get; }

        /// <summary>
        /// The current turn of the player, used for attack cooldown.
        /// </summary>
        int Turn { get; }

        
        
        /// <summary>
        /// Changes the selected Pokémon for a new one based on the player's choice.
        /// </summary>
        /// <param name="pokemonChoice">The index of the new Pokémon selected from the player's Pokémon list.</param>
        void SwitchPokemon(int pokemonChoice);

        /// <summary>
        /// Moves the currently "dead" Pokémon to the cemetery list of Pokémon that are no longer in battle.
        /// This method is called when a Pokémon is fainted.
        /// </summary>
        void CarryToCementerio();

        public void CarryToTeam(IPokemon pokemon);

        /// <summary>
        /// Retrieves the item from the player's item list based on the provided index.
        /// </summary>
        /// <param name="itemListIndex">The index of the item in the list to be retrieved.</param>
        /// <returns>The item corresponding to the provided index.</returns>
        Item GetItem(int itemListIndex);

        /// <summary>
        /// Removes the item from the player's item list based on the provided index.
        /// </summary>
        /// <param name="itemListIndex">The index of the item in the list to be removed.</param>
        void RemoveItem(int itemListIndex);

        /// <summary>
        /// Returns the amount of items the player has.
        /// </summary>
        /// <returns></returns>
        int ReturnItemCount();

        /// <summary>
        /// Returns the amount of pokemons the player has.
        /// </summary>
        /// <returns></returns>
        public int ReturnPokemonsCount();

    }
}
