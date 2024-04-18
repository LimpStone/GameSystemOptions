
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
//Made by Josue Galvan in help with ChatGPT 3.5 
public class UISystem : MonoBehaviour
{
    [Header("Display visuals")]
    [Tooltip("Principal Pause menu")]
    public GameObject PausedIcon;
    [Tooltip("Options Menu Display Object")]
    public GameObject OptionsMenu;
    [Tooltip("Used only to display the extra darkness in pause")]
    public GameObject BackGround;
    [Tooltip("Used to display the video options")]
    public GameObject VideoOptions;
    [Tooltip("Used to display the key options")]
    public GameObject KeyOptions;
    [Tooltip("Used to display the sound options")]
    public GameObject SoundOptions;
    [Tooltip("Used to reference the object with language options")]
    public LanguageSwap LanguageHelper;

    [Header("Dropdowns")]
    [Tooltip("Dropdown to set Fullscren,Windowed..")]
    public TMP_Dropdown ScreenType;
    [Tooltip("Dropdown to set Resolution")]
    public TMP_Dropdown Resolutions;
    [Tooltip("Dropdown to set Refreshrate")]
    public TMP_Dropdown RefreshRate;
    [Tooltip("Dropdown to set Vertical Sync")]
    public TMP_Dropdown Vsync;
    [Tooltip("Dropdown to set a FPS limit")]
    public TMP_Dropdown LimitFPS;
    [Tooltip("Dropdown to set Quality setting")]
    public TMP_Dropdown QualityDrop;
    [Tooltip("Dropdown to set Antialiasing")]
    public TMP_Dropdown Antialiasing;
    [Tooltip("Dropdown to set aviable Languages")]
    public TMP_Dropdown Language;
    [Header("Keybinds")]
    public TMP_InputField LeftMovement;
    public TMP_InputField LeftMovement2, RightMovement, RightMovement2, Jump, Jump2, Attack, Attack2
    , Special1, Special1_2, Special2, Special2_2, Special3, Special3_2, Crunch, Crunch2, Interact;

    [Header("Setting Brightness")]
    [Tooltip("Slider to set value for Brightness")]
    public UnityEngine.UI.Slider BrigthnessSlider;
    [Tooltip("Black Square filling the camera to mimic darkness")]

    public SpriteRenderer Darkness;
    private bool isPaused = false; // variable for pause
    private string[] ScreenSizeOptions = new string[] { "FullScreen", "FullScreen windowed", "Windowed" };
    private string[] BoolOptions = new string[] { "Off", "On" };
    private string[] FPS = new string[] { "No Limit", "60", "120", "144", "240", "360" };
    private string[] AntiAlia = new string[] { "Off", "2x", "4x", "8x" };
    private string[] texts = new string[] { "Español", "English" };
    public static bool Keys = false; // Method to flag when is keybinding
    private UIClassOption data = new UIClassOption //default
    {
        SavedResolution = " ",
        SavedFPS = 60,
        SavedVsync = false,
        SavedBright = 0.0f,
        SavedAntiA = 0,
        Language = 1
    };

    private string ArchiveRoute;

