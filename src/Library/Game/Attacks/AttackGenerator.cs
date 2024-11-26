using Library.Game.Pokemons;

namespace Library.Game.Attacks
{
    /// <summary>
    /// This static class is responsible for creating a list of attacks depending on the Pokémon type.
    /// </summary>
    public static class AttackGenerator
    {
        private static readonly Random Rnd = new Random();

        /// <summary>
        /// Generates a random list of attacks for the specified Pokémon type.
        /// </summary>
        /// <param name="type">The type of Pokémon.</param>
        /// <returns>A list of attacks: three of the Pokémon's type and one random attack from another type.</returns>
        public static List<IAttack> GenerateRandomAttack(string type)
        {
            // Genera los tres ataques del tipo del Pokémon
            List<IAttack> attacks = GetAttacksByType(type);

            // Asegura que la lista tenga exactamente 3 ataques
            attacks = attacks.GetRange(0, Math.Min(3, attacks.Count));

            // Generar un ataque aleatorio de otro tipo y agregarlo a la lista
            IAttack randomAttack = GetRandomAttackOfOtherType(type);
            attacks.Add(randomAttack);

            // Devuelve la lista completa con 4 ataques
            return attacks;
        }

        /// <summary>
        /// Gets a random attack from a type different from the specified one.
        /// </summary>
        /// <param name="type">The type to exclude.</param>
        /// <returns>A random attack from a different type.</returns>
        private static IAttack GetRandomAttackOfOtherType(string type)
        {
            string randomType;
            do
            {
                randomType = GenerateRandomType();
            } while (randomType == type); // Asegura que el tipo no sea el mismo que el del Pokémon

            List<IAttack> randomTypeAttacks = GetAttacksByType(randomType);
            return randomTypeAttacks
                [Rnd.Next(randomTypeAttacks.Count)]; // Selecciona un ataque aleatorio del tipo aleatorio
        }

        /// <summary>
        /// Retrieves a list of attacks by type.
        /// </summary>
        /// <param name="type">The type of Pokémon.</param>
        /// <returns>A list of attacks for the given type.</returns>
        private static List<IAttack> GetAttacksByType(string type)
        {
            var attacksByType = new Dictionary<string, List<IAttack>>()
            {
                {
                    "Plant", [
                        new Attack("Vine Whip", 10, SpecialEffect.None, "Plant", 100),
                        new Attack("Solar Beam", 35, SpecialEffect.None, "Plant", 95),
                        new Attack("Leaf Storm", 60, SpecialEffect.None, "Plant", 90)
                    ]
                },
                {
                    "Water", [
                        new Attack("Water Gun", 10, SpecialEffect.None, "Water", 100),
                        new Attack("Hydro Pump", 35, SpecialEffect.None, "Water", 95),
                        new Attack("Aqua Tail", 60, SpecialEffect.Sleep, "Water", 90)
                    ]
                },
                {
                    "Bug", [
                        new Attack("Bug Bite", 10, SpecialEffect.None, "Bug", 100),
                        new Attack("Signal Beam", 35, SpecialEffect.None, "Bug", 90),
                        new Attack("X-Scissor", 60, SpecialEffect.None, "Bug", 90)
                    ]
                },
                {
                    "Dragon", [
                        new Attack("Dragon Claw", 10, SpecialEffect.None, "Dragon", 100),
                        new Attack("Dragon Tail", 35, SpecialEffect.None, "Dragon", 95),
                        new Attack("Draco Meteor", 60, SpecialEffect.Burn, "Dragon", 90)
                    ]
                },
                {
                    "Electric", [
                        new Attack("Thunder Shock", 10, SpecialEffect.None, "Electric", 95),
                        new Attack("Spark", 35, SpecialEffect.None, "Electric", 95),
                        new Attack("Thunderbolt", 60, SpecialEffect.Paralyze, "Electric", 90)
                    ]
                },
                {
                    "Ghost", [
                        new Attack("Shadow Ball", 10, SpecialEffect.None, "Ghost", 100),
                        new Attack("Night Shade", 35, SpecialEffect.None, "Ghost", 95),
                        new Attack("Hex", 60, SpecialEffect.Poison, "Ghost", 90)
                    ]
                },
                {
                    "Fire", [
                        new Attack("Ember", 10, SpecialEffect.None, "Fire", 95),
                        new Attack("Flamethrower", 35, SpecialEffect.None, "Fire", 90),
                        new Attack("Fire Blast", 60, SpecialEffect.Burn, "Fire", 85)
                    ]
                },
                {
                    "Ice", [
                        new Attack("Ice Shard", 10, SpecialEffect.None, "Ice", 90),
                        new Attack("Blizzard", 35, SpecialEffect.None, "Ice", 95),
                        new Attack("Frost Breath", 60, SpecialEffect.Poison, "Ice", 90)
                    ]
                },
                {
                    "Fight", [
                        new Attack("Karate Chop", 10, SpecialEffect.None, "Fight", 100),
                        new Attack("Dynamic Punch", 35, SpecialEffect.None, "Fight", 90),
                        new Attack("Close Combat", 60, SpecialEffect.Sleep, "Fight", 90)
                    ]
                },
                {
                    "Normal", [
                        new Attack("Tackle", 10, SpecialEffect.None, "Normal", 100),
                        new Attack("Body Slam", 35, SpecialEffect.None, "Normal", 95),
                        new Attack("Hyper Beam", 60, SpecialEffect.Sleep, "Normal", 90)
                    ]
                },
                {
                    "Psychic", [
                        new Attack("Psybeam", 10, SpecialEffect.None, "Psychic", 100),
                        new Attack("Psychic", 35, SpecialEffect.None, "Psychic", 95),
                        new Attack("Future Sight", 60, SpecialEffect.Sleep, "Psychic", 90)
                    ]
                },
                {
                    "Rock", [
                        new Attack("Rock Throw", 10, SpecialEffect.None, "Rock", 100),
                        new Attack("Rock Slide", 35, SpecialEffect.None, "Rock", 95),
                        new Attack("Stone Edge", 60, SpecialEffect.Sleep, "Rock", 90)
                    ]
                },
                {
                    "Earth", [
                        new Attack("Mud-Slap", 10, SpecialEffect.None, "Earth", 100),
                        new Attack("Earthquake", 35, SpecialEffect.None, "Earth", 95),
                        new Attack("Bulldoze", 60, SpecialEffect.None, "Earth", 90)
                    ]
                },
                {
                    "Venom", [
                        new Attack("Poison Sting", 10, SpecialEffect.None, "Venom", 90),
                        new Attack("Sludge Bomb", 35, SpecialEffect.None, "Venom", 90),
                        new Attack("Venom Shock", 60, SpecialEffect.Poison, "Venom", 90)
                    ]
                },
                {
                    "Flying", [
                        new Attack("Wing Attack", 10, SpecialEffect.None, "Flying", 100),
                        new Attack("Aerial Ace", 35, SpecialEffect.None, "Flying", 95),
                        new Attack("Sky Attack", 60, SpecialEffect.Sleep, "Flying", 90)
                    ]
                }
            };

            return attacksByType[type];
        }


        /// <summary>
        /// Generates a random Pokémon type.
        /// </summary>
        /// <returns>A random type as a string.</returns>
        private static string GenerateRandomType()
        {
            var types = new List<string>
            {
                "Plant", "Water", "Bug", "Dragon", "Electric", "Ghost",
                "Fire", "Ice", "Fight", "Normal", "Psychic", "Rock",
                "Earth", "Venom", "Flying"
            };
            return types[Rnd.Next(types.Count)];
        }
    }
}