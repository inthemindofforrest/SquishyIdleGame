using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class CurrencyService : IService
{
    private StatisticsService statisticsService;
    private Dictionary<string, ICurrency> currencies = new Dictionary<string, ICurrency>();
    public async Task Initialize()
    {
        GetDataFromServer();
    }

    public void Update()
    {
        
    }

    public void Injection(Dictionary<Type, IService> services)
    {
        statisticsService = services[typeof(StatisticsService)] as StatisticsService;
    }

    public ICurrency GetCurrencyModel(string id)
    {
        if (currencies.ContainsKey(id))
        {
            return currencies[id];
        }
        
        MindfulDebugger.DebugError("No currency with the ID: " + id,
            MindfulDebugger.DEBUGURGENCY.HIGH);
        return new CurrencyModel("", 0);
    }

    public ICurrency AddToCurrencyModel(string id, long amount)
    {
        if (currencies.ContainsKey(id))
        {
            currencies[id].AddCurrency(amount);
            statisticsService.MassCurrencyUpdate(amount);
            return currencies[id];
        }
        
        MindfulDebugger.DebugError("No currency with the ID: " + id,
            MindfulDebugger.DEBUGURGENCY.HIGH);
        return new CurrencyModel("", 0);
    }
    
    public ICurrency RemoveFromCurrencyModel(string id, long amount)
    {
        if (currencies.ContainsKey(id))
        {
            currencies[id].AddCurrency(amount);
            return currencies[id];
        }
        
        MindfulDebugger.DebugError("No currency with the ID: " + id,
            MindfulDebugger.DEBUGURGENCY.HIGH);
        return new CurrencyModel("", 0);
    }
    
    private void GetDataFromServer()
    {
        MindfulDebugger.DebugMessage("Currency's are not being pulled from database.",
            MindfulDebugger.DEBUGURGENCY.HIGH);
        currencies.Add("HC", new CurrencyModel("HC", 0));
        currencies.Add("SC", new CurrencyModel("SC", 0));
        currencies.Add("EC", new CurrencyModel("EC", 0));
    }
}
