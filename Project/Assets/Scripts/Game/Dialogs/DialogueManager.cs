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
    public Button talkButton;

    public void Start()
    {
        dialogueActive = false;
        dialogueBox.SetActive(false);
        dialogueBoxTitle.SetActive(false);
        _playerController = FindObjectOfType<PlayerController>();
        _animator = GameObject.Find("Player").GetComponent<Animator>();
        Button btn = talkButton.GetComponent<Button>();
        btn.onClick.AddListener(ContinueTalk);
    }

    public void ContinueTalk()
    {
        if (dialogueActive)
        {
            SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.MENU);
            currentDialogueLine++;

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
    }

    public void ShowDialogue(string[] lines)
    {
        ShowDialogueFill(lines);
    }

    public void ShowDialogue(string[] lines, string npcName, Sprite sprite)
    {
        ShowDialogue(lines, npcName);
        avatarImage.enabled = true;
        avatarImage.sprite = sprite;
    }

    public void ShowDialogueFill(string[] lines)
    {
        SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.MENU);
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
