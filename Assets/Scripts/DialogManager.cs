using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{

    public Text dialogText;
    public Text nameText;
    public GameObject dialogBox;
    public GameObject nameBox;

    public string[] dialogLines;

    public int currentLine;

    public static DialogManager instance;
    private bool justStarted;

    private string questToMark;
    private bool markQuestComplete;
    private bool shouldMarkQuest;

    // Start is called before the first frame update
    private void Start()
    {
        instance = this;

        // dialogText.text = dialogLines[currentLine];
    }

    // Update is called once per frame
    private void Update()
    {
        if (!dialogBox.activeInHierarchy) return;
        if (!Input.GetButtonUp("Fire1")) return;
        if (!justStarted)
        {
            currentLine++;

            if (currentLine >= dialogLines.Length)
            {
                dialogBox.SetActive(false);

                GameManager.instance.dialogActive = false;

                if (!shouldMarkQuest) return;
                shouldMarkQuest = false;
                if (markQuestComplete)
                {
                    QuestManager.instance.MarkQuestComplete(questToMark);
                }
                else
                {
                    QuestManager.instance.MarkQuestIncomplete(questToMark);

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
            justStarted = false;
        }
    }

    public void ShowDialog(string[] newLines, bool isPerson)
    {
        dialogLines = newLines;

        currentLine = 0;

        CheckIfName();

        dialogText.text = dialogLines[currentLine];
        dialogBox.SetActive(true);
        justStarted = true;

        nameBox.SetActive(isPerson);

        GameManager.instance.dialogActive = true;
    }

    private void CheckIfName()
    {
        if (!dialogLines[currentLine].StartsWith("n-")) return;
        nameText.text = dialogLines[currentLine].Replace("n-", "");
        currentLine++;
    }

    public void ShouldActivateQuestAtEnd(string questName, bool markComplete)
    {
        questToMark = questName;
        markQuestComplete = markComplete;

        shouldMarkQuest = true;
    }
}
