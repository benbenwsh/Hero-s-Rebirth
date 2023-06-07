using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{

    private static string path = Application.persistentDataPath + "/game_data.txt";

    

    public static void SaveGameData(Player player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        
        FileStream stream = new FileStream(path, FileMode.Create);

        GameData PlayerData = new GameData(player);

        formatter.Serialize(stream, PlayerData);
        stream.Close();
    }

    public static GameData LoadGameData()
    {
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GameData gameData = formatter.Deserialize(stream) as GameData;
            stream.Close();
            return gameData;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

}
