using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Character;
using Movement;
using UnityEngine;

namespace Save
{
    public static class SaveSystem
    {
        public static void SavePlayer(Character.Character character, PlayerController playerController)
        {
            var formatter = new BinaryFormatter();
            var path = Application.persistentDataPath + "/playerStats.fun";

            var stream = new FileStream(path, FileMode.Create);

            var data = new PlayerData(character, playerController);

            formatter.Serialize(stream, data);
            stream.Close();
        }

        public static PlayerData LoadPlayer()
        {
            var path = Application.persistentDataPath + "/playerStats.fun";
            if (File.Exists(path))
            {
                var formatter = new BinaryFormatter();
                var stream = new FileStream(path, FileMode.Open);

                var data = formatter.Deserialize(stream) as PlayerData;
                stream.Close();

                return data;
            }
            else
            {
                Debug.LogError("Save file not found in " + path);
                return null;
            }
        }
    }
}
