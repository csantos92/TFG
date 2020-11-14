using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CircleCollider2D))]
public class NPCDialogue : MonoBehaviour
{

    public string npcName;
    public string[] npcDialogueLines;
    public Sprite npcSprite;

    private DialogueManager dialogueManager;
    private bool playerInTheZone;

    //private Button startTalkButton;

    // Start is called before the first frame update
    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();

        //startTalkButton = GameObject.Find("Talk").GetComponent<Button>();
        //startTalkButton.onClick.AddListener(StartTalk);
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


            /*if (npcName != null)
            {
                finalDialogue = npcName + "\n\n" + npcDialogue;
            }
            else
            {
                finalDialogue = npcDialogue;
            }*/

            if (npcSprite != null)
            {
                dialogueManager.ShowDialogue(finalDialogue, npcName, npcSprite);

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
}
