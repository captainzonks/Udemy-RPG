using Core;
using UnityEngine;

namespace Quest
{
    public class QuestMarker : MonoBehaviour
    {
        public string questToMark;
        public bool markComplete;

        public bool markOnEnter;
        private bool _canMark;

        public bool deactivateOnMarking;

        private void Update()
        {
            if (GameManager.Instance.consoleOpen) return;

            if (!_canMark || !Input.GetButtonDown("Fire1")) return;
            _canMark = false;
            MarkQuest();
        }

        public void MarkQuest()
        {
            if (markComplete)
            {
                QuestManager.Instance.MarkQuestComplete(questToMark);
            }
            else
            {
                QuestManager.Instance.MarkQuestIncomplete(questToMark);
            }

            gameObject.SetActive(!deactivateOnMarking);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            if (markOnEnter)
            {
                MarkQuest();
            }
            else
            {
                _canMark = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _canMark = false;
            }
        }
    }
}
