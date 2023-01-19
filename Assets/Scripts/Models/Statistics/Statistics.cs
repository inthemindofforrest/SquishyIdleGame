using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Statistics : IStatistics
{
    public Dictionary<string, long> Stat => stat;
    private Dictionary<string, long> stat = new Dictionary<string, long>();

    public long GetStat(string id)
    {
        if (stat.ContainsKey(id))
        {
            return stat[id];
        }
        else
        {
            return -1;
        }
    }

    public long SetStat(string id, long newValue)
    {
        if (stat.ContainsKey(id))
        {
            stat[id] = newValue;
        }
        else
        {
            stat.Add(id, newValue);
        }

        return stat[id];
    }

    public Dictionary<string, long> GetAllStats()
    {
        return stat;
    }

    public void SetAllStats(Dictionary<string, long> newStats)
    {
        stat = newStats;
    }

    public bool RemoveStatistics(string id)
    {
        if (stat.ContainsKey(id))
        {
            stat.Remove(id);
            return true;
        }
        else
        {
            return false;
        }
    }
}
