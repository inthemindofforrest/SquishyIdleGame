using System.Threading.Tasks;
using UnityEngine;

public interface IAutoCollector
{
    int BaseCollector { get; }
    int UpgradeMultiplier { get; }
    int AmountCanCollect { get; }
    bool CanStackAmount { get; }
    bool IsCollecorAutomated { get; }
    float CanCollectAgain { get; }
    int TimeForRecollection { get; }
    bool CollectorActive { get; }
    long CostToBuyCollector { get; }

    Task<int> CollectCurrency();
    int UpgradeCollector();
    bool AutomateCollector();
    int AddToCollector();
    void PullSavedData();
    void BuyCollector();
}