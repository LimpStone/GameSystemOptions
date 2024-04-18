using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Unity.VisualScripting;
using System.Reflection;
using UnityEngine.UI;
public class InputCleaner : MonoBehaviour
{
    private string val;
    private bool flag = false;
    private bool flag2 = false;
    private string TestKey;
    private Keybinds keys;
    private void Start()
    {
        keys = Keybinds.GetInstance();
        TMP_InputField inputField = GetComponent<TMP_InputField>();
        SetInitialKeyValue(inputField);
    }
    private void Update()
    {
        if (flag)
        {
            SetKey(TestKey);
        }
    }

    public void OnSelection()
    {
        if (!flag)
        {
            gameObject.GetComponent<TMP_InputField>().text = "";
        }

    }
    public void OnDeselection()
    {
        gameObject.GetComponent<TMP_InputField>().text = val;
        flag = false;
    }
    private string KeyNameCleaner(KeyCode key)
    {
        string TextKey = key.ToString();
        if (TextKey.Contains("Alpha"))
        {
            // Obtener el último carácter de la cadena (el número)
            char lastChar = TextKey[TextKey.Length - 1];
            return lastChar.ToString();
        }
        return TextKey;
    }
    private HashSet<KeyCode> FillKeys()
    {
        HashSet<KeyCode> lista = new HashSet<KeyCode>
        {
            keys.KeybindDictionary[KeyNames.LeftMovement.GetStringValue()],
            keys.KeybindDictionary[KeyNames.RightMovement.GetStringValue()],
            keys.KeybindDictionary[KeyNames.LeftMovement2.GetStringValue()],
            keys.KeybindDictionary[KeyNames.RightMovement2.GetStringValue()],
            keys.KeybindDictionary[KeyNames.Jump.GetStringValue()],
            keys.KeybindDictionary[KeyNames.Jump2.GetStringValue()],
            keys.KeybindDictionary[KeyNames.Interact.GetStringValue()],
            keys.KeybindDictionary[KeyNames.Crunch.GetStringValue()],
            keys.KeybindDictionary[KeyNames.Crunch2.GetStringValue()],
            keys.KeybindDictionary[KeyNames.Special1.GetStringValue()],
            keys.KeybindDictionary[KeyNames.Special2.GetStringValue()],
            keys.KeybindDictionary[KeyNames.Special3.GetStringValue()],
            keys.KeybindDictionary[KeyNames.Special1_2.GetStringValue()],
            keys.KeybindDictionary[KeyNames.Special2_2.GetStringValue()],
            keys.KeybindDictionary[KeyNames.Special3_2.GetStringValue()],
            keys.KeybindDictionary[KeyNames.Attack.GetStringValue()],
            keys.KeybindDictionary[KeyNames.Attack2.GetStringValue()]
        };
        return lista;
    }
    private bool IsKeyCodeInHashSet(KeyCode keyToCheck, HashSet<KeyCode> keySet)
    {
        return keySet.Contains(keyToCheck);
    }
    private void SetKey(string key)
    {
        if (Input.anyKeyDown)
        {
            if (flag2)
            {
                foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKeyDown(keyCode))
                    {
                        if (keyCode != KeyCode.Escape && keyCode != KeyCode.Tab && keyCode != KeyCode.Return && keyCode != KeyCode.Backspace) //Avoid this keys
                        {
                            if (!IsKeyCodeInHashSet(keyCode, FillKeys()))
                            {
                                keys.KeybindDictionary[key] = keyCode;
                                val = KeyNameCleaner(keyCode);
                                JSeditor.SaveDictionary(keys.KeybindDictionary, "Keybinds.json");
                                Debug.Log(gameObject.name + " set to " + keyCode);
                            }
                            else
                            {
                                Debug.Log("Used key");
                            }
                            flag2 = false;
                            flag = false;
                            UISystem.Keys = false;
                            gameObject.GetComponent<TMP_InputField>().text = val;
                        }else if(keyCode == KeyCode.Escape){ //Cancel keybind
                            flag2 = false;
                            flag = false;
                            gameObject.GetComponent<TMP_InputField>().text = val;
                        }
                    }
                }
            }
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))//Avoid insta click bind
        {
            flag2 = true;
            UISystem.Keys = true; //calling flag in UI system
        }
    }
    //Initial value set for all subscriptions
    private void SetInitialKeyValue(TMP_InputField obj)
    {
        switch (obj.onSelect.GetPersistentMethodName(1))
        {
            case "OnAttack":
                obj.text = KeyNameCleaner(keys.KeybindDictionary[KeyNames.Attack.GetStringValue()]);
                break;
            case "OnAttack2":
                obj.text = KeyNameCleaner(keys.KeybindDictionary[KeyNames.Attack2.GetStringValue()]);
                break;
            case "OnLeftMove":
                obj.text = KeyNameCleaner(keys.KeybindDictionary[KeyNames.LeftMovement.GetStringValue()]);
                break;
            case "OnLeftMove2":
                obj.text = KeyNameCleaner(keys.KeybindDictionary[KeyNames.LeftMovement2.GetStringValue()]);
                break;
            case "OnRightMove":
                obj.text = KeyNameCleaner(keys.KeybindDictionary[KeyNames.RightMovement.GetStringValue()]);
                break;
            case "OnRightMove2":
                obj.text = KeyNameCleaner(keys.KeybindDictionary[KeyNames.RightMovement2.GetStringValue()]);
                break;
            case "OnJump":
                obj.text = KeyNameCleaner(keys.KeybindDictionary[KeyNames.Jump.GetStringValue()]);
                break;
            case "OnJump2":
                obj.text = KeyNameCleaner(keys.KeybindDictionary[KeyNames.Jump2.GetStringValue()]);
                break;
            case "OnCrunch":
                obj.text = KeyNameCleaner(keys.KeybindDictionary[KeyNames.Crunch.GetStringValue()]);
                break;
            case "OnCrunch2":
                obj.text = KeyNameCleaner(keys.KeybindDictionary[KeyNames.Crunch2.GetStringValue()]);
                break;
            case "OnSpecial1":
                obj.text = KeyNameCleaner(keys.KeybindDictionary[KeyNames.Special1.GetStringValue()]);
                break;
            case "OnSpecial1_2":
                obj.text = KeyNameCleaner(keys.KeybindDictionary[KeyNames.Special1_2.GetStringValue()]);
                break;
            case "OnSpecial2":
                obj.text = KeyNameCleaner(keys.KeybindDictionary[KeyNames.Special2.GetStringValue()]);
                break;
            case "OnSpecial2_2":
                obj.text = KeyNameCleaner(keys.KeybindDictionary[KeyNames.Special2_2.GetStringValue()]);
                break;
            case "OnSpecial3":
                obj.text = KeyNameCleaner(keys.KeybindDictionary[KeyNames.Special3.GetStringValue()]);
                break;
            case "OnSpecial3_2":
                obj.text = KeyNameCleaner(keys.KeybindDictionary[KeyNames.Special3_2.GetStringValue()]);
                break;
            case "OnInteract":
                obj.text = KeyNameCleaner(keys.KeybindDictionary[KeyNames.Interact.GetStringValue()]);
                break;
        }
        val = obj.text;
    }
    //Event Subscriptions 
    public void OnAttack()
    {
        TestKey = KeyNames.Attack.GetStringValue();
        flag = true;
    }
    public void OnAttack2()
    {
        TestKey = KeyNames.Attack2.GetStringValue();
        flag = true;
    }
    public void OnLeftMove()
    {
        TestKey = KeyNames.LeftMovement.GetStringValue();
        flag = true;
    }
    public void OnLeftMove2()
    {
        TestKey = KeyNames.LeftMovement2.GetStringValue();
        flag = true;
    }
    public void OnRightMove()
    {
        TestKey = KeyNames.RightMovement.GetStringValue();
        flag = true;
    }
    public void OnRightMove2()
    {
        TestKey = KeyNames.RightMovement2.GetStringValue();
        flag = true;
    }
    public void OnJump()
    {
        TestKey = KeyNames.Jump.GetStringValue();
        flag = true;
    }
    public void OnJump2()
    {
        TestKey = KeyNames.Jump2.GetStringValue();
        flag = true;
    }
    public void OnCrunch()
    {
        TestKey = KeyNames.Crunch.GetStringValue();
        flag = true;
    }
    public void OnCrunch2()
    {
        TestKey = KeyNames.Crunch2.GetStringValue();
        flag = true;
    }
    public void OnSpecial1()
    {
        TestKey = KeyNames.Special1.GetStringValue();
        flag = true;
    }
    public void OnSpecial1_2()
    {
        TestKey = KeyNames.Special1_2.GetStringValue();
        flag = true;
    }
    public void OnSpecial2()
    {
        TestKey = KeyNames.Special2.GetStringValue();
        flag = true;
    }
    public void OnSpecial2_2()
    {
        TestKey = KeyNames.Special2_2.GetStringValue();
        flag = true;
    }
    public void OnSpecial3()
    {
        TestKey = KeyNames.Special3.GetStringValue();
        flag = true;
    }
    public void OnSpecial3_2()
    {
        TestKey = KeyNames.Special3_2.GetStringValue();
        flag = true;
    }
    public void OnInteract()
    {
        TestKey = KeyNames.Interact.GetStringValue();
        flag = true;
    }

}
