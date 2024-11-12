using Library.Interfaces;

namespace Library.Classes;

public class RevivePotion : IPotions
{
    public string Name { get; set; }
    public int RegenValue { get; set; }

    public RevivePotion()
    {
        Name = "Revive Potion";
        RegenValue = 50;
    }
    
}