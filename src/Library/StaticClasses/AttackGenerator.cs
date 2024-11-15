using Library.Classes;
using Library.Interfaces;
using System;
using System.Collections.Generic;

using Library.Classes;
using Library.Interfaces;
using System;
using System.Collections.Generic;

namespace Library.StaticClasses
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
                { "Plant", new List<IAttack> { new Attack("Vine Whip", 10, 35, "Plant"), new Attack("Solar Beam", 35, 60, "Plant"), new Attack("Leaf Storm", 60, 100, "Plant") }},
                { "Water", new List<IAttack> { new Attack("Water Gun", 10, 35, "Water"), new Attack("Hydro Pump", 35, 60, "Water"), new Attack("Aqua Tail", 60, 100, "Water") }},
                { "Bug", new List<IAttack> { new Attack("Bug Bite", 10, 35, "Bug"), new Attack("Signal Beam", 35, 60, "Bug"), new Attack("X-Scissor", 60, 100, "Bug") }},
                { "Dragon", new List<IAttack> { new Attack("Dragon Claw", 10, 35, "Dragon"), new Attack("Dragon Tail", 35, 60, "Dragon"), new Attack("Draco Meteor", 60, 100, "Dragon") }},
                { "Electric", new List<IAttack> { new Attack("Thunder Shock", 10, 35, "Electric"), new Attack("Spark", 35, 60, "Electric"), new Attack("Thunderbolt", 60, 100, "Electric") }},
                { "Ghost", new List<IAttack> { new Attack("Shadow Ball", 10, 35, "Ghost"), new Attack("Night Shade", 35, 60, "Ghost"), new Attack("Hex", 60, 100, "Ghost") }},
                { "Fire", new List<IAttack> { new Attack("Ember", 10, 35, "Fire"), new Attack("Flamethrower", 35, 60, "Fire"), new Attack("Fire Blast", 60, 100, "Fire") }},
                { "Ice", new List<IAttack> { new Attack("Ice Shard", 10, 35, "Ice"), new Attack("Blizzard", 35, 60, "Ice"), new Attack("Frost Breath", 60, 100, "Ice") }},
                { "Fight", new List<IAttack> { new Attack("Karate Chop", 10, 35, "Fight"), new Attack("Dynamic Punch", 35, 60, "Fight"), new Attack("Close Combat", 60, 100, "Fight") }},
                { "Normal", new List<IAttack> { new Attack("Tackle", 10, 35, "Normal"), new Attack("Body Slam", 35, 60, "Normal"), new Attack("Hyper Beam", 60, 100, "Normal") }},
                { "Psychic", new List<IAttack> { new Attack("Psybeam", 10, 35, "Psychic"), new Attack("Psychic", 35, 60, "Psychic"), new Attack("Future Sight", 60, 100, "Psychic") }},
                { "Rock", new List<IAttack> { new Attack("Rock Throw", 10, 35, "Rock"), new Attack("Rock Slide", 35, 60, "Rock"), new Attack("Stone Edge", 60, 100, "Rock") }},
                { "Earth", new List<IAttack> { new Attack("Mud-Slap", 10, 35, "Earth"), new Attack("Earthquake", 35, 60, "Earth"), new Attack("Bulldoze", 60, 100, "Earth") }},
                { "Venom", new List<IAttack> { new Attack("Poison Sting", 10, 35, "Venom"), new Attack("Sludge Bomb", 35, 60, "Venom"), new Attack("Venom Shock", 60, 100, "Venom") }},
                { "Flying", new List<IAttack> { new Attack("Wing Attack", 10, 35, "Flying"), new Attack("Aerial Ace", 35, 60, "Flying"), new Attack("Sky Attack", 60, 100, "Flying") }}
            };

            if (!attacksByType.ContainsKey(type))
                throw new ArgumentException("Tipo de Pokémon no reconocido.");
                
            return attacksByType[type];
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
