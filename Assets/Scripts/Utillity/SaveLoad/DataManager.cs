using System.IO;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    string GameDataFileName = "GameData.json";
    public Data data = new Data();
    public void LoadGameData()
    {
        string filePath = Application.persistentDataPath + "/" + GameDataFileName;

        if (File.Exists(filePath))
        {
            string FromJsonData = File.ReadAllText(filePath);
            data = JsonUtility.FromJson<Data>(FromJsonData);
        }
    }
    public void SaveGameData()
    {
        string ToJsonData = JsonUtility.ToJson(data, true);
        string filePath = Application.persistentDataPath + "/" + GameDataFileName;
        File.WriteAllText(filePath, ToJsonData);
    }

    public void SoftResetGameData()
    {
        data.savePos = Vector2.zero;
        data.savedScene = "Tutorial";
        data.difficulty = "";
    }
    public void HardResetData()
    {
        Data newData = new Data();
        data = newData;
        SaveGameData();
    }
}