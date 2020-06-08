using Movement;
using UnityEngine.SceneManagement;

namespace Save
{
    [System.Serializable]
    public class PlayerData
    {
        public string currentScene;

        public float[] position;

        public string characterName;
        public int currentHp;
        public int maxHp;
        public int currentEnergy;
        public int maxEnergy;


        public PlayerData(Character.Character character, PlayerController playerController)
        {
            // current scene
            currentScene = SceneManager.GetActiveScene().ToString();

            // character stats
            characterName = character.characterName;
            currentHp = character.currentHp;
            maxHp = character.maxHp;
            currentEnergy = character.currentEnergy;
            maxEnergy = character.maxEnergy;

            // player position
            position = new float[3];
            var playerPosition = playerController.transform.position;
            position[0] = playerPosition.x;
            position[1] = playerPosition.y;
            position[2] = playerPosition.z;
        }
    }
}
