using System.Collections.Generic;
using UnityEngine;

public static class MindfulDebugger
{
    public enum DEBUGURGENCY
    {
        HIGH, MEDUIM, LOW
    }

    private static Dictionary<DEBUGURGENCY, string> urgencyColors = new Dictionary<DEBUGURGENCY, string>()
    {
        {DEBUGURGENCY.HIGH, "red"},
        {DEBUGURGENCY.MEDUIM, "orange"},
        {DEBUGURGENCY.LOW, "yellow"},
    };

    public static void DebugError(string message, DEBUGURGENCY urgency = DEBUGURGENCY.LOW)
    {
        Debug.LogError("<color=" + urgencyColors[urgency] + ">" + urgency.ToString() + ") </color>" + message);
    }
    
    public static void DebugMessage(string message, DEBUGURGENCY urgency = DEBUGURGENCY.LOW)
    {
        Debug.Log("<color=" + urgencyColors[urgency] + ">" + urgency.ToString() + ") </color>" + message);
    }
}