using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.Events;

public class QuestService : IService
{
    public enum QUESTS
    {
        LEFT,
        CENTER,
        RIGHT
    }
    
    private StatisticsService statisticsService;
    
    private List<QuestSO> leftQuestsSO;
    private List<QuestSO> centerQuestsSO;
    private List<QuestSO> rightQuestsSO;
    
    private List<Quest> leftQuests = new List<Quest>();
    private int currentLeftQuest;
    private List<Quest> centerQuests = new List<Quest>();
    private int currentCenterQuest;
    private List<Quest> rightQuests = new List<Quest>();
    private int currentRightQuest;

    public async Task Initialize()
    {
        if (GameController.GetLevel() == null) return;
        
        leftQuestsSO = GameController.GetLevel().LeftQuests;
        centerQuestsSO = GameController.GetLevel().CenterQuests;
        rightQuestsSO = GameController.GetLevel().RightQuests;
        currentLeftQuest = 0;
        currentCenterQuest = 0;
        currentRightQuest = 0;
        
        
        
        CreateQuest(QUESTS.LEFT);
        CreateQuest(QUESTS.RIGHT);
        CreateQuest(QUESTS.CENTER);
        
        leftQuests[currentLeftQuest].StartQuest(statisticsService);
        centerQuests[currentCenterQuest].StartQuest(statisticsService);
        rightQuests[currentRightQuest].StartQuest(statisticsService);
    }

    public void Update()
    {
        CheckQuest(QUESTS.LEFT);
        CheckQuest(QUESTS.CENTER);
        CheckQuest(QUESTS.RIGHT);
    }

    public void Injection(Dictionary<Type, IService> services)
    {
        statisticsService = services[typeof(StatisticsService)] as StatisticsService;
    }

    public float GetQuestStatus(QUESTS quest)
    {
        if (statisticsService == null) return 0;

        if (quest == QUESTS.LEFT)
        {
            if (leftQuests.Count < currentLeftQuest + 1) return -1;
            return (float)statisticsService.GetCurrencyData(leftQuests[currentLeftQuest].QuestSo.QuestId) /
                   leftQuests[currentLeftQuest].QuestSo.QuestCompletedRequirement;
        }

        if (quest == QUESTS.RIGHT)
        {
            if (rightQuests.Count < currentRightQuest + 1) return -1;
            return (float)statisticsService.GetCurrencyData(rightQuests[currentRightQuest].QuestSo.QuestId) /
                   rightQuests[currentRightQuest].QuestSo.QuestCompletedRequirement;
        }

        if (centerQuests.Count < currentCenterQuest + 1) return -1;
        return (float)statisticsService.GetCurrencyData(centerQuests[currentCenterQuest].QuestSo.QuestId) /
               centerQuests[currentCenterQuest].QuestSo.QuestCompletedRequirement;
    }

    public int GetQuestRequiredAmount(QUESTS quest)
    {
        if (statisticsService == null) return 0;

        if (quest == QUESTS.LEFT)
        {
            if (leftQuests.Count < currentLeftQuest + 1) return -1;
            return leftQuests[currentLeftQuest].QuestSo.QuestCompletedRequirement;
        }

        if (quest == QUESTS.RIGHT)
        {
            if (rightQuests.Count < currentRightQuest + 1) return -1;
            return rightQuests[currentRightQuest].QuestSo.QuestCompletedRequirement;
        }

        if (centerQuests.Count < currentCenterQuest + 1) return -1;
        return centerQuests[currentCenterQuest].QuestSo.QuestCompletedRequirement;
    }

    
    public long GetQuestAmount(QUESTS quest)
    {
        if (statisticsService == null) return 0;

        if (quest == QUESTS.LEFT)
        {
            if (leftQuests.Count < currentLeftQuest + 1) return -1;
            return statisticsService.GetCurrencyData(leftQuests[currentLeftQuest].QuestSo.QuestId);
        }

        if (quest == QUESTS.RIGHT)
        {
            if (rightQuests.Count < currentRightQuest + 1) return -1;
            return statisticsService.GetCurrencyData(rightQuests[currentRightQuest].QuestSo.QuestId);
        }

        if (centerQuests.Count < currentCenterQuest + 1) return -1;
        return statisticsService.GetCurrencyData(centerQuests[currentCenterQuest].QuestSo.QuestId);
    }
    
    public string GetQuestDescription(QUESTS quest)
    {
        if (statisticsService == null) return "";

        if (quest == QUESTS.LEFT)
        {
            if (leftQuests.Count < currentLeftQuest + 1) return "";
            return leftQuests[currentLeftQuest].QuestSo.QuestDescription; 
        }

        if (quest == QUESTS.RIGHT)
        {
            if (rightQuests.Count < currentRightQuest + 1) return "";
            return rightQuests[currentRightQuest].QuestSo.QuestDescription;
        }

        if (centerQuests.Count < currentCenterQuest + 1) return "";
        return centerQuests[currentCenterQuest].QuestSo.QuestDescription;
    }
    
    public void CollectRewards(QUESTS quest)
    {
        if (quest == QUESTS.LEFT)
        {
            leftQuests[currentLeftQuest].StopQuest();
            leftQuests[currentLeftQuest].CollectRewards();
            currentLeftQuest++;
            if (leftQuests.Count < currentLeftQuest + 1) return;
            leftQuests[currentLeftQuest].StartQuest(statisticsService);
        }
        else if (quest == QUESTS.RIGHT)
        {
            rightQuests[currentRightQuest].StopQuest();
            rightQuests[currentRightQuest].CollectRewards();
            currentRightQuest++;
            if (rightQuests.Count < currentRightQuest + 1) return;
            rightQuests[currentRightQuest].StartQuest(statisticsService);
        }
        else
        {
            centerQuests[currentCenterQuest].StopQuest();
            centerQuests[currentCenterQuest].CollectRewards();
            currentCenterQuest++;
            if (centerQuests.Count < currentCenterQuest + 1) return;
            centerQuests[currentCenterQuest].StartQuest(statisticsService);
        }
    }
    
    public void CreateQuest(QUESTS quest)
    {
        if (quest == QUESTS.LEFT)
        {
            foreach (var so in leftQuestsSO)
            {
                leftQuests.Add(new Quest(so));
            }
        }
        else if (quest == QUESTS.RIGHT)
        {
            foreach (var so in rightQuestsSO)
            {
                rightQuests.Add(new Quest(so));
            }
        }
        else
        {
            foreach (var so in centerQuestsSO)
            {
                centerQuests.Add(new Quest(so));
            }
        }
    }
    
    private void CheckQuest(QUESTS quests)
    {
        if (quests == QUESTS.LEFT)
        {
            if (leftQuests.Count < currentLeftQuest + 1) return;
            var quest = leftQuests[currentLeftQuest];
            var currentQuestAmount = statisticsService.GetCurrencyData(quest.QuestSo.QuestId);
            quest.IsQuestCompleted(currentQuestAmount);
        }
        else if (quests == QUESTS.RIGHT)
        {
            if (rightQuests.Count < currentRightQuest + 1) return;
            var quest = leftQuests[currentRightQuest];
            var currentQuestAmount = statisticsService.GetCurrencyData(quest.QuestSo.QuestId);
            quest.IsQuestCompleted(currentQuestAmount);
        }
        else
        {
            if (centerQuests.Count < currentCenterQuest + 1) return;
            var quest = leftQuests[currentCenterQuest];
            var currentQuestAmount = statisticsService.GetCurrencyData(quest.QuestSo.QuestId);
            quest.IsQuestCompleted(currentQuestAmount);
        }
    }
}
