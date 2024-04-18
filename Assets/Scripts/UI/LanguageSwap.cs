using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
public class LanguageSwap : MonoBehaviour
{
    [Header("First options")]
    public TMP_Text resume;
    public TMP_Text options, exit;
    [Header("Left panel")]
    public TMP_Text Keys;
    public TMP_Text Game, Return, TitleKeys, TitleGame, TitleGraphics;
    [Header("Video options")]
    public TMP_Text Display;
    public TMP_Text Resolution, Refresh, VSync, FPS, Graphics, Anialiasing, Brightness;
    [Header("Key options")]
    public TMP_Text Left;
    public TMP_Text Right, Jump, Attack, Special, Special2, Special3, Crunch,
     PressKey,PressKey2,PressKey3,PressKey4,PressKey5,PressKey6,PressKey7,Interact;
    [Header("Game options")]
    public TMP_Text Languag;
    public ScriptableRef[] Language;
    private UIClassOption data = new UIClassOption
    {
        Language = 1
    };
    void Start()
    {
        string ArchiveRoute = Path.Combine(Application.persistentDataPath, "UIoptions.json");
        if (File.Exists(ArchiveRoute))
        {
            data = JSeditor.ReadData<UIClassOption>("UIoptions.json"); //reading all data here
            ChangeText(data.Language);
        }
    }
    public void ChangeText(int LanguageSelection)
    {
        resume.text = Language[LanguageSelection].resume;
        options.text = Language[LanguageSelection].options;
        exit.text = Language[LanguageSelection].exit;
        ///
        Keys.text = Language[LanguageSelection].Keys;
        Game.text = Language[LanguageSelection].Game;
        Return.text = Language[LanguageSelection].Return;
        TitleGame.text = Language[LanguageSelection].Game;
        TitleGraphics.text = Language[LanguageSelection].Graphics;
        TitleKeys.text = Language[LanguageSelection].Keys;
        ///
        Display.text = Language[LanguageSelection].Display;
        Resolution.text = Language[LanguageSelection].Resolution;
        Graphics.text = Language[LanguageSelection].Graphics;
        Refresh.text = Language[LanguageSelection].Refresh;
        VSync.text = Language[LanguageSelection].VSync;
        FPS.text = Language[LanguageSelection].FPS;
        Anialiasing.text = Language[LanguageSelection].Anialiasing;
        Brightness.text = Language[LanguageSelection].Brightness;
        ///
        Left.text = Language[LanguageSelection].Left;
        Right.text = Language[LanguageSelection].Right;
        Jump.text = Language[LanguageSelection].Jump;
        Attack.text = Language[LanguageSelection].Attack;
        Special.text = Language[LanguageSelection].Special + " 1";
        Special2.text = Language[LanguageSelection].Special + " 2";
        Special3.text = Language[LanguageSelection].Special + " 3";
        Crunch.text = Language[LanguageSelection].Crunch;
        PressKey.text = Language[LanguageSelection].PressKey;
        PressKey2.text = Language[LanguageSelection].PressKey;
        PressKey3.text = Language[LanguageSelection].PressKey;
        PressKey4.text = Language[LanguageSelection].PressKey;
        PressKey5.text = Language[LanguageSelection].PressKey;
        PressKey6.text = Language[LanguageSelection].PressKey;
        PressKey7.text = Language[LanguageSelection].PressKey;
        Interact.text = Language[LanguageSelection].Interact;
        //
        Languag.text = Language[LanguageSelection].Language;
    }
}
