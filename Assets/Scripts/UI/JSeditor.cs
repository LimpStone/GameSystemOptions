using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JSeditor : MonoBehaviour
{
    //Write Function
    public static void SaveData<T>(T data, string fileName)
    {
        // Ruta completa del archivo en la carpeta persistente
        string filePath = Path.Combine(Application.persistentDataPath, fileName);


        string json = JsonUtility.ToJson(data);
        File.WriteAllText(filePath, json);

        Debug.Log($"Data saved in {filePath}");
    }
   
    public static Dictionary<string, KeyCode> ReadDictonary(string fileName)
    {
        string filePath = Path.Combine(Application.persistentDataPath, fileName);

        if (File.Exists(filePath))
        {
            // Leer el JSON desde el archivo
            string json = File.ReadAllText(filePath);

            // Deserializar el JSON a una instancia de KeybindData
            KeybindData keybindData = JsonUtility.FromJson<KeybindData>(json);

            // Crear un diccionario para almacenar los datos
            Dictionary<string, KeyCode> keybindDictionary = new Dictionary<string, KeyCode>();

            // Iterar sobre la lista de KeybindEntry y agregar los datos al diccionario
            foreach (var entry in keybindData.KeybindEntries)
            {
                keybindDictionary.Add(entry.Key, entry.Value);
                Debug.Log($"- Key: {entry.Key}, Value: {entry.Value}");
            }    
            return keybindDictionary;
        }
        else
        {
            Debug.LogWarning($"El archivo {fileName} no existe.");
            return null;
        }
    }
    [System.Serializable]
    public class KeybindEntry
    {
        public string Key;
        public KeyCode Value;
    }
    [System.Serializable]
    public class KeybindData
    {
        public List<KeybindEntry> KeybindEntries;
    }
    public static void SaveDictionary(Dictionary<string, KeyCode> data, string fileName)
    {
        string filePath = Path.Combine(Application.persistentDataPath, fileName);

        // Convertir el diccionario en una lista de KeybindEntry
        List<KeybindEntry> dataList = new List<KeybindEntry>();
        foreach (var pair in data)
        {
            dataList.Add(new KeybindEntry { Key = pair.Key, Value = pair.Value });
        }

        // Log de depuración para imprimir los datos de la lista
        Debug.Log("Datos de la lista:");
        foreach (var entry in dataList)
        {
            Debug.Log($"- Key: {entry.Key}, Value: {entry.Value}");
        }

        // Crear una instancia de KeybindData y asignar la lista de KeybindEntry
        KeybindData keybindData = new KeybindData();
        keybindData.KeybindEntries = dataList;

        // Convertir la instancia de KeybindData en una cadena JSON
        string json = JsonUtility.ToJson(keybindData);

        // Log de depuración para imprimir el JSON generado
        //Debug.Log($"JSON generado: {json}");

        // Guardar el JSON en el archivo
        File.WriteAllText(filePath, json);

       // Debug.Log($"Datos guardados en {filePath}");
    }
    // Read Function
    public static T ReadData<T>(string fileName)
    {
        // Double validation (You have to be sure ;) )
        string filePath = Path.Combine(Application.persistentDataPath, fileName);
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            return JsonUtility.FromJson<T>(json);
        }
        else
        {
            filePath = Path.Combine(Application.dataPath, fileName);  //for no persistant data
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                return JsonUtility.FromJson<T>(json);
            }
            else
            {
                Debug.LogError($"The file {filePath} doesn't exist.");
                return default(T);
            }

        }
    }
}