using UnityEngine;
using TMPro;

public class LogsTMP : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;

    void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        string log = $"<color=white>{logString}</color>\n";

        switch (type)
        {
            case LogType.Warning:
                log = $"<color=yellow>{logString}</color>\n";
                break;
            case LogType.Error:
            case LogType.Exception:
                log = $"<color=red>{logString}</color>\n";
                break;
        }

        textMeshPro.text += log;
    }
}

