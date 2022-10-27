public class PlayerStats
{
    private int _amountWizards = 3;

    public PlayerStats(int amountWizards)
    {
        _amountWizards = amountWizards;
    }
    
    public int AmountWizards => _amountWizards;
}