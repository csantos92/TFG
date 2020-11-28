using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXType : MonoBehaviour
{
    public enum SoundType
    {
        ATTACK, DIE, HIT, KNOCK, DRINK, WALK, SLASH, MENU, DIALOG, INVENTORY, GRAB, QUEST
    }

    public SoundType type;

}
