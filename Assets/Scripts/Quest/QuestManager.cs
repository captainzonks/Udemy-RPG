using UnityEngine;

namespace Quest
{
    public class QuestManager : MonoBehaviour
    {
        [SerializeField] private string[] questMarkerNames;
        [SerializeField] private bool[] questMarkersComplete;

        public static QuestManager Instance { get; private set; }

        // Start is called before the first frame update
        private void Start()
        {
            Instance = this;

            questMarkersComplete = new bool[questMarkerNames.Length];
        }

        // Update is called once per frame
        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Q)) return;
            Debug.Log(CheckIfComplete("quest test"));
            MarkQuestComplete("quest test");
            MarkQuestIncomplete("fight the demon");
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

        private void UpdateLocalQuestObjects()
        {
            var questObjects = FindObjectsOfType<QuestObjectActivator>();
            if (questObjects.Length <= 0) return;
            foreach (var t in questObjects)
            {
                t.CheckCompletion();
            }
        }
    }
}
