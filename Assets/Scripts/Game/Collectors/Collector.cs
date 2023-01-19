using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collector : MonoBehaviour
{
    public Button button;
    private CurrencyService currencyService;
    
    private async void Start()
    {
        currencyService = await ServiceController.GetService<CurrencyService>() as CurrencyService;
        button.onClick.AddListener(AddCurrency);
    }

    private void OnDestroy()
    {
        button.onClick.RemoveListener(AddCurrency);
    }

    private void AddCurrency()
    {
        currencyService.AddToCurrencyModel("SC", 1);
    }
}