    void Start()
    {
        ArchiveRoute = Path.Combine(Application.persistentDataPath, "UIoptions.json"); //Setting route for general options
        if (PausedIcon) PausedIcon.SetActive(false);
        //Filling Dropdowns 
        FillDropdown(ScreenType, ScreenSizeOptions);
        FillDropdown(Resolutions, GetResolutions());
        FillDropdown(RefreshRate, GetRefreshRate());
        FillDropdown(Vsync, BoolOptions);
        FillDropdown(LimitFPS, FPS);
        FillDropdown(QualityDrop, QualitySettings.names);
        FillDropdown(Antialiasing, AntiAlia);
        FillDropdown(Language, texts);

        //Verify if file for General options exist
        if (File.Exists(ArchiveRoute))
        {
            data = JSeditor.ReadData<UIClassOption>("UIoptions.json"); //reading all data here
            Resolutions.value = GetSavedResolution();
        }
        else
        {
            Resolutions.value = GetCurrentResolution();
        }

        //Setting first value in dropdown 
        Vsync.value = GetCurrentVsync();
        RefreshRate.value = GetCurrentRefreshRatio();
        ScreenType.value = GetCurrentTypeScreen();
        LimitFPS.value = GetCurrnetFPSLimit();
        QualityDrop.value = GetCurrentQuality();
        Antialiasing.value = data.SavedAntiA;
        BrigthnessSlider.value = data.SavedBright;
        Language.value = data.Language;

        Debug.Log("JSONs in :" + ArchiveRoute);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                if (OptionsMenu.active)
                {
                    if(!Keys)Return();
                }
                else
                {
                    ResumeSystem();
                }
            }
            else
            {
                PauseSystem();
            }
        }

    }
    public void EXIT()
    {
        Application.Quit(); //Can be swaped to main menu 
    }
    public void PauseSystem() //function to pause the game
    {
        BackGround.SetActive(true);
        isPaused = true;
        UnityEngine.Cursor.visible = true;
        if (PausedIcon) PausedIcon.SetActive(true);
        Time.timeScale = 0;
    }
    public void ResumeSystem()//function to resume the game
    {
        BackGround.SetActive(false);
        isPaused = false;
        UnityEngine.Cursor.visible = false;
        if (PausedIcon) PausedIcon.SetActive(false);
        Time.timeScale = 1;
    }
    public void Return()
    {
        OptionsMenu.SetActive(false);
        PausedIcon.SetActive(true);
        KeyOptions.SetActive(false);
        SoundOptions.SetActive(false);
        VideoOptions.SetActive(false);
    }

    public void Options()//function to close pause menu and open options menu called from button
    {
        PausedIcon.SetActive(false);
        OptionsMenu.SetActive(true);
        VideoOptions.SetActive(true);
    }
    public void VideoOption()//function to open Video options menu called from button
    {
        KeyOptions.SetActive(false);
        SoundOptions.SetActive(false);
        VideoOptions.SetActive(true);
    }
    public void SoundOption()//function to open Sound options menu called from button
    {
        KeyOptions.SetActive(false);
        SoundOptions.SetActive(true);
        VideoOptions.SetActive(false);
    }
    public void KeyOption()//function to open key options menu called from button
    {
        KeyOptions.SetActive(true);
        SoundOptions.SetActive(false);
        VideoOptions.SetActive(false);

    }
    public void OnLanguageValueChange()
    {
        switch (Language.value)
        {
            case 0:
                //Español
                LanguageHelper.ChangeText(0);
                data.Language = 0;
                break;
            case 1:
                //English
                LanguageHelper.ChangeText(1);
                data.Language = 1;
                break;
        }
        JSeditor.SaveData(data, "UIoptions.json");
    }

    public void OnVsyncValueChanged()
    {
        if (Vsync.value == 0)
        {
            Debug.Log("VsyncOff");
            QualitySettings.vSyncCount = 0;
            data.SavedVsync = false;
        }
        else
        {
            Debug.Log("VsyncOn");
            QualitySettings.vSyncCount = 1;
            data.SavedVsync = true;
        }
    }
    public void OnBrigthnessValueChanted()
    {
        Color ActualColor = Darkness.color;
        ActualColor.a = BrigthnessSlider.value;
        Darkness.color = ActualColor;
        data.SavedBright = BrigthnessSlider.value;
        JSeditor.SaveData(data, "UIoptions.json");
    }
    public void OnScreenTypeValueChanged()
    {
        int idex = GetCurrentDisplayIndex();
        Screen.SetResolution(Display.displays[idex].renderingWidth, Display.displays[idex].renderingHeight, Screen.fullScreenMode);

        if (ScreenType.value == 0)
        {
            RefreshRate.enabled = true;
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
            Debug.Log("Fullscreen");
        }
        if (ScreenType.value == 1)
        {
            RefreshRate.enabled = false;
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
            Debug.Log("Fullscreen Windowed");
        }
        if (ScreenType.value == 2)
        {
            RefreshRate.enabled = false;
            Screen.fullScreenMode = FullScreenMode.Windowed;
            Debug.Log("Windowed");
        }

    }

    public void OnResolutionValueChange()
    {

        string selectedResolution = Resolutions.options[Resolutions.value].text;
        Debug.Log($" {selectedResolution}");

        // Parsear la resolución seleccionada para obtener width y height
        string[] dimensions = selectedResolution.Split('x');
        if (selectedResolution == data.SavedResolution)
        {
            return;
        }
        if (dimensions.Length == 2 && int.TryParse(dimensions[0], out int width) && int.TryParse(dimensions[1], out int height))
        {
            // Cambiar la resolución de la pantalla
            Screen.SetResolution(width, height, Screen.fullScreenMode);
            data.SavedResolution = selectedResolution;
            Debug.Log(data.SavedResolution + " Saved resolution");
            JSeditor.SaveData(data, "UIoptions.json");
        }
        else
        {
            Debug.LogError("Error al parsear la resolución seleccionada.");
        }
    }
    public void OnRefreshRateValueChange()
    {
        string optionText = RefreshRate.options[RefreshRate.value].text;
        int refreshRate = int.Parse(optionText.Split(' ')[0]);
        Debug.Log($" {refreshRate}Hz");
        string[] dimensions = data.SavedResolution.Split('x');
        if (dimensions.Length == 2 && int.TryParse(dimensions[0], out int width) && int.TryParse(dimensions[1], out int height))
            Screen.SetResolution(width, height, Screen.fullScreenMode, refreshRate);
    }
    public void OnAntiAliasingChangeValue()
    {

        switch (Antialiasing.value)
        {
            case 0:
                Debug.Log("Antialiasing OFF");
                QualitySettings.antiAliasing = 0; //OFF
                data.SavedAntiA = 0;
                break;
            case 1:
                Debug.Log("Antialiasing 2X");
                QualitySettings.antiAliasing = 2;
                data.SavedAntiA = 1;
                break;
            case 2:
                Debug.Log("Antialiasing 4X");
                QualitySettings.antiAliasing = 4;
                data.SavedAntiA = 2;
                break;
            case 3:
                Debug.Log("Antialiasing 8X");
                QualitySettings.antiAliasing = 8;
                data.SavedAntiA = 3;
                break;
        }
    }
    public void onQualityValueChange()
    {
        switch (QualityDrop.value)
        {
            case 0:
                Debug.Log(QualitySettings.names[0]);
                QualitySettings.SetQualityLevel(0, true);
                break;
            case 1:
                Debug.Log(QualitySettings.names[1]);
                QualitySettings.SetQualityLevel(1, true);
                break;
            case 2:
                Debug.Log(QualitySettings.names[2]);
                QualitySettings.SetQualityLevel(2, true);
                break;
            case 3:
                Debug.Log(QualitySettings.names[3]);
                QualitySettings.SetQualityLevel(3, true);
                break;
            case 4:
                Debug.Log(QualitySettings.names[4]);
                QualitySettings.SetQualityLevel(4, true);
                break;
            case 5:
                Debug.Log(QualitySettings.names[5]);
                QualitySettings.SetQualityLevel(4, true);
                break;
        }

    }
    public void OnFPSlimitValueChange()
    {
        switch (LimitFPS.value)
        {
            case 0:
                Debug.Log("No FPS limit saved");
                Application.targetFrameRate = -1;
                break;
            case 1:
                Debug.Log("60 FPS limit saved");
                Application.targetFrameRate = 60;
                break;
            case 2:
                Debug.Log("120 FPS limit saved");
                Application.targetFrameRate = 120;
                break;
            case 3:
                Debug.Log("144 FPS limit saved");
                Application.targetFrameRate = 144;
                break;
            case 4:
                Debug.Log("240 FPS limit saved");
                Application.targetFrameRate = 240;
                break;
            case 5:
                Debug.Log("360 FPS limit");
                Application.targetFrameRate = 360;
                break;
        }
        data.SavedFPS = (short)Application.targetFrameRate;
        JSeditor.SaveData(data, "UIoptions.json");
    }

    void FillDropdown(TMP_Dropdown Dropdown2Fill, string[] Options)//Function to fill dropdowns with their specific options
    {
        //Clear the dropdown
        Dropdown2Fill.ClearOptions();
        //Create list to save the data
        List<TMP_Dropdown.OptionData> optionDatas = new List<TMP_Dropdown.OptionData>();
        //For to fill the dropdown
        foreach (string name in Options)
        {
            //Adding optionts to the list
            TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData(name);
            optionDatas.Add(option);
        }
        //Adding options to the dropdown
        Dropdown2Fill.AddOptions(optionDatas);
    }
    int GetCurrentDisplayIndex()
    {
        int currentDisplayIndex = -1;

        for (int i = 0; i < Display.displays.Length; i++)
        {
            if (Display.displays[i].active)
            {
                currentDisplayIndex = i;
                break;
            }
        }

        return currentDisplayIndex;
    }
    int GetCurrentQuality()
    {
        return QualitySettings.GetQualityLevel();
    }
    int GetCurrentVsync()
    {
        return data.SavedVsync ? 0 : 1;
    }
    int GetCurrentRefreshRatio()
    {
        // Obtén las tasas de actualización disponibles del monitor actual
        Resolution[] resolutions = Screen.resolutions;


        // Encuentra el índice de la tasa de actualización actual en la lista de tasas de actualización disponibles
        for (int i = 0; i < resolutions.Length; i++)
        {
            string[] dimensions = data.SavedResolution.Split('x');
            if (dimensions.Length == 2 && int.TryParse(dimensions[0], out int width) && int.TryParse(dimensions[1], out int height))
            {
                if (resolutions[i].width == width && resolutions[i].height == height)
                {
                    Debug.Log(resolutions[i].ToString());
                    return i;
                }
            }
        }

        // Si no se encuentra la tasa de actualización actual, devuelve -1 o algún valor que indique que no se encontró
        return -1;
    }
    int GetCurrnetFPSLimit()
    {
        switch (data.SavedFPS)
        {
            case -1:
                return 0;
            case 60:
                return 1;
            case 120:
                return 2;
            case 144:
                return 3;
            case 240:
                return 4;
            case 360:
                return 5;
            default:
                return 0;
        }
    }
    int GetCurrentTypeScreen()
    {
        FullScreenMode CurrentScreen = Screen.fullScreenMode;

        switch (CurrentScreen)
        {
            case FullScreenMode.ExclusiveFullScreen:
                RefreshRate.enabled = true;
                return 0;
            case FullScreenMode.FullScreenWindow:
                RefreshRate.enabled = false;
                return 1;
            case FullScreenMode.Windowed:
                RefreshRate.enabled = false;
                return 2;
            default:
                return 0;
        }
    }
    int GetCurrentResolution() //Function used to default resolution and works for refresh rate too 
    {
        // Actual resolution
        Resolution currentResolution = Screen.currentResolution;

        // Busca el índice de la resolución actual en el Dropdown
        for (int i = 0; i < Resolutions.options.Count; i++)
        {
            string optionText = Resolutions.options[i].text;
            if (optionText.Contains($"{currentResolution.width} x {currentResolution.height}"))
            {
                return i;
            }
        }
        Debug.Log("Resolution not found");
        // Si no se encuentra la resolución actual, devuelve 0
        return 0;
    }
    int GetSavedResolution()
    {
        string[] dimensions = data.SavedResolution.Split('x');
        if (dimensions.Length == 2 && int.TryParse(dimensions[0], out int width) && int.TryParse(dimensions[1], out int height))
        {
            for (int i = 0; i < Resolutions.options.Count; i++)
            {
                string optionText = Resolutions.options[i].text;
                if (optionText.Contains($"{width} x {height}"))
                {
                    return i;
                }
            }
        }

        return GetCurrentResolution(); //If something explodes it returns the default res
    }

    string[] GetResolutions()
    {
        Resolution[] resolutions = Screen.resolutions;
        HashSet<string> resolutionString = new HashSet<string>();

        for (int i = 0; i < resolutions.Length; i++)
        {
            resolutionString.Add($"{resolutions[i].width} x {resolutions[i].height}");
        }
        string[] resolutionStringArray = new string[resolutionString.Count];
        resolutionString.CopyTo(resolutionStringArray);
        return resolutionStringArray;
    }
    string[] GetRefreshRate()
    {
        // Obtén las tasas de actualización disponibles del monitor actual
        Resolution[] resolutions = Screen.resolutions;
        HashSet<int> refreshRatesSet = new HashSet<int>();

        // Agrega todas las tasas de actualización al conjunto
        foreach (Resolution resolution in resolutions)
        {
            refreshRatesSet.Add(resolution.refreshRate);

        }

        // Convierte el conjunto a una lista para ordenar las tasas de actualización
        List<int> refreshRatesList = new List<int>(refreshRatesSet);

        // Crea un arreglo de strings con las tasas de actualización ordenadas
        string[] opcionesRefreshRate = new string[refreshRatesSet.Count];
        for (int i = 0; i < refreshRatesSet.Count; i++)
        {
            opcionesRefreshRate[i] = $"{refreshRatesList[i]} Hz";
        }

        return opcionesRefreshRate;
    }

}
