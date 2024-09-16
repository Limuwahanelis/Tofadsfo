using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameSaver : MonoBehaviour
{
    public static int _tutoriallevelindex=1;
    public static string gameSaveFolderPath = Application.dataPath + @"\save";
    public static string gameSaveFilePath = gameSaveFolderPath + @"\data.json";
    public static GameData GameData => _gameData;
    private static GameData _gameData;
    public static void Save()
    {
        string json = JsonUtility.ToJson(_gameData);

        if (!Directory.Exists(gameSaveFolderPath))
        {
            Directory.CreateDirectory(gameSaveFolderPath);
        }
        File.WriteAllText(gameSaveFilePath, json);
    }
    public static bool LoadGameData()
    {
        string json;
        Debug.Log("get save from: " + gameSaveFilePath);
        if (File.Exists(gameSaveFilePath))
        {
            json = File.ReadAllText(gameSaveFilePath);
            _gameData = JsonUtility.FromJson<GameData>(json);
            return true;
        }

        return false;
    }
    public static void CreateGameData()
    {

        GameData saveData = new GameData(_tutoriallevelindex, 0);
        string json = JsonUtility.ToJson(saveData);

        if (!Directory.Exists(gameSaveFolderPath))
        {
            Directory.CreateDirectory(gameSaveFolderPath);
        }
        File.WriteAllText(gameSaveFilePath, json);
        _gameData = saveData;
    }
    public static void UpdateMoney(int money)
    {
        _gameData.savedMoney += money;
    }
    public static void Setlevel(int level)
    {
        _gameData.level = level;
    }
}
