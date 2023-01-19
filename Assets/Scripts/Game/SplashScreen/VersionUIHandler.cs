using TMPro;
using UnityEngine;

public class VersionUIHandler : MonoBehaviour
{
    public Version GameVersion;
    public TMP_Text VersionText;
    void Start()
    {
        if (!GameVersion)
        {
            MindfulDebugger.DebugMessage("GameVersion not set on VersionUIHandler", MindfulDebugger.DEBUGURGENCY.HIGH);
            return;
        }
        VersionText.SetText($"v{GameVersion.Release}.{GameVersion.Epic}.{GameVersion.Update}");
    }

    
}
