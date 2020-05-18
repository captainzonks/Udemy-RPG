using UnityEngine;

namespace Quest
{
    public class QuestObjectActivator : MonoBehaviour
    {
        [SerializeField] GameObject objectToActivate;

        [SerializeField] string questToCheck;

        [SerializeField] bool activeIfComplete;

        bool initialCheckDone;

        void Update()
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
