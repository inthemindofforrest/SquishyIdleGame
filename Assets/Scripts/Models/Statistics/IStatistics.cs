using System;
using System.Collections.Generic;

public interface IStatistics
{
    Dictionary<string, long> Stat { get; }
}
