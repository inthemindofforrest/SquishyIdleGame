using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplashLoadingBar : MonoBehaviour
{
    public Slider ProgressBar;

    private void Update()
    {
        ProgressBar.value = ServiceController.GetInitializedPercent();
    }
}
