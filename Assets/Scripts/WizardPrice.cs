using System.Collections.Generic;

public class WizardPrice
{
    private Dictionary<int, int> _price;

    public WizardPrice()
    {
        _price = new Dictionary<int, int>()
        {
            { 2, 20 },
            { 3, 60 },
            { 4, 130 },
            { 5, 200 },
            { 6, 400 },
            { 7, 700 },
            { 8, 1200 },
            { 9, 2000 },
            { 10, 3000 },
        };
    }

    public int GetPrice(int key) => _price[key];
}