using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayCurrency : MonoBehaviour
{
    public TMP_Text text;
    [SerializeField] private string id;
    private CurrencyService currencyService;

    private async void Start()
    {
        currencyService = await ServiceController.GetService<CurrencyService>() as CurrencyService;
    }

    private void Update()
    {
        if (currencyService != null)
        {
            text.text = currencyService.GetCurrencyModel(id).GetCurrency().ToString();
        }
    }
}
