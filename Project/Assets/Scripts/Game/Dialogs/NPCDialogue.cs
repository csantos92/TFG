using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider2D))]
public class NPCDialogue : MonoBehaviour
{
    public string npcName;
    public string[] npcDialogueLines;
    private bool playerInTheZone;
    public bool automaticTalk, finishQuestByTalk, isBoss, thisEnemyTalking, finishTalk;
    public Sprite npcSprite;
    public GameObject finishQuest, blockPaths, boss;
    private DialogueManager _dialogueManager;

    // Start is called before the first frame update
    public void Start()
    {
        _dialogueManager = FindObjectOfType<DialogueManager>();
    }

    public void Update()
    {
        if(playerInTheZone && automaticTalk)
        {
            StartTalk();
            automaticTalk = false;
            thisEnemyTalking = true;

            if(isBoss && blockPaths != null)
            {
                blockPaths.SetActive(true);
            }
        }

        if (Input.GetKey(KeyCode.Q))
        {
            if (!_dialogueManager.dialogueActive)
            {
                StartTalk();
            }
        }

        if(playerInTheZone && finishQuestByTalk)
        {
            _dialogueManager.finishQuestByTalking = true;
        }

        if (_dialogueManager.setActive && playerInTheZone && finishQuestByTalk)
        {
            if (finishQuest != null && !finishQuest.activeInHierarchy)
            {
                finishQuest.SetActive(true);
            }
        }

        if (isBoss && thisEnemyTalking && !_dialogueManager.dialogueActive)
        {
            transform.parent.GetComponent<EnemyController>().enabled = true;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            playerInTheZone = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
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
                _dialogueManager.ShowDialogue(finalDialogue, npcName, npcSprite);

                if (finishQuestByTalk)
                {
                    _dialogueManager.finishQuestByTalking = true;
                }
            }
            else
            {
                _dialogueManager.ShowDialogue(finalDialogue, npcName);
            }

            if (gameObject.GetComponentInParent<NpcMovement>() != null)
            {
                gameObject.GetComponentInParent<NpcMovement>().isTalking = true;
            }
        }
    }
}
