using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "Mindful/Quest", order = 1)]
public class QuestSO : ScriptableObject
{
    public IQuest Prerequisite;
    public string QuestId;
    public int QuestCompletedRequirement;
    public string QuestDescription;
    public Quest.QUESTTYPES QuestType;

    [Header("QuestSpecific")] 
    public GeneratorSO GeneratorSo;
    public string CurrencyType;
}
