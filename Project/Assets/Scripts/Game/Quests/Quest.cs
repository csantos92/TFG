using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Quest : MonoBehaviour
{
    public bool needsItem, killEnemy, questCompleted, lastQuest;
    public string startText, completeText, title;
    public int questID;
    private QuestManager questManager;
    private DialogueManager _dialogueManager;
    public Sprite npcSprite;
    public QuestItem item;
    public QuestEnemy enemy;
    public Quest nextQuest;
    public GameObject gameFinished;

    public void StartQuest()
    {
        questManager = FindObjectOfType<QuestManager>();
        _dialogueManager = FindObjectOfType<DialogueManager>();
        questManager.ShowQuestText(startText, title, npcSprite);
    }

    public void CompleteQuest()
    {
        questManager = FindObjectOfType<QuestManager>();
        questManager.ShowQuestText(completeText, title, npcSprite);
        questCompleted = true;
        SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.QUEST);

        if (nextQuest != null)
        {
            Invoke("ActivateNextQuest", 7.0f);
        }

        if (lastQuest && _dialogueManager.setActive)
        {
            Time.timeScale = 0;
            gameFinished.SetActive(true);
        }

        gameObject.SetActive(false);
    }

    public void ActivateNextQuest()
    {
        nextQuest.gameObject.SetActive(true);
        nextQuest.StartQuest();
    }

    public void Update()
    {
        if(needsItem && 1 == 0)
        {
            CompleteQuest();
        }

        if(killEnemy && enemy.gameObject.GetComponent<HealthManager>().currentHealth <= 0)
        {
            CompleteQuest();
        }
    }
}
