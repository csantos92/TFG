using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour
{
    public bool dialogueActive, finishQuestByTalking, finishTalk, isTalking, setActive;
    public string[] dialogueLines;
    public int currentDialogueLine;
    public GameObject dialogueBox, dialogueBoxTitle;
    public Text dialogueTitleText, dialogueText;
    public Image avatarImage;
    private PlayerController _playerController;
    public Animator _animator;
    private NPCDialogue _npcDialogue;
    public Button talkButton;

    private void Start()
    {
        dialogueActive = false;
        dialogueBox.SetActive(false);
        dialogueBoxTitle.SetActive(false);

        _playerController = FindObjectOfType<PlayerController>();
        _npcDialogue = FindObjectOfType<NPCDialogue>();
        _animator = GameObject.Find("Player").GetComponent<Animator>();

        Button btn = talkButton.GetComponent<Button>();
        btn.onClick.AddListener(ContinueTalk);
    }

    public void ContinueTalk()
    {
        if (dialogueActive)
        {
            currentDialogueLine++;
            SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.MENU);

            if (currentDialogueLine >= dialogueLines.Length)
            {
                _playerController.isTalking = false;
                currentDialogueLine = 0;
                dialogueActive = false;
                avatarImage.enabled = false;
                dialogueBox.SetActive(false);
                dialogueBoxTitle.SetActive(false);
                _animator.enabled = true;

                if (finishQuestByTalking)
                {
                    setActive = true;
                }
                SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.DIALOG);                
            }
            else
            {
                dialogueText.text = dialogueLines[currentDialogueLine];
            }
        } 
    }

    public void ShowDialogue(string[] lines, string npcName)
    {
        ShowDialogueFill(lines);
        dialogueTitleText.text = npcName;
        SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.DIALOG);
    }

    public void ShowDialogue(string[] lines)
    {
        ShowDialogueFill(lines);
        SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.DIALOG);
    }

    public void ShowDialogue(string[] lines, string npcName, Sprite sprite)
    {
        ShowDialogue(lines, npcName);
        avatarImage.enabled = true;
        avatarImage.sprite = sprite;
        SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.DIALOG);
    }

    private void ShowDialogueFill(string[] lines)
    {
        currentDialogueLine = 0;
        dialogueLines = lines;
        dialogueActive = true;
        dialogueBox.SetActive(true);
        dialogueBoxTitle.SetActive(true);
        avatarImage.enabled = false;
        dialogueText.text = dialogueLines[currentDialogueLine];
        _playerController.isTalking = true;
    }
}
