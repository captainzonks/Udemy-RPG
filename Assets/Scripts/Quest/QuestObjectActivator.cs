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

        private bool _initialCheckDone;

        private void Update()
        {
            if (_initialCheckDone) return;
            _initialCheckDone = true;

            CheckCompletion();
        }

        public void CheckCompletion()
        {
            if (QuestManager.Instance.CheckIfComplete(questToCheck))
            {
                objectToActivate.SetActive(activeIfComplete);


            }
        }
    }
}
