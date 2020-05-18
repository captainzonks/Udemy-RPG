using Core;
using Movement;
using UnityEngine;

namespace Quest
{
    public class QuestObjectActivator : MonoBehaviour
    {
        public GameObject objectToActivate;

        public string questToCheck;

        public bool activeIfComplete;

        private bool initialCheckDone;

        private void Update()
        {
            if (initialCheckDone) return;
            initialCheckDone = true;

            CheckCompletion();
        }

        public void CheckCompletion()
        {
            if (QuestManager.instance.CheckIfComplete(questToCheck))
            {
                objectToActivate.SetActive(activeIfComplete);


            }
        }
    }
}
