﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour
{

    public GameObject dialogueBox, dialogueBoxTitle;
    public Text dialogueTitleText, dialogueText;
    public Image avatarImage;
    public bool dialogueActive;

    public string[] dialogueLines;
    public int currentDialogueLine;

    private PlayerController playerController;
    public Animator _animator;

    public Button talkButton;


    private void Start()
    {
        dialogueActive = false;
        dialogueBox.SetActive(false);
        dialogueBoxTitle.SetActive(false);

        playerController = FindObjectOfType<PlayerController>();

        Button btn = talkButton.GetComponent<Button>();
        btn.onClick.AddListener(ContinueTalk);

        _animator = GameObject.Find("Player").GetComponent<Animator>();

    }


    public void ContinueTalk()
    {
        if (dialogueActive)
        {
            currentDialogueLine++;
            //SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.MENU);


            if (currentDialogueLine >= dialogueLines.Length)
            {
                playerController.isTalking = false;
                currentDialogueLine = 0;
                dialogueActive = false;
                avatarImage.enabled = false;
                dialogueBox.SetActive(false);
                dialogueBoxTitle.SetActive(false);
                _animator.enabled = true;
                //SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.DIALOG);

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
        //SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.DIALOG);

    }

    public void ShowDialogue(string[] lines)
    {
        ShowDialogueFill(lines);

        //SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.DIALOG);

    }

    public void ShowDialogue(string[] lines, string npcName, Sprite sprite)
    {
        ShowDialogue(lines, npcName);
        avatarImage.enabled = true;
        avatarImage.sprite = sprite;
        //SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.DIALOG);

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
        playerController.isTalking = true;
    }
}
