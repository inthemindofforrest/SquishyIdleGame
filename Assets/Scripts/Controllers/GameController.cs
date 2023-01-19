using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public LevelSO Level;
    private static GameController instance;

    private async void Start()
    {
        instance = this;
        (await ServiceController.GetService<QuestService>() as QuestService)?.Initialize();
    }

    public static LevelSO GetLevel()
    {
        if (instance == null)
        {
            return null;
        }
        return instance.Level;
    }
}
