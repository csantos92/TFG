﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider2D))]
public class NPCDialogue : MonoBehaviour
{

    public string npcName;
    public string[] npcDialogueLines;
    public Sprite npcSprite;
    private DialogueManager dialogueManager;
    private bool playerInTheZone;
    public bool automaticTalk, finishQuestByTalk, isBoss;
    public GameObject finishQuest, blockPaths, boss;
    public EnemyController enemyController;

    // Start is called before the first frame update
    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();

        if (isBoss)
        {
            enemyController = this.transform.gameObject.GetComponentInParent<EnemyController>();
        }
    }

    void Update()
    {
        if(playerInTheZone && automaticTalk)
        {
            StartTalk();
            automaticTalk = false;

            if(isBoss && blockPaths != null)
            {
                blockPaths.SetActive(true);
            }
            
        }

        if (Input.GetKeyDown(KeyCode.Q)) { StartTalk(); }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            playerInTheZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            playerInTheZone = false;
        }
    }

    public void StartTalk()
    {
        if (playerInTheZone)
        {
            string[] finalDialogue = new string[npcDialogueLines.Length];

            int i = 0;
            foreach (string line in npcDialogueLines)
            {
                finalDialogue[i] = line;
                i++;
            }

            if (npcSprite != null)
            {
                dialogueManager.ShowDialogue(finalDialogue, npcName, npcSprite);

                if (finishQuestByTalk)
                {
                    dialogueManager.finishQuestByTalking = true;
                }
            }
            else
            {
                dialogueManager.ShowDialogue(finalDialogue, npcName);
            }

            if (gameObject.GetComponentInParent<NpcMovement>() != null)
            {
                gameObject.GetComponentInParent<NpcMovement>().isTalking = true;
            }
        }
    }

    public void FinishQuest()
    {
        if (finishQuest != null && !finishQuest.activeInHierarchy && finishQuestByTalk)
        {
            finishQuest.SetActive(true);
        }
    }
}
