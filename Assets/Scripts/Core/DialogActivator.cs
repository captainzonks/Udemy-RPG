using UnityEngine;

namespace Core
{
    public class DialogActivator : MonoBehaviour
    {

        public string[] lines;

        private bool canActivate;

        public bool isPerson = true;

        public bool shouldActivateQuest;
        public string questToMark;
        public bool markComplete;


        // Update is called once per frame
        private void Update()
        {
            if (canActivate && Input.GetButtonDown("Fire1") && !DialogManager.instance.dialogBox.activeInHierarchy)
            {
                DialogManager.instance.ShowDialog(lines, isPerson);
                DialogManager.instance.ShouldActivateQuestAtEnd(questToMark, markComplete);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                canActivate = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                canActivate = false;
            }
        }
    }
}