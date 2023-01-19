using System.Collections;
public class CurrencyModel : ICurrency, IModel
{
    public string ModelId => modelId;
    private string modelId;

    private string currencyId;
    private long currency;

    public CurrencyModel(string id, long amount)
    {
        currencyId = id;
        currency = amount;
    }
    
    public string GetCurrencyId()
    {
        return currencyId;
    }

    public long GetCurrency()
    {
        return currency;
    }

    public long AddCurrency(long addition)
    {
        currency += addition;
        return currency;
    }

    public long SetCurrency(long amount)
    {
        currency = amount;
        return currency;
    }

    public long GetCurrencyPerSecond()
    {
        throw new System.NotImplementedException();
    }
}
