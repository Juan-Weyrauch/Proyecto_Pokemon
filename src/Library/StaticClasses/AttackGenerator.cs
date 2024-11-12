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
        int randomNumber = Rnd.Next(0, 3);
        switch (type)
        {
            case "Plant":
                return CreatePlantAttack(randomNumber);
            case "Water":
                return CreateWaterAttack(randomNumber);
            case "Bug":
                return CreateBugAttack(randomNumber);
            case "Dragon":
                return CreateDragonAttack(randomNumber);
            case "Electric":
                return CreateElectricAttack(randomNumber);
            case "Ghost":
                return CreateGhostAttack(randomNumber);
            case "Fire":
                return CreateFireAttack(randomNumber);
            case "Ice":
                return CreateIceAttack(randomNumber);
            case "Fight":
                return CreateFightAttack(randomNumber);
            case "Normal":
                return CreateNormalAttack(randomNumber);
            case "Psychic":
                return CreatePsychicAttack(randomNumber);
            case "Rock":
                return CreateRockAttack(randomNumber);
            case "Earth":
                return CreateEarthAttack(randomNumber);
            case "Venom":
                return CreateVenomAttack(randomNumber);
            case "Flying":
                return CreateFlyingAttack(randomNumber);
            default:
                return new List<IAttack>(); // Return empty list if type is unrecognized
        }
    }

    private static List<IAttack> CreatePlantAttack(int randomNumber)
    {
        return GenerateAttackList("Vine Whip", "Solar Beam", "Leaf Storm");
    }

    private static List<IAttack> CreateWaterAttack(int randomNumber)
    {
        return GenerateAttackList("Water Gun", "Hydro Pump", "Aqua Tail");
    }

    private static List<IAttack> CreateBugAttack(int randomNumber)
    {
        return GenerateAttackList("Bug Bite", "Signal Beam", "X-Scissor");
    }

    private static List<IAttack> CreateDragonAttack(int randomNumber)
    {
        return GenerateAttackList("Dragon Claw", "Dragon Tail", "Draco Meteor");
    }

    private static List<IAttack> CreateElectricAttack(int randomNumber)
    {
        return GenerateAttackList("Thunder Shock", "Spark", "Thunderbolt");
    }

    private static List<IAttack> CreateGhostAttack(int randomNumber)
    {
        return GenerateAttackList("Shadow Ball", "Night Shade", "Hex");
    }

    private static List<IAttack> CreateFireAttack(int randomNumber)
    {
        return GenerateAttackList("Ember", "Flamethrower", "Fire Blast");
    }

    private static List<IAttack> CreateIceAttack(int randomNumber)
    {
        return GenerateAttackList("Ice Shard", "Blizzard", "Frost Breath");
    }

    private static List<IAttack> CreateFightAttack(int randomNumber)
    {
        return GenerateAttackList("Karate Chop", "Dynamic Punch", "Close Combat");
    }

    private static List<IAttack> CreateNormalAttack(int randomNumber)
    {
        return GenerateAttackList("Tackle", "Body Slam", "Hyper Beam");
    }

    private static List<IAttack> CreatePsychicAttack(int randomNumber)
    {
        return GenerateAttackList("Psybeam", "Psychic", "Future Sight");
    }

    private static List<IAttack> CreateRockAttack(int randomNumber)
    {
        return GenerateAttackList("Rock Throw", "Rock Slide", "Stone Edge");
    }

    private static List<IAttack> CreateEarthAttack(int randomNumber)
    {
        return GenerateAttackList("Mud-Slap", "Earthquake", "Bulldoze");
    }

    private static List<IAttack> CreateVenomAttack(int randomNumber)
    {
        return GenerateAttackList("Poison Sting", "Sludge Bomb", "Venom Shock");
    }

    private static List<IAttack> CreateFlyingAttack(int randomNumber)
    {
        return GenerateAttackList( "Wing Attack", "Aerial Ace", "Sky Attack");
    }

    /// <summary>
    /// Helper method to create an attack list based on the type and a random number.
    /// </summary>
    private static List<IAttack> GenerateAttackList(string attack1, string attack2, string attack3)
    {
        return new List<IAttack>
        {
            new Attack(attack1, Rnd.Next(10, 35), Rnd.Next(0, 4)),
            new Attack(attack2, Rnd.Next(10, 35), Rnd.Next(0, 4)),
            new Attack(attack3, Rnd.Next(10, 35), Rnd.Next(0, 4))
        };
    }
}