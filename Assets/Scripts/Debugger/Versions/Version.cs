using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Version", menuName = "Mindful/MindfulDebugger/Version", order = 1)]
public class Version : ScriptableObject
{
    public int Release;
    public int Epic;
    public int Update;
}
