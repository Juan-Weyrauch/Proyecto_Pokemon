using Library.Game.Pokemons;

namespace Library.Game.Attacks
{
    /// <summary>
    /// Static class responsible for creating a list of attacks depending on the Pokémon type.
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
            // Generate the three attacks from the Pokémon's type
            List<IAttack> attacks = GetAttacksByType(type);

            // Ensure the list has exactly 3 attacks
            attacks = attacks.GetRange(0, 3);

            // Generate a random attack from another type and add it to the list
            IAttack randomAttack = GetRandomAttackOfOtherType(type);
            attacks.Add(randomAttack);

            // Return the complete list with 4 attacks
            return attacks;
        }

        // Method to get a random attack from a type different from the Pokémon's type
        private static IAttack GetRandomAttackOfOtherType(string type)
        {
            string randomType;
            do
            {
                randomType = GenerateRandomType();
            } while (randomType == type); // Ensure the random type is not the same as the Pokémon's type

            List<IAttack> randomTypeAttacks = GetAttacksByType(randomType);
            return randomTypeAttacks[Rnd.Next(randomTypeAttacks.Count)]; // Select a random attack from the random type
        }

        // Method to get attacks by Pokémon type
        private static List<IAttack> GetAttacksByType(string type)
        {
            var attacksByType = new Dictionary<string, List<IAttack>>()
            {
                {
                    "Plant", new List<IAttack>
                    {
                        new Attack("Vine Whip", 10, Estado.Normal, "Plant"),
                        new Attack("Solar Beam", 35, Estado.Normal, "Plant"),
                        new Attack("Leaf Storm", 60, Estado.Normal, "Plant")
                    }
                },
                {
                    "Water", new List<IAttack>
                    {
                        new Attack("Water Gun", 10, Estado.Normal, "Water"),
                        new Attack("Hydro Pump", 35, Estado.Normal, "Water"),
                        new Attack("Aqua Tail", 60, Estado.Normal, "Water")
                    }
                },
                {
                    "Bug", new List<IAttack>
                    {
                        new Attack("Bug Bite", 10, Estado.Normal, "Bug"),
                        new Attack("Signal Beam", 35, Estado.Paralizado, "Bug"), // Paralizes
                        new Attack("X-Scissor", 60, Estado.Normal, "Bug")
                    }
                },
                {
                    "Dragon", new List<IAttack>
                    {
                        new Attack("Dragon Claw", 10, Estado.Normal, "Dragon"),
                        new Attack("Dragon Tail", 35, Estado.Normal, "Dragon"),
                        new Attack("Draco Meteor", 60, Estado.Normal, "Dragon")
                    }
                },
                {
                    "Electric", new List<IAttack>
                    {
                        new Attack("Thunder Shock", 10, Estado.Paralizado, "Electric"), // Paralizes
                        new Attack("Spark", 35, Estado.Paralizado, "Electric"), // Paralizes
                        new Attack("Thunderbolt", 60, Estado.Paralizado, "Electric")
                    }
                },
                {
                    "Ghost", new List<IAttack>
                    {
                        new Attack("Shadow Ball", 10, Estado.Normal, "Ghost"),
                        new Attack("Night Shade", 35, Estado.Normal, "Ghost"),
                        new Attack("Hex", 60, Estado.Normal, "Ghost")
                    }
                },
                {
                    "Fire", new List<IAttack>
                    {
                        new Attack("Ember", 10, Estado.Quemado, "Fire"), // Burns
                        new Attack("Flamethrower", 35, Estado.Quemado, "Fire"), // Burns
                        new Attack("Fire Blast", 60, Estado.Quemado, "Fire")
                    }
                },
                {
                    "Ice", new List<IAttack>
                    {
                        new Attack("Ice Shard", 10, Estado.Normal, "Ice"),
                        new Attack("Blizzard", 35, Estado.Normal, "Ice"),
                        new Attack("Frost Breath", 60, Estado.Normal, "Ice")
                    }
                },
                {
                    "Fight", new List<IAttack>
                    {
                        new Attack("Karate Chop", 10, Estado.Normal, "Fight"),
                        new Attack("Dynamic Punch", 35, Estado.Normal, "Fight"), // Confuses
                        new Attack("Close Combat", 60, Estado.Normal, "Fight")
                    }
                },
                {
                    "Normal", new List<IAttack>
                    {
                        new Attack("Tackle", 10, Estado.Normal, "Normal"),
                        new Attack("Body Slam", 35, Estado.Dormido, "Normal"), // Puts to sleep
                        new Attack("Hyper Beam", 60, Estado.Normal, "Normal")
                    }
                },
                {
                    "Psychic", new List<IAttack>
                    {
                        new Attack("Psybeam", 10, Estado.Normal, "Psychic"),
                        new Attack("Psychic", 35, Estado.Normal, "Psychic"),
                        new Attack("Future Sight", 60, Estado.Normal, "Psychic")
                    }
                },
                {
                    "Rock", new List<IAttack>
                    {
                        new Attack("Rock Throw", 10, Estado.Normal, "Rock"),
                        new Attack("Rock Slide", 35, Estado.Normal, "Rock"),
                        new Attack("Stone Edge", 60, Estado.Normal, "Rock")
                    }
                },
                {
                    "Earth", new List<IAttack>
                    {
                        new Attack("Mud-Slap", 10, Estado.Normal, "Earth"),
                        new Attack("Earthquake", 35, Estado.Normal, "Earth"),
                        new Attack("Bulldoze", 60, Estado.Normal, "Earth")
                    }
                },
                {
                    "Venom", new List<IAttack>
                    {
                        new Attack("Poison Sting", 10, Estado.Envenenado, "Venom"), // Poisons
                        new Attack("Sludge Bomb", 35, Estado.Envenenado, "Venom"), // Poisons
                        new Attack("Venom Shock", 60, Estado.Envenenado, "Venom")
                    }
                },
                {
                    "Flying", new List<IAttack>
                    {
                        new Attack("Wing Attack", 10, Estado.Normal, "Flying"),
                        new Attack("Aerial Ace", 35, Estado.Normal, "Flying"),
                        new Attack("Sky Attack", 60, Estado.Normal, "Flying")
                    }
                }
            };

            if (!attacksByType.TryGetValue(type, out List<IAttack> value))
                throw new ArgumentException("Tipo de Pokémon no reconocido.");

            return value;
        }

        // Method to generate a random type
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