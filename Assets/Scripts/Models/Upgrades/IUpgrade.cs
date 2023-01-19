public interface IUpgrade
{
    string GetUpgradeId();
    long GetUpgrade();
    long AddUpgrade(long addition);
    long IncrementUpgrade();
    long SetUpgrade(long amount);
}
