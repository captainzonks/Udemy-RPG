using Quest;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class DialogManager : MonoBehaviour
    {

        public Text dialogText;
        public Text nameText;
        public GameObject dialogBox;
        public GameObject nameBox;

        public string[] dialogLines;

        public int currentLine;

        public static DialogManager Instance;
        private bool _justStarted;

        private string _questToMark;
        private bool _markQuestComplete;
        private bool _shouldMarkQuest;

        // Start is called before the first frame update
        private void Start()
        {
            Instance = this;

            // dialogText.text = dialogLines[currentLine];
        }

        // Update is called once per frame
        private void Update()
        {
            if (!dialogBox.activeInHierarchy) return;
            if (!Input.GetButtonUp("Fire1")) return;
            if (!_justStarted)
            {
                currentLine++;

                if (currentLine >= dialogLines.Length)
                {
                    dialogBox.SetActive(false);

                    GameManager.Instance.dialogActive = false;

                    if (!_shouldMarkQuest) return;
                    _shouldMarkQuest = false;
                    if (_markQuestComplete)
                    {
                        QuestManager.instance.MarkQuestComplete(_questToMark);
                    }
                    else
                    {
                        QuestManager.instance.MarkQuestIncomplete(_questToMark);

                    }
                }
                else
                {
                    CheckIfName();
                    dialogText.text = dialogLines[currentLine];
                }
            }
            else
            {
                _justStarted = false;
            }
        }

        public void ShowDialog(string[] newLines, bool isPerson)
        {
            dialogLines = newLines;

            currentLine = 0;

            CheckIfName();

            dialogText.text = dialogLines[currentLine];
            dialogBox.SetActive(true);
            _justStarted = true;

            nameBox.SetActive(isPerson);

            GameManager.Instance.dialogActive = true;
        }

        private void CheckIfName()
        {
            if (!dialogLines[currentLine].StartsWith("n-")) return;
            nameText.text = dialogLines[currentLine].Replace("n-", "");
            currentLine++;
        }

        public void ShouldActivateQuestAtEnd(string questName, bool markComplete)
        {
            _questToMark = questName;
            _markQuestComplete = markComplete;

            _shouldMarkQuest = true;
        }
    }
}