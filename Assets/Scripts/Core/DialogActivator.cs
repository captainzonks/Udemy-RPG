using UnityEngine;

namespace Core
{
    public class DialogActivator : MonoBehaviour
    {

        public string[] lines;

        private bool _canActivate;

        public bool isPerson = true;

        public bool shouldActivateQuest;
        public string questToMark;
        public bool markComplete;

        private void Update()
        {
            if (GameManager.Instance.consoleOpen) return;

            if (!_canActivate || !Input.GetButtonDown("Fire1") ||
                DialogManager.Instance.dialogBox.activeInHierarchy) return;
            DialogManager.Instance.ShowDialog(lines, isPerson);
            DialogManager.Instance.ShouldActivateQuestAtEnd(questToMark, markComplete);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _canActivate = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _canActivate = false;
            }
        }
    }
}