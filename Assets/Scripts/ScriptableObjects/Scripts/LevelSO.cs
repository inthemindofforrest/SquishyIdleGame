using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Mindful/Level", order = 1)]
public class LevelSO : ScriptableObject
{
    public int LevelNumber;
    public List<GeneratorSO> Generators;
    public List<QuestSO> LeftQuests;
    public List<QuestSO> CenterQuests;
    public List<QuestSO> RightQuests;
}
