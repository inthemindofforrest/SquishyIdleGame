using System;
using System.Globalization;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AutoCollectors : MonoBehaviour, IAutoCollector
{
    public GeneratorSO SO;
    public AutoCollectors Reliance;
    
    public GameObject Contents;

    public Slider TimeForCollection;
    public Button BuyButton;
    public TMP_Text BuyText;
    
    public int BaseCollector => baseCollector;
    public int UpgradeMultiplier => upgradeMultiplier;
    public int AmountCanCollect => amountCanCollect;
    public bool CanStackAmount => canStackAmount;
    public bool IsCollecorAutomated => isCollectorAutomated;
    public float CanCollectAgain => canCollectAgain;
    public int TimeForRecollection => timeForRecollection;
    public bool CollectorActive => collectorActive;
    public long CostToBuyCollector => costToBuyCollector;

    private int baseCollector;
    private int upgradeMultiplier;
    private int amountCanCollect = 0;
    private bool canStackAmount;
    private bool isCollectorAutomated;
    private float canCollectAgain;
    private int timeForRecollection;
    private bool collectorActive;
    private long costToBuyCollector;

    private CurrencyService currencyService;
    private bool currencyServiceInitialized;

    private string currencyType = "SC";

    private async void Start()
    {
        currencyService = await ServiceController.GetService<CurrencyService>() as CurrencyService;
        PullSavedData();
        currencyServiceInitialized = true;
    }

    private async void Update()
    {
        if (currencyService == null)
        {
            return;
        }

        if (Reliance == null || (Reliance != null && Reliance.collectorActive))
        {
            if (collectorActive)
            {
                if (Time.time - canCollectAgain > timeForRecollection &&
                    ((amountCanCollect > 0 && canStackAmount) || amountCanCollect == 0))
                {
                    AddToCollector();
                    CollectCurrency();
                    canCollectAgain = Time.time;
                }

                TimeForCollection.value = timeForRecollection - (timeForRecollection - (Time.time - canCollectAgain));
            }
            else
            {
                await ServiceController.WaitForInitialized();
                BuyButton.gameObject.SetActive(true);
                if (currencyService.GetCurrencyModel("SC").GetCurrency() >= costToBuyCollector)
                {
                    BuyButton.enabled = true;
                }
                else
                {
                    BuyButton.enabled = false;
                    
                }
            }
        }
        else
        {
            BuyButton.gameObject.SetActive(false);
        }
        
        
    }

    public async Task<int> CollectCurrency()
    {
        if (currencyServiceInitialized == false)
        {
            return 0;
        }

        currencyService.AddToCurrencyModel(currencyType, AmountCanCollect);
        int collected = amountCanCollect;
        amountCanCollect = 0;
        return collected;
    }

    public int UpgradeCollector()
    {
        upgradeMultiplier++;
        return upgradeMultiplier;
    }

    public bool AutomateCollector()
    {
        isCollectorAutomated = true;
        return isCollectorAutomated;
    }

    public int AddToCollector()
    {
        amountCanCollect += baseCollector * upgradeMultiplier;
        return amountCanCollect;
    }

    public void PullSavedData()
    {
        MindfulDebugger.DebugMessage("AutoCollectors not pulling data correctly",
            MindfulDebugger.DEBUGURGENCY.HIGH);
        isCollectorAutomated = true;
        baseCollector = SO.baseCollector;
        upgradeMultiplier = 1;
        timeForRecollection = SO.timeForRecollection;
        TimeForCollection.maxValue = timeForRecollection;
        collectorActive = false;
        canStackAmount = SO.canStackAmount;
        costToBuyCollector = SO.costToBuyCollector;
        BuyText.text = $"${costToBuyCollector}";
    }

    public void BuyCollector()
    {
        currencyService.RemoveFromCurrencyModel("SC", -1 * costToBuyCollector);
        collectorActive = true;
        BuyButton.gameObject.SetActive(false);
        Contents.SetActive(true);
        canCollectAgain = Time.time;
    }
}
