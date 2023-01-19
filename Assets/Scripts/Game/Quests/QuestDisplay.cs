using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestDisplay : MonoBehaviour
{
    [Header("LeftQuests")]
    public Slider LeftSlider;
    public Button LeftNextQuestButton;
    public TMP_Text LeftQuestText;
    public TMP_Text LeftAmountText;

    [Header("CenterQuests")]
    public Slider CenterSlider;
    public Button CenterNextQuestButton;
    public TMP_Text CenterQuestText;
    public TMP_Text CenterAmountText;
    
    [Header("RightQuests")]
    public Slider RightSlider;
    public Button RightNextQuestButton;
    public TMP_Text RightQuestText;
    public TMP_Text RightAmountText;

    private QuestService questService;
    
    private async void Start()
    {
        questService = await ServiceController.GetService<QuestService>() as QuestService;
        InitializeQuestButton();
    }

    private void OnDestroy()
    {
        LeftNextQuestButton.onClick.RemoveAllListeners();
        CenterNextQuestButton.onClick.RemoveAllListeners();
        RightNextQuestButton.onClick.RemoveAllListeners();
    }

    private void Update()
    {
        if (questService == null) return;

        UpdateLeftQuest();
        UpdateCenterQuest();
        UpdateRightQuest();
    }

    private void CollectLeftQuest()
    {
        LeftNextQuestButton.interactable = false;
        questService.CollectRewards(QuestService.QUESTS.LEFT);
        LeftQuestText.SetText(questService.GetQuestDescription(QuestService.QUESTS.LEFT));
    }
    
    private void CollectCenterQuest()
    {
        CenterNextQuestButton.interactable = false;
        questService.CollectRewards(QuestService.QUESTS.CENTER);
        CenterQuestText.SetText(questService.GetQuestDescription(QuestService.QUESTS.CENTER));
    }
    
    private void CollectRightQuest()
    {
        RightNextQuestButton.interactable = false;
        questService.CollectRewards(QuestService.QUESTS.RIGHT);
        RightQuestText.SetText(questService.GetQuestDescription(QuestService.QUESTS.RIGHT));
    }

    private void InitializeQuestButton()
    {
        //Left
        if (LeftNextQuestButton)
        {
            LeftNextQuestButton.interactable = false;
            LeftNextQuestButton.onClick.AddListener(CollectLeftQuest);
            LeftSlider.gameObject.SetActive(true);
            LeftQuestText.SetText(questService.GetQuestDescription(QuestService.QUESTS.LEFT));
        }
        
        //Center
        if (CenterNextQuestButton)
        {
            CenterNextQuestButton.interactable = false;
            CenterNextQuestButton.onClick.AddListener(CollectCenterQuest);
            CenterSlider.gameObject.SetActive(true);
            CenterQuestText.SetText(questService.GetQuestDescription(QuestService.QUESTS.CENTER));
        }
        
        //Right
        if (RightNextQuestButton)
        {
            RightNextQuestButton.interactable = false;
            RightNextQuestButton.onClick.AddListener(CollectRightQuest);
            RightSlider.gameObject.SetActive(true);
            RightQuestText.SetText(questService.GetQuestDescription(QuestService.QUESTS.RIGHT));
        }
    }

    private void UpdateLeftQuest()
    {
        var questStatus = questService.GetQuestStatus(QuestService.QUESTS.LEFT);

        if (questStatus == -1)
        {
            LeftSlider.gameObject.SetActive(false);
        }
        
        LeftSlider.value = questStatus;
        if (Math.Abs(questStatus - 1) < .001f)
        {
            LeftNextQuestButton.interactable = true;
        }

        var AmountText = $"{questService.GetQuestAmount(QuestService.QUESTS.LEFT).ToString()}/" +
                         $"{questService.GetQuestRequiredAmount(QuestService.QUESTS.LEFT).ToString()}";
        LeftAmountText.SetText(AmountText);
    }
    
    private void UpdateCenterQuest()
    {
        var questStatus = questService.GetQuestStatus(QuestService.QUESTS.CENTER);

        if (questStatus == -1)
        {
            CenterSlider.gameObject.SetActive(false);
        }
        
        CenterSlider.value = questStatus;
        if (Math.Abs(questStatus - 1) < .001f)
        {
            CenterNextQuestButton.interactable = true;
        }
        
        var AmountText = $"{questService.GetQuestAmount(QuestService.QUESTS.CENTER).ToString()}/" +
                         $"{questService.GetQuestRequiredAmount(QuestService.QUESTS.CENTER).ToString()}";
        CenterAmountText.SetText(AmountText);
    }
    
    private void UpdateRightQuest()
    {
        var questStatus = questService.GetQuestStatus(QuestService.QUESTS.RIGHT);

        if (questStatus == -1)
        {
            RightSlider.gameObject.SetActive(false);
        }
        
        RightSlider.value = questStatus;
        if (Math.Abs(questStatus - 1) < .001f)
        {
            RightNextQuestButton.interactable = true;
        }
        
        var AmountText = $"{questService.GetQuestAmount(QuestService.QUESTS.RIGHT).ToString()}/" +
                         $"{questService.GetQuestRequiredAmount(QuestService.QUESTS.RIGHT).ToString()}";
        RightAmountText.SetText(AmountText);
    }
}
