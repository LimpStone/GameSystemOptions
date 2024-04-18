
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.ComponentModel;
using System.Reflection;
using System;
using System.Runtime.InteropServices;

[System.Serializable]
public class Keybinds
{
    public Dictionary<string, KeyCode> KeybindDictionary;
    private static Keybinds instance;
    public Keybinds()
    {
        KeybindDictionary = new Dictionary<string, KeyCode>
        {
            { KeyNames.LeftMovement.GetStringValue(),KeyCode.A},
            { KeyNames.LeftMovement2.GetStringValue(),KeyCode.LeftArrow},
            { KeyNames.RightMovement.GetStringValue(),KeyCode.D },
            { KeyNames.RightMovement2.GetStringValue(),KeyCode.RightArrow },
            { KeyNames.Jump.GetStringValue(),KeyCode.Space },
            { KeyNames.Jump2.GetStringValue(),KeyCode.UpArrow},
            { KeyNames.Attack.GetStringValue(), KeyCode.Mouse0},
            { KeyNames.Attack2.GetStringValue(), KeyCode.None},
            { KeyNames.Special1.GetStringValue(), KeyCode.Alpha1 },
            { KeyNames.Special1_2.GetStringValue(), KeyCode.None },
            { KeyNames.Special2.GetStringValue(), KeyCode.Alpha2},
            { KeyNames.Special2_2.GetStringValue(), KeyCode.None},
            { KeyNames.Special3.GetStringValue(),  KeyCode.Alpha3},
            { KeyNames.Special3_2.GetStringValue(), KeyCode.None },
            { KeyNames.Crunch.GetStringValue(), KeyCode.C},
            { KeyNames.Crunch2.GetStringValue(), KeyCode.DownArrow},
            { KeyNames.Interact.GetStringValue(),  KeyCode.E}
        };
    }

    public static Keybinds GetInstance()
    {
        if (instance == null)
        {
            instance = new Keybinds();

            if (!File.Exists(Path.Combine(Application.persistentDataPath, "Keybinds.json")))
            {
                JSeditor.SaveDictionary(instance.KeybindDictionary, "Keybinds.json");
            }
            else
            {
                instance.KeybindDictionary = JSeditor.ReadDictonary("Keybinds.json");
            }
        }
        return instance;
    }
}
public static class KeyNamesExtensions// O.o
{
    public static string GetStringValue(this KeyNames value)
    {
        FieldInfo field = value.GetType().GetField(value.ToString());
        DescriptionAttribute attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
        return attribute.Description;
    }
}
public enum KeyNames
{

    [Description("LeftMovement")]
    LeftMovement,
    [Description("LeftMovement2")]
    LeftMovement2,
    [Description("RightMovement")]
    RightMovement,
    [Description("RightMovement2")]
    RightMovement2,
    [Description("Jump")]
    Jump,
    [Description("Jump2")]
    Jump2,
    [Description("Attack")]
    Attack,
    [Description("Attack2")]
    Attack2,
    [Description("Special1")]
    Special1,
    [Description("Special1_2")]
    Special1_2,
    [Description("Special2")]
    Special2,
    [Description("Special2_2")]
    Special2_2,
    [Description("Special3")]
    Special3,
    [Description("Special3_2")]
    Special3_2,
    [Description("Crunch")]
    Crunch,
    [Description("Crunch2")]
    Crunch2,
    [Description("Interact")]
    Interact
}



