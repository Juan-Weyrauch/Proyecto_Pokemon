using Library.Game.Items;
using Library.Game.Pokemons;


namespace Library.Game.Players
{
    /// <inheritdoc />
    public class Player : IPlayer
    {
        // Static fields for singleton instances
        private static Player _player1;
        private static Player _player2;

        /// <summary>
        /// Property to access Player1 singleton instance.
        /// Throws an exception if Player1 has not been initialized.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when Player1 has not been initialized.</exception>
        public static Player Player1
        {
            get
            {
                if (_player1 == null)
                {
                    throw new InvalidOperationException("Player1 has not been initialized.");
                }

                return _player1;
            }
        }

        /// <summary>
        /// Property to access Player2 singleton instance.
        /// Throws an exception if Player2 has not been initialized.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when Player2 has not been initialized.</exception>
        public static Player Player2
        {
            get
            {
                if (_player2 == null)
                {
                    throw new InvalidOperationException("Player2 has not been initialized.");
                }

                return _player2;
            }
        }

        // Public properties
        /// <summary>
        /// The name of the player.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The list of Pokémon owned by the player.
        /// </summary>
        public List<IPokemon> Pokemons { get; private set; }

        /// <summary>
        /// The list of Pokémon that are out of battle (dead or fainted).
        /// </summary>
        public List<IPokemon> Cementerio { get; private set; }

        /// <summary>
        /// The list of items the player possesses (potions, revival items, etc.).
        /// </summary>
        public List<List<Item>> Items { get; private set; }

        /// <summary>
        /// The currently selected Pokémon for the player.
        /// </summary>
        public IPokemon SelectedPokemon { get; private set; }

        /// <summary>
        /// The current turn of the player, used for attack cooldown.
        /// </summary>
        public int Turn { get; private set; }

        // Private constructor to initialize a player
        /// <summary>
        /// Private constructor for creating a new Player instance.
        /// </summary>
        /// <param name="name">The name of the player.</param>
        /// <param name="pokemons">The list of Pokémon the player owns.</param>
        /// <param name="selectedPokemon">The Pokémon the player selects to fight with.</param>
        private Player(string name, List<IPokemon> pokemons, IPokemon selectedPokemon)
        {
            Name = name;
            Pokemons = pokemons;
            Cementerio = new List<IPokemon>();

            // Initialize the Items list with some predefined items
            this.Items = new List<List<Item>>
            {
                new List<Item> { new RevivePotion() },
                new List<Item> { new TotalCure(), new TotalCure() },
                new List<Item> { new SuperPotion(), new SuperPotion(), new SuperPotion() }
            };

            SelectedPokemon = selectedPokemon;
            Turn = 0;
        }

        // Initialization methods for the players
        /// <summary>
        /// Initializes Player1 with the given name, Pokémon, and selected Pokémon.
        /// </summary>
        /// <param name="name">The name of Player1.</param>
        /// <param name="pokemons">The list of Pokémon Player1 owns.</param>
        /// <param name="selectedPokemon">The Pokémon Player1 selects to fight with.</param>
        /// <exception cref="InvalidOperationException">Thrown if Player1 is already initialized.</exception>
        public static void InitializePlayer1(string name, List<IPokemon> pokemons, IPokemon selectedPokemon)
        {
            if (_player1 == null)
            {
                _player1 = new Player(name, pokemons, selectedPokemon);
            }
            else
            {
                throw new InvalidOperationException("Player1 has already been initialized.");
            }
        }

        /// <summary>
        /// Initializes Player2 with the given name, Pokémon, and selected Pokémon.
        /// </summary>
        /// <param name="name">The name of Player2.</param>
        /// <param name="pokemons">The list of Pokémon Player2 owns.</param>
        /// <param name="selectedPokemon">The Pokémon Player2 selects to fight with.</param>
        /// <exception cref="InvalidOperationException">Thrown if Player2 is already initialized.</exception>
        public static void InitializePlayer2(string name, List<IPokemon> pokemons, IPokemon selectedPokemon)
        {
            if (_player2 == null)
            {
                _player2 = new Player(name, pokemons, selectedPokemon);
            }
            else
            {
                throw new InvalidOperationException("Player2 has already been initialized.");
            }
        }

        // Player action methods

        /// <summary>
        /// Switches the player's selected Pokémon based on the player's choice.
        /// </summary>
        /// <param name="pokemonChoice">The index of the Pokémon selected by the player.</param>
        public void SwitchPokemon(int pokemonChoice)
        {
            SelectedPokemon = Pokemons[pokemonChoice];
        }

        /// <summary>
        /// Moves the currently selected Pokémon to the cemetery list and removes it from the player's active Pokémon list.
        /// </summary>
        public void CarryToCementerio()
        {
            if (!Cementerio.Contains(SelectedPokemon))
            {
                Cementerio.Add(SelectedPokemon);
                Pokemons.Remove(SelectedPokemon);
            }
        }

        public void CarryToTeam(IPokemon pokemon)
        {
            if (!Pokemons.Contains(pokemon))
            {
                Pokemons.Add(pokemon);
                Cementerio.Remove(pokemon);
            }
        }

        /// <summary>
        /// Retrieves an item from the player's inventory based on the given index.
        /// </summary>
        /// <param name="itemListIndex">The index of the item in the player's inventory list.</param>
        /// <returns>The item selected by the player.</returns>
        public Item GetItem(int itemListIndex)
        {
            itemListIndex--; // Adjust for 1-based indexing
            Item item = Items[itemListIndex].FirstOrDefault();

            // Remove the item from the player's list and return it
            return item;
        }

        /// <summary>
        /// Removes an item from the player's inventory list based on the given index.
        /// </summary>
        /// <param name="itemListIndex">The index of the item to remove from the inventory.</param>
        public void RemoveItem(int itemListIndex)
        {
            itemListIndex--; // Adjust for 1-based indexing
            Items[itemListIndex].RemoveAt(0);
        }
    }
}
