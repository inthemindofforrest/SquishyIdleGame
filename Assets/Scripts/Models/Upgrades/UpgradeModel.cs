public class UpgradeModel : IUpgrade ,IModel
{
    public string ModelId => modelId;
    private string modelId;
    
    private string upgradeId;
    private long upgrade;
    private double multiplier;
    private double baseCost;

    public UpgradeModel(string id, int upgrade, double multiplier, double baseCost)
    {
        upgradeId = id;
        this.upgrade = upgrade;
        this.multiplier = multiplier;
        this.baseCost = baseCost;
    }
    
    public string GetUpgradeId()
    {
        return upgradeId;
    }

    public long GetUpgrade()
    {
        return upgrade;
    }

    public double GetMultiplier()
    {
        return multiplier;
    }

    public long AddUpgrade(long addition)
    {
        upgrade += addition;
        return upgrade;
    }

    public long IncrementUpgrade()
    {
        upgrade++;
        return upgrade;
    }

    public long SetUpgrade(long amount)
    {
        upgrade = amount;
        return upgrade;
    }

    public double SetMultiplier(double amount)
    {
        multiplier = amount;
        return multiplier;
    }

    public double GetCost()
    {
        return baseCost * multiplier * upgrade;
    }
}
