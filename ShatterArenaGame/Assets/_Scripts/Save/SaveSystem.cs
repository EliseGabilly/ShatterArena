using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem {

    private static readonly string PATH = Application.persistentDataPath + "/player.txt";

    public static void SavePlayer(Player player) {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(PATH, FileMode.Create);

        PlayerData data = new PlayerData(player);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadData() {
        if (File.Exists(PATH)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(PATH, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        } else {
            Debug.LogError("Save file not found (" + PATH + ")");
            return null;
        }
    }

    public static Player LoadDataAsPlayer (Player player) {
        return player.ChangeData(LoadData());
    }

}
