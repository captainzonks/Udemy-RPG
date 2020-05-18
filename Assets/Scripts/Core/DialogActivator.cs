using UnityEngine;

namespace Core
{
    public class DialogActivator : MonoBehaviour
    {

        [SerializeField] string[] lines;

        bool _canActivate;

        [SerializeField] bool isPerson = true;

        [SerializeField] bool shouldActivateQuest;
        [SerializeField] string questToMark;
        [SerializeField] bool markComplete;


        void Update()
        {
            if (!_canActivate || !Input.GetButtonDown("Fire1") ||
                DialogManager.instance.GetDialogBox().activeInHierarchy) return;
            DialogManager.instance.ShowDialog(lines, isPerson);
            DialogManager.instance.ShouldActivateQuestAtEnd(questToMark, markComplete);
        }

        public bool CanActivate()
        {
            return _canActivate;
        }

        public void ModifyActivate(bool activate)
        {
            _canActivate = activate;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _canActivate = true;
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _canActivate = false;
            }
        }
    }
}