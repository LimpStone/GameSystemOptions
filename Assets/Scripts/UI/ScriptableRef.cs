using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "UItext", menuName = "ScriptableObject/UItext", order = 0)]
public class ScriptableRef : ScriptableObject
{
    [Header("First options")]
    public string resume;
    public string options,exit;
    [Header("Left panel")]
    public string Keys;
    public string Game,Return;
    [Header("Video options")]
    public string Display;
    public string Resolution,Refresh,VSync,FPS,Graphics,Anialiasing,Brightness;
    [Header("Key options")]
    public string Left;
    public string Right,Jump,Attack,Special,Crunch,PressKey,Interact;
    [Header("Game options")]
    public string Language;
}
