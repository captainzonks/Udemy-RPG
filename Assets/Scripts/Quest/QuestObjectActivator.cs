using UnityEngine;

namespace Quest
{
    public class QuestObjectActivator : MonoBehaviour
    {
        [SerializeField] private GameObject objectToActivate;

        [SerializeField] private string questToCheck;

        [SerializeField] private bool activeIfComplete;

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
