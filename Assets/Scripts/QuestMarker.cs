using UnityEngine;

public class QuestMarker : MonoBehaviour
{
    public string questToMark;
    public bool markComplete;

    public bool markOnEnter;
    private bool canMark;

    public bool deactivateOnMarking;

    private void Update()
    {
        if (!canMark || !Input.GetButtonDown("Fire1")) return;
        canMark = false;
        MarkQuest();
    }

    public void MarkQuest()
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        if (markOnEnter)
        {
            MarkQuest();
        }
        else
        {
            canMark = true;
        }
    } 
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canMark = false;
        }
    }
}
