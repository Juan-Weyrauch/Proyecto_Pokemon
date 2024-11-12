using Library.Interfaces;

namespace Library.Classes;

public class SuperPotion : IPotions
{
    public string Name { get; set; }
    public int RegenValue { get; set; }

    public SuperPotion()
    {
        Name = "Super Potion";
        RegenValue = 70;
    }
}