using Quest;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class DialogManager : MonoBehaviour
    {
        [SerializeField] Text dialogText;
        [SerializeField] Text nameText;
        [SerializeField] GameObject dialogBox;
        [SerializeField] GameObject nameBox;

        [SerializeField] string[] dialogLines;

        [SerializeField] int currentLine;

        public static DialogManager instance;

        bool _justStarted;

        string _questToMark;
        bool _markQuestComplete;
        bool _shouldMarkQuest;

        void Start()
        {
            instance = this;
        }

        void Update()
        {
            if (!dialogBox.activeInHierarchy) return;
            if (!Input.GetButtonUp("Fire1")) return;
            if (!_justStarted)
            {
                currentLine++;

                if (currentLine >= dialogLines.Length)
                {
                    dialogBox.SetActive(false);

                    GameManager.instance.ModifyDialogActive(false);

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

        public Text GetDialogText()
        {
            return dialogText;
        }

        public Text GetNameText()
        {
            return nameText;
        }

        public GameObject GetDialogBox()
        {
            return dialogBox;
        }

        public GameObject GetNameBox()
        {
            return nameBox;
        }

        public string[] GetDialogLines()
        {
            return dialogLines;
        }

        public int GetCurrentLine()
        {
            return currentLine;
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

            GameManager.instance.ModifyDialogActive(true);
        }

        void CheckIfName()
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