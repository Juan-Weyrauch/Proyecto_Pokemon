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
            attacks = attacks.GetRange(0, 3);

            // Generar un ataque aleatorio de otro tipo y agregarlo a la lista
            IAttack randomAttack = GetRandomAttackOfOtherType(type);
            attacks.Add(randomAttack);

            // Devuelve la lista completa con 4 ataques
            return attacks;
        }

        // Método para obtener un ataque aleatorio de otro tipo distinto al tipo del Pokémon
        private static IAttack GetRandomAttackOfOtherType(string type)
        {
            string randomType;
            do
            {
                randomType = GenerateRandomType();
            } while (randomType == type);  // Asegura que el tipo no sea el mismo que el del Pokémon

            List<IAttack> randomTypeAttacks = GetAttacksByType(randomType);
            return randomTypeAttacks[Rnd.Next(randomTypeAttacks.Count)];  // Selecciona un ataque aleatorio del tipo aleatorio
        }

        // Método para obtener ataques por tipo
        private static List<IAttack> GetAttacksByType(string type)
        {
            var attacksByType = new Dictionary<string, List<IAttack>>()
{
   { "Plant", [
        new Attack("Vine Whip", 50, 0, "Plant"),
        new Attack("Solar Beam", 60, 0, "Plant"),
        new Attack("Leaf Storm", 65, 0, "Plant")
    ]
},
{ "Water", [
        new Attack("Water Gun", 50, 0, "Water"),
        new Attack("Hydro Pump", 60, 0, "Water"),
        new Attack("Aqua Tail", 65, 0, "Water")
    ]
},
{ "Bug", [
        new Attack("Bug Bite", 50, 0, "Bug"),
        new Attack("Signal Beam", 60, 3, "Bug"), // Paraliza
        new Attack("X-Scissor", 65, 0, "Bug")
    ]
},
{ "Dragon", [
        new Attack("Dragon Claw", 50, 0, "Dragon"),
        new Attack("Dragon Tail", 60, 0, "Dragon"),
        new Attack("Draco Meteor", 65, 0, "Dragon")
    ]
},
{ "Electric", [
        new Attack("Thunder Shock", 50, 3, "Electric"), // Paraliza
        new Attack("Spark", 60, 3, "Electric"), // Paraliza
        new Attack("Thunderbolt", 65, 3, "Electric")
    ]
},
{ "Ghost", [
        new Attack("Shadow Ball", 50, 0, "Ghost"),
        new Attack("Night Shade", 60, 0, "Ghost"),
        new Attack("Hex", 65, 0, "Ghost")
    ]
},
{ "Fire", [
        new Attack("Ember", 50, 1, "Fire"), // Quema
        new Attack("Flamethrower", 60, 1, "Fire"), // Quema
        new Attack("Fire Blast", 65, 1, "Fire")
    ]
},
{ "Ice", [
        new Attack("Ice Shard", 50, 0, "Ice"),
        new Attack("Blizzard", 60, 0, "Ice"),
        new Attack("Frost Breath", 65, 0, "Ice")
    ]
},
{ "Fight", [
        new Attack("Karate Chop", 50, 0, "Fight"),
        new Attack("Dynamic Punch", 60, 5, "Fight"), // Confunde
        new Attack("Close Combat", 65, 0, "Fight")
    ]
},
{ "Normal", [
        new Attack("Tackle", 50, 0, "Normal"),
        new Attack("Body Slam", 60, 4, "Normal"), // Duerme
        new Attack("Hyper Beam", 65, 0, "Normal")
    ]
},
{ "Psychic", [
        new Attack("Psybeam", 50, 0, "Psychic"),
        new Attack("Psychic", 60, 0, "Psychic"),
        new Attack("Future Sight", 65, 0, "Psychic")
    ]
},
{ "Rock", [
        new Attack("Rock Throw", 50, 0, "Rock"),
        new Attack("Rock Slide", 60, 0, "Rock"),
        new Attack("Stone Edge", 65, 0, "Rock")
    ]
},
{ "Earth", [
        new Attack("Mud-Slap", 50, 0, "Earth"),
        new Attack("Earthquake", 60, 0, "Earth"),
        new Attack("Bulldoze", 65, 0, "Earth")
    ]
},
{ "Venom", [
        new Attack("Poison Sting", 50, 2, "Venom"), // Envenena
        new Attack("Sludge Bomb", 60, 2, "Venom"), // Envenena
        new Attack("Venom Shock", 65, 2, "Venom")
    ]
},
{ "Flying", [
        new Attack("Wing Attack", 50, 0, "Flying"),
        new Attack("Aerial Ace", 60, 0, "Flying"),
        new Attack("Sky Attack", 65, 0, "Flying")
    ]
}

        

};


            if (!attacksByType.TryGetValue(type, out List<IAttack> value))
                throw new ArgumentException("Tipo de Pokémon no reconocido.");
                
            return value;
        }

        // Método para generar un tipo aleatorio
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
