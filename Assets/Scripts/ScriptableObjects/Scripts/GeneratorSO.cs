using UnityEngine;

[CreateAssetMenu(fileName = "Generator", menuName = "Mindful/Generator", order = 1)]
public class GeneratorSO : ScriptableObject
{
    public int baseCollector;
    public bool canStackAmount;
    public int timeForRecollection;
    public long costToBuyCollector;
}
