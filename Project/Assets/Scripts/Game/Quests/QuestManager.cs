using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{

    public List<Quest> quests;
    private DialogueManager dialogueManager;
    public QuestItem itemCollected;
    public QuestEnemy enemyKilled;

    public Animator _animator;

    public Text questTitle, questMessage;

    // Start is called before the first frame update
    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();

        foreach(Transform t in transform)
        {
            quests.Add(t.gameObject.GetComponent<Quest>());
        }
    }

    public void ShowQuestText(string questText, string title)
    {
        _animator.enabled = false;
        questTitle.text = title;
        questMessage.text = questText;
        dialogueManager.ShowDialogue(new string[] { questText }, title);

    }

    public Quest QuestWithID(int questID)
    {
        Quest q = null;
        foreach(Quest temp in quests)
        {
            if(temp.questID == questID)
            {
                q = temp;
            }

        }
        return q;
    }
}
