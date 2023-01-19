using System;
using UnityEngine.Events;
using Object = UnityEngine.Object;

public interface IQuest
{
    QuestSO QuestSo { get; }
    bool IsQuestComplete { get; }
    
    bool IsQuestCompleted(long stat);
    void StartQuest(StatisticsService service);
    void StopQuest();
    void CollectRewards();
}
