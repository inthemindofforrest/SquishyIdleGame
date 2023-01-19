using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class StatisticsService : IService
{
    private const string CurrencyID = "CURRENCY";
    
    private Dictionary<string, Statistics> StatisticsMap = new Dictionary<string, Statistics>();
    public async Task Initialize()
    {
        StatisticsMap = PullData();
        await Task.Delay(1000);
    }

    public void Update()
    {
        //Empty
    }

    public void Injection(Dictionary<Type, IService> services)
    {
        //Empty
    }

    public bool IsStatistic(string id)
    {
        return StatisticsMap.ContainsKey(id);
    }

    public bool IsStatWithinStatistic(string statisticID, string statID)
    {
        if (IsStatistic(statisticID))
        {
            return StatisticsMap[statisticID].Stat.ContainsKey(statID);
        }
        else
        {
            return false;
        }
    }

    //StatisticsFunctions

    public long GetCurrencyData(string statID)
    {
        if (IsStatWithinStatistic(CurrencyID, statID))
        {
            return StatisticsMap[CurrencyID].GetStat(statID);
        }
        else
        {
            return -1;
        }
    }

    public long SetCurrencyData(string statID, long value)
    {
        return StatisticsMap[CurrencyID].SetStat(statID, value);
    }

    public bool RemoveCurrencyStat(string statID)
    {
        return StatisticsMap[CurrencyID].RemoveStatistics(statID);
    }

    public void MassCurrencyUpdate(long addingAmount)
    {
        var stats = StatisticsMap[CurrencyID].GetAllStats();
        var newStats = new Statistics();
        foreach (var stat in stats)
        {
            var statAmount = (long)stat.Value;
            newStats.SetStat(stat.Key, statAmount + addingAmount);
        }

        StatisticsMap[CurrencyID] = newStats;
    }
    //DataFunctions
    private Dictionary<string, Statistics> PullData()
    {
        MindfulDebugger.DebugMessage("Statistics are not being pulled from database.",
            MindfulDebugger.DEBUGURGENCY.HIGH);

        var stats = new Dictionary<string, Statistics>();
        var newStat = new Statistics();
        newStat.SetStat("Main", 0);
        newStat.SetStat("Quest1:1", 0);
        
        stats.Add(CurrencyID, newStat);
        
        return stats;
    }
}
