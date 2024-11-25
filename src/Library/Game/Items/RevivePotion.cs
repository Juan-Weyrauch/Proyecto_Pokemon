using Library.Game.Players;
using Library.Game.Pokemons;
namespace Library.Game.Items

{
    /// <summary>
    /// This is a potion item, it allows the player to revive a dead pokemon.
    /// </summary>
    public class RevivePotion : Item
    {
        /// <summary>
        /// Constructor of revive.
        /// </summary>
        public RevivePotion()
        {
            Name = "Revive"; // The Name property is set here because the setter is protected.
        }

        /// <summary>
        /// Let select the pokemon to utilize the effect of the potion
        /// </summary>
        /// <param name="pokemon"> Pokemon selected by the player</param>
        public override void Use(IPlayer player, int index)
        {
            if (player != null)
            {
                IPokemon pokemon = player.Cementerio[index];
                if (pokemon != null && pokemon.Health == 0) // Only revive if the Pokémon is fainted.
                {
                    pokemon.Health = pokemon.InitialHealth / 2;
                    player.CarryToTeam(pokemon);// Revives the Pokémon with half its initial health.
                }
            }
        }
    }
}