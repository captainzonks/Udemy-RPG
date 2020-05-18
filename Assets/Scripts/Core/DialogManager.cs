using Quest;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class DialogManager : MonoBehaviour
    {
        [SerializeField] private Text dialogText;
        [SerializeField] private Text nameText;
        [SerializeField] private GameObject dialogBox;
        [SerializeField] private GameObject nameBox;

        [SerializeField] private string[] dialogLines;

        [SerializeField] private int currentLine;

        public static DialogManager Instance { get; private set; }

        private bool _justStarted;

        private string _questToMark;
        private bool _markQuestComplete;
        private bool _shouldMarkQuest;

        private void Start()
        {
            Instance = this;
        }

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

                    GameManager.Instance.ModifyDialogActive(false);

                    if (!_shouldMarkQuest) return;
                    _shouldMarkQuest = false;
                    if (_markQuestComplete)
                    {
                        QuestManager.Instance.MarkQuestComplete(_questToMark);
                    }
                    else
                    {
                        QuestManager.Instance.MarkQuestIncomplete(_questToMark);

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

            GameManager.Instance.ModifyDialogActive(true);
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