using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    private ItemsManager itemsManager;

    private void Start()
    {
        itemsManager = FindObjectOfType<ItemsManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            itemsManager.AddItem(this.gameObject);
            this.gameObject.SetActive(false);
            //SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.GRAB);
        }
    }
}
