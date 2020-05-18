using UnityEngine;

namespace Core
{
    public class DialogActivator : MonoBehaviour
    {

        [SerializeField] private string[] lines;

        private bool _canActivate;

        [SerializeField] private bool isPerson = true;

        [SerializeField] private bool shouldActivateQuest;
        [SerializeField] private string questToMark;
        [SerializeField] private bool markComplete;


        private void Update()
        {
            if (!_canActivate || !Input.GetButtonDown("Fire1") ||
                DialogManager.Instance.GetDialogBox().activeInHierarchy) return;
            DialogManager.Instance.ShowDialog(lines, isPerson);
            DialogManager.Instance.ShouldActivateQuestAtEnd(questToMark, markComplete);
        }

        public bool CanActivate()
        {
            return _canActivate;
        }

        public void ModifyActivate(bool activate)
        {
            _canActivate = activate;
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