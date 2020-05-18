using UnityEngine;

namespace Quest
{
    public class QuestMarker : MonoBehaviour
    {
        [SerializeField] string questToMark;
        [SerializeField] bool markComplete;

        [SerializeField] bool markOnEnter;
        bool _canMark;

        [SerializeField] bool deactivateOnMarking;

        void Update()
        {
            if (!_canMark || !Input.GetButtonDown("Fire1")) return;
            _canMark = false;
            MarkQuest();
        }

        void MarkQuest()
        {
            if (markComplete)
            {
                QuestManager.instance.MarkQuestComplete(questToMark);
            }
            else
            {
                QuestManager.instance.MarkQuestIncomplete(questToMark);
            }

            gameObject.SetActive(!deactivateOnMarking);
        }

        void OnTriggerEnter2D(Collider2D other)
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

        void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _canMark = false;
            }
        }
    }
}
