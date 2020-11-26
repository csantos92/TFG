using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Quest : MonoBehaviour
{
    public bool needsItem, killEnemy, questCompleted;
    public string startText, completeText, title;
    public int questID;
    private QuestManager questManager;
    public Sprite npcSprite;
    public QuestItem item;
    public QuestEnemy enemy;
    public Quest nextQuest;

    public void StartQuest()
    {
        questManager = FindObjectOfType<QuestManager>();
        questManager.ShowQuestText(startText, title, npcSprite);
    }

    public void CompleteQuest()
    {
        questManager = FindObjectOfType<QuestManager>();
        questManager.ShowQuestText(completeText, title, npcSprite);
        questCompleted = true;
        //SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.QUEST);

        if (nextQuest != null)
        {
            Invoke("ActivateNextQuest", 5.0f);
        }

        gameObject.SetActive(false);
    }

    public void ActivateNextQuest()
    {
        nextQuest.gameObject.SetActive(true);
        nextQuest.StartQuest();
    }

    private void Update()
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
