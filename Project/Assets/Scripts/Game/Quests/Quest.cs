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
    private AudioManager _audioManager;
    public Sprite npcSprite;
    public QuestItem item;
    public QuestEnemy enemy;
    public Quest nextQuest;
    public GameObject gameFinished, dialogOff;
    public GameObject[] dialogOn;

    public void Start()
    {
        _audioManager = FindObjectOfType<AudioManager>();
    }

    public void Update()
    {
        if (needsItem && 1 == 0)
        {
            CompleteQuest();
        }

        if (killEnemy && enemy.gameObject.GetComponent<HealthManager>().currentHealth <= 0)
        {
            CompleteQuest();
        }
    }

    public void StartQuest()
    {
        questManager = FindObjectOfType<QuestManager>();
        questManager.ShowQuestText(startText, title, npcSprite);

        if (lastQuest)
        {
            foreach(GameObject dialog in dialogOn)
            {
                dialog.SetActive(true);
            }
            dialogOff.SetActive(false);
        }
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

        if (lastQuest)
        {
            Invoke("FinishGame", 10.0f);
        }

        gameObject.SetActive(false);
    }

    public void FinishGame()
    {
        Time.timeScale = 0;
        _audioManager.audioCanBePlayed = false;
        SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.VICTORY);
        gameFinished.SetActive(true);
    }

    public void ActivateNextQuest()
    {
        nextQuest.gameObject.SetActive(true);
        nextQuest.StartQuest();
    }
}
