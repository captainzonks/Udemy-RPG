using UnityEngine;

namespace Quest
{
    public class QuestMarker : MonoBehaviour
    {
        [SerializeField] private string questToMark;
        [SerializeField] private bool markComplete;

        [SerializeField] private bool markOnEnter;
        private bool _canMark;

        [SerializeField] private bool deactivateOnMarking;

        private void Update()
        {
            if (!_canMark || !Input.GetButtonDown("Fire1")) return;
            _canMark = false;
            MarkQuest();
        }

        private void MarkQuest()
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
