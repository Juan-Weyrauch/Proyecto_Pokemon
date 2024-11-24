

using Library.Game.Attacks;

namespace Library.Game.Pokemons
{
    /// <summary>
    /// Represents a Pokémon with various attributes like health, defense, type, status, and attacks.
    /// </summary>
    public class Pokemon : IPokemon
    {
        /// <summary>
        /// Gets or sets the name of the Pokémon.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the health of the Pokémon.
        /// </summary>
        public int Health { get; set; }

        /// <summary>
        /// Gets or sets the defense value of the Pokémon.
        /// </summary>
        public int Defense { get; set; }

        /// <summary>
        /// Gets or sets the type of the Pokémon (e.g., Fire, Water).
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the state of the Pokémon (e.g., Normal, Burned, Poisoned, etc.).
        /// </summary>
        public Estado State { get; set; }

        /// <summary>
        /// Gets or sets the number of turns the Pokémon remains asleep.
        /// </summary>
        public int TurnosDormido { get; set; }

        /// <summary>
        /// Gets the list of all the attacks that the Pokémon has.
        /// </summary>
        public List<IAttack> Attacks { get; }

        /// <summary>
        /// Gets or sets the initial health of the Pokémon.
        /// </summary>
        public int InitialHealth { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the Pokémon is currently in battle.
        /// </summary>
        public bool EnBatalla { get; set; }

        /// <summary>
        /// Initializes a new instance of the Pokémon class with the given attributes.
        /// </summary>
        /// <param name="name">The name of the Pokémon.</param>
        /// <param name="defense">The defense value of the Pokémon.</param>
        /// <param name="type">The type of the Pokémon.</param>
        /// <param name="attacks">The list of attacks that the Pokémon has.</param>
        public Pokemon(string name, int defense, string type, List<IAttack> attacks)
        {
            InitialHealth = 100;
            Name = name;
            Health = 100;
            Defense = defense;
            Type = type;
            Attacks = attacks;
            TurnosDormido = 0;
            EnBatalla = false;
        }

        /// <summary>
        /// Creates a deep clone of the Pokémon, including cloned attacks.
        /// </summary>
        /// <returns>A new instance of the <see cref="IPokemon"/> with the same properties as the original.</returns>
        public IPokemon Clone()
        {
            return new Pokemon(Name, Defense, Type, Attacks.Select(attack => attack.Clone()).ToList())
            {
                Health = this.Health,
                InitialHealth = this.InitialHealth,
                State = this.State,
                TurnosDormido = this.TurnosDormido,
                EnBatalla = this.EnBatalla
            };
        }

        /// <summary>
        /// Returns the attack at the specified index from the Pokémon's attack list.
        /// </summary>
        /// <param name="index">The index of the attack in the list.</param>
        /// <returns>The <see cref="IAttack"/> at the specified index.</returns>
        public IAttack GetAttack(int index)
        {
            return Attacks[index];
        }

        /// <summary>
        /// Decreases the Pokémon's health by the specified amount after calculation.
        /// If the health drops below 0, it is set to 0.
        /// </summary>
        /// <param name="valueAfterCalculation">The amount by which to decrease the health.</param>
        public void DecreaseHealth(int valueAfterCalculation)
        {
            Health -= valueAfterCalculation;
            if (Health < 0) Health = 0;
        }

        /// <summary>
        /// Changes the Pokémon's state to the specified new state.
        /// </summary>
        /// <param name="nuevoEstado">The new state to apply to the Pokémon.</param>
        public void CambiarEstado(Estado nuevoEstado)
        {
            State = nuevoEstado;
        }
    }

    /// <summary>
    /// Enum representing different states of a Pokémon.
    /// </summary>
    public enum Estado
    {
        Normal = 0,
        Quemado = 1,
        Envenenado = 2,
        Paralizado = 3,
        Dormido = 4
    }
}


