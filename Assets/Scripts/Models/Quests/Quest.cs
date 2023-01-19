using UnityEngine.Events;

public class Quest : IQuest
{
    public enum QUESTTYPES
    {
        CURRENCY,
        GENERATOR,
        UPGRADE
    }

    public QuestSO QuestSo => questSO;
    public bool IsQuestComplete => isQuestComplete;

    private QuestSO questSO;
    private bool isQuestComplete;
    private StatisticsService statisticsServiceervice;

    public Quest(QuestSO so)
    {
        questSO = so;
    }
    
    public bool IsQuestCompleted(long stat)
    {
        if (stat >= questSO.QuestCompletedRequirement)
        {
            isQuestComplete = true;
            return true;
        }

        return false;
    }

    public void StartQuest(StatisticsService service)
    {
        statisticsServiceervice = service;
        service.SetCurrencyData(questSO.QuestId, 0);
    }

    public void StopQuest()
    {
        statisticsServiceervice.RemoveCurrencyStat(questSO.QuestId);
    }

    public void CollectRewards()
    {
        //Empty
    }
}
