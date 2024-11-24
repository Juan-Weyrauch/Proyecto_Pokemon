using Library.Game.Attacks;
using System.Collections.Generic;

namespace Library.Game.Pokemons
{
    /// <summary>
    /// Interface for Pokémon.
    /// </summary>
    public interface IPokemon
    {
        /// <summary>
        /// Name for Pokémon
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// HP points of the Pokémon
        /// </summary>
        int Health { get; set; }

        /// <summary>
        /// Initial health of the Pokémon
        /// </summary>
        int InitialHealth { get; set; }

        int TurnosDormido { get;  }

    /// <summary>
        /// The Pokémon must have a defense status
        /// </summary>
        int Defense { get; set; }

        /// <summary>
        /// Sets the type of the Pokémon.
        /// </summary>
        string Type { get; set; }

        /// <summary>
        /// Sets the state of the Pokémon.
        /// </summary>
        Estado State { get; set; }

        /// <summary>
        /// A list of all the attacks that the Pokémon has.
        /// </summary>
        List<IAttack> AttackList { get; }

        /// <summary>
        /// Returns the attack depending on the index inputted.
        /// </summary>
        /// <param name="index">The index of the attack in the list.</param>
        /// <returns>The attack at the specified index.</returns>
        IAttack GetAttack(int index);

        /// <summary>
        /// Clones the Pokémon so that there can exist multiple instances of the same.
        /// </summary>
        public void DecreaseHealth(int valueAfterCalculation);
        IPokemon Clone();

        public void DecreaseTurnosDormido();
    }
}