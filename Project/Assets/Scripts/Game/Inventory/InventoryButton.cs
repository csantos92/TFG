using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour
{

    public enum ItemType { WEAPON, ITEM, SPECIAL_ITEMS};

    public int itemIdx;
    public ItemType type;
    
    public void ActivateButton()
    {
        switch (type)
        {
            case ItemType.WEAPON:
                FindObjectOfType<WeaponManager>().ChangeWeapon(itemIdx);
                FindObjectOfType<UIManager>().inventoryText.text = "Te has equipado: " + FindObjectOfType<WeaponManager>().GetWeaponAt(itemIdx).weaponName;
                //SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.MENU);
                break;

            case ItemType.SPECIAL_ITEMS:
                QuestItem item = FindObjectOfType<ItemsManager>().GetItemAt(itemIdx);
                FindObjectOfType<UIManager>().inventoryText.text = item.itemName;
                //SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.MENU);
                break;
        }
    }
}
