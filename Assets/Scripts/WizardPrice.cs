using System.Collections.Generic;

public class WizardPrice
{
    private Dictionary<int, int> _price;

    public WizardPrice()
    {
        _price = new Dictionary<int, int>()
        {
            { 2, 20 },
            { 3, 30 },
            { 4, 60 },
            { 5, 180 },
            { 6, 300 },
            { 7, 500 },
            { 8, 800 },
            { 9, 1000 },
            { 10, 1600 },
        };
    }
    
    public int GetPrice(int key) => _price[key];
}