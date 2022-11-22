using System.Collections.Generic;

public class WizardPrice
{
    private Dictionary<int, int> _price;

    public WizardPrice()
    {
        _price = new Dictionary<int, int>()
        {
            { 2, 150 },
            { 3, 300 },
            { 4, 600 },
            { 5, 1200 },
            { 6, 2400 },
            { 7, 4800 },
            { 8, 6000 },
            { 9, 8000 },
            { 10, 8000 },
        };
    }
    
    public int GetPrice(int key) => _price[key];
}