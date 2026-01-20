using JetBrains.Annotations;
using System.IO;
using UnityEngine;

public class SaveSystem
{
    private static string fileName = "savegame.json";
    //zwraca path do pliku zapisu gry
    private static string GetFilePath()
    {
        return Path.Combine(Application.persistentDataPath, fileName);
    }
    //zapis danych gry do pliku
    public static void Save(SaveData data)
    {
        //konwertuje dane do formatu JSON
        string json = JsonUtility.ToJson(data);
        //zapisuje JSON do pliku
        File.WriteAllText(GetFilePath(), json);
        Debug.Log("Game saved to" + GetFilePath());
    }

    //wczytuje dane gry z pliku
    public static SaveData Load()
    {
        string path = GetFilePath();
        if (!File.Exists(path))
        {
            Debug.LogWarning("Save file not foun in" + path);
            return null;
        }


        //odczytuje JSON z pliku
        string json = File.ReadAllText(path);
        //konwertuje JSON na obiekt SaveData
        SaveData data = JsonUtility.FromJson<SaveData>(json);
        Debug.Log("Game loaded from" + path);
        return data;
    }
}
