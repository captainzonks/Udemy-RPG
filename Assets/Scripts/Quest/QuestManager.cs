using Core;
using UnityEngine;

namespace Quest
{
    public class QuestManager : MonoBehaviour
    {
        public string[] questMarkerNames;
        public bool[] questMarkersComplete;

        public static QuestManager Instance;

        private void Start()
        {
            Instance = this;

            questMarkersComplete = new bool[questMarkerNames.Length];
        }

        private void Update()
        {
            if (GameManager.Instance.consoleOpen) return;

            // debug for saving and loading quest data
            /*
            if (Input.GetKeyDown(KeyCode.O))
            {
                SaveQuestData();
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                LoadQuestData();
            }
            */

            // debug question completion via keycode
            /*
            if (!Input.GetKeyDown(KeyCode.Q)) return;
            Debug.Log(CheckIfComplete("quest test"));
            MarkQuestComplete("quest test");
            MarkQuestIncomplete("fight the demon");
            */
        }

        private int GetQuestNumber(string questToFind)
        {
            for (var i = 0; i < questMarkerNames.Length; i++)
            {
                if (questMarkerNames[i] == questToFind)
                {
                    return i;
                }
            }

            Debug.LogError("Quest " + questToFind + " does not exist");
            return 0;
        }

        public bool CheckIfComplete(string questToCheck)
        {
            return GetQuestNumber(questToCheck) != 0 && questMarkersComplete[GetQuestNumber(questToCheck)];
        }

        public void MarkQuestComplete(string questToMark)
        {
            questMarkersComplete[GetQuestNumber(questToMark)] = true;
            UpdateLocalQuestObjects();
        }

        public void MarkQuestIncomplete(string questToMark)
        {
            questMarkersComplete[GetQuestNumber(questToMark)] = false;
            UpdateLocalQuestObjects();
        }

        private static void UpdateLocalQuestObjects()
        {
            var questObjects = FindObjectsOfType<QuestObjectActivator>();
            if (questObjects.Length <= 0) return;
            foreach (var t in questObjects)
            {
                t.CheckCompletion();
            }
        }

        public void SaveQuestData()
        {
            for (var i = 0; i < questMarkerNames.Length; i++)
            {
                PlayerPrefs.SetInt("QuestMarker_" + questMarkerNames[i], questMarkersComplete[i] ? 1 : 0);
            }
        }

        public void LoadQuestData()
        {
            for (var i = 0; i < questMarkerNames.Length; i++)
            {
                var questName = questMarkerNames[i];
                var valueToSet = 0;
                if (PlayerPrefs.HasKey("QuestMarker_" + questName))
                {
                    valueToSet = PlayerPrefs.GetInt("QuestMarker_" + questName);
                }

                if (valueToSet == 0)
                {
                    questMarkersComplete[i] = false;
                }
                else
                {
                    questMarkersComplete[i] = true;
                }
            }
        }
    }
}