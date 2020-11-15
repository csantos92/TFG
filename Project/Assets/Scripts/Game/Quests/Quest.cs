using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Quest : MonoBehaviour
{

    public int questID;
    private QuestManager questManager;
    public string startText, completeText, title;
    public bool needsItem, killsEnemy, questCompleted;
    public List<QuestItem> itemsNeeded;
    public List<QuestEnemy> enemies;
    public List<int> numberOfEnemies;
    public Quest nextQuest;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (needsItem)
        {
            ActivateItems();
        }

        if (killsEnemy)
        {
            ActivateEnemies();
        }
    }

    public void StartQuest()
    {
        questManager = FindObjectOfType<QuestManager>();
        questManager.ShowQuestText(startText, title);

        if (needsItem)
        {
            ActivateItems();
        }

        if (killsEnemy)
        {
            ActivateEnemies();
        }
    }

    void ActivateItems()
    {
        Object[] items = Resources.FindObjectsOfTypeAll(typeof(QuestItem));
        foreach (QuestItem item in items)
        {
            if (item.questID == questID)
            {
                item.gameObject.SetActive(true);
            }
        }
    }

    void ActivateEnemies()
    {
        Object[] qenemies = Resources.FindObjectsOfTypeAll(typeof(QuestEnemy));
        foreach (QuestEnemy enemy in qenemies)
        {
            if (enemy.questID == questID)
            {
                enemy.gameObject.SetActive(true);
            }
        }
    }

    public void CompleteQuest()
    {
        questManager = FindObjectOfType<QuestManager>();
        questManager.ShowQuestText(completeText, title);
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
        if(needsItem && questManager.itemCollected != null)
        {
            for(int i = 0; i < itemsNeeded.Count; i++)
            {
                if(itemsNeeded[i].itemName == questManager.itemCollected.itemName)
                {
                    itemsNeeded.RemoveAt(i);
                    questManager.itemCollected = null;

                    break;
                }
            }

            if(itemsNeeded.Count == 0)
            {
                CompleteQuest();
            }
        }

        if(killsEnemy && questManager.enemyKilled != null)
        {
            for(int i = 0; i < enemies.Count; i++)
            {
                if(enemies[i].enemyName == questManager.enemyKilled.enemyName)
                {
                    numberOfEnemies[i]--;
                    questManager.enemyKilled = null;

                    if (numberOfEnemies[i] <= 0)
                    {
                        enemies.RemoveAt(i);
                        numberOfEnemies.RemoveAt(i);
                    }
                    break;
                }
            }
            if(enemies.Count == 0)
            {
                CompleteQuest();
            }
        }
    }
}
