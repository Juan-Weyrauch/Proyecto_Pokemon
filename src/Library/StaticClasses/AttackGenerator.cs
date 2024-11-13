using Library.Classes;
using Library.Interfaces;
using System;
using System.Collections.Generic;

namespace Library.StaticClasses;

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
    /// <returns>A list of attacks specific to the type.</returns>
    public static List<IAttack> GenerateRandomAttack(string type)
    {
        return type switch
        {
            "Plant" => CreatePlantAttack(),
            "Water" => CreateWaterAttack(),
            "Bug" => CreateBugAttack(),
            "Dragon" => CreateDragonAttack(),
            "Electric" => CreateElectricAttack(),
            "Ghost" => CreateGhostAttack(),
            "Fire" => CreateFireAttack(),
            "Ice" => CreateIceAttack(),
            "Fight" => CreateFightAttack(),
            "Normal" => CreateNormalAttack(),
            "Psychic" => CreatePsychicAttack(),
            "Rock" => CreateRockAttack(),
            "Earth" => CreateEarthAttack(),
            "Venom" => CreateVenomAttack(),
            "Flying" => CreateFlyingAttack(),
            _ => new List<IAttack>() // Return empty list if type is unrecognized
        };
    }

    private static List<IAttack> CreatePlantAttack()
    {
        return GenerateAttackList("Vine Whip", "Solar Beam", "Leaf Storm", "Plant");
    }

    private static List<IAttack> CreateWaterAttack()
    {
        return GenerateAttackList("Water Gun", "Hydro Pump", "Aqua Tail", "Water");
    }

    private static List<IAttack> CreateBugAttack()
    {
        return GenerateAttackList("Bug Bite", "Signal Beam", "X-Scissor", "Bug");
    }

    private static List<IAttack> CreateDragonAttack()
    {
        return GenerateAttackList("Dragon Claw", "Dragon Tail", "Draco Meteor", "Dragon");
    }

    private static List<IAttack> CreateElectricAttack()
    {
        return GenerateAttackList("Thunder Shock", "Spark", "Thunderbolt", "Electric");
    }

    private static List<IAttack> CreateGhostAttack()
    {
        return GenerateAttackList("Shadow Ball", "Night Shade", "Hex", "Ghost");
    }

    private static List<IAttack> CreateFireAttack()
    {
        return GenerateAttackList("Ember", "Flamethrower", "Fire Blast", "Fire");
    }

    private static List<IAttack> CreateIceAttack()
    {
        return GenerateAttackList("Ice Shard", "Blizzard", "Frost Breath", "Ice");
    }

    private static List<IAttack> CreateFightAttack()
    {
        return GenerateAttackList("Karate Chop", "Dynamic Punch", "Close Combat", "Fight");
    }

    private static List<IAttack> CreateNormalAttack()
    {
        return GenerateAttackList("Tackle", "Body Slam", "Hyper Beam", "Normal");
    }

    private static List<IAttack> CreatePsychicAttack()
    {
        return GenerateAttackList("Psybeam", "Psychic", "Future Sight", "Psychic");
    }

    private static List<IAttack> CreateRockAttack()
    {
        return GenerateAttackList("Rock Throw", "Rock Slide", "Stone Edge", "Rock");
    }

    private static List<IAttack> CreateEarthAttack()
    {
        return GenerateAttackList("Mud-Slap", "Earthquake", "Bulldoze", "Earth");
    }

    private static List<IAttack> CreateVenomAttack()
    {
        return GenerateAttackList("Poison Sting", "Sludge Bomb", "Venom Shock", "Venom");
    }

    private static List<IAttack> CreateFlyingAttack()
    {
        return GenerateAttackList("Wing Attack", "Aerial Ace", "Sky Attack", "Flying");
    }

    /// <summary>
    /// Helper method to create an attack list based on the type and attack names.
    /// </summary>
    private static List<IAttack> GenerateAttackList(string attack1, string attack2, string attack3, string type)
    {
        return new List<IAttack>
        {
            new Attack(attack1, Rnd.Next(10, 35), Rnd.Next(0, 2), type),
            new Attack(attack2, Rnd.Next(35, 60), Rnd.Next(0, 2), type),
            new Attack(attack3, Rnd.Next(60, 100), Rnd.Next(1, 3), type)
        };
    }
}
