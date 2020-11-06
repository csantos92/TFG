using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemsManager : MonoBehaviour
{
    private HealthManager healthManager;
    public Text potionCount;
    private int count = 0;
    public GameObject potionCanvas;
    private Items potion;
    public GameObject player;

    private void Start()
    {
        healthManager = FindObjectOfType<HealthManager>();
        potion = FindObjectOfType<Items>();

    }

    private void Update()
    {
        if(count < 10)
        {
            potionCount.text = "x0" + count;
        } 
        else
        {
            potionCount.text = "x" + count;
        }
       
    }

    private List<GameObject> questItems = new List<GameObject>();
    public List<GameObject> GetQuestItems()
    {
        return questItems;
    }
    /*
    public QuestItem GetItemAt(int idx)
    {
        return questItems[idx].GetComponent<QuestItem>();
    }

    public bool HasTheQuestItem(QuestItem item)
    {
        foreach(GameObject qi in questItems)
        {
            if(qi.GetComponent<QuestItem>().itemName == item.itemName)
            {
                return true;
            }
        }
        return false;
    }*/

    public void AddQuestItem(GameObject newItem)
    {
        //SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.GRAB);

        questItems.Add(newItem);
    }

    /// <summary>
    /// Items
    /// </summary>

    private List<GameObject> items = new List<GameObject>();
    public List<GameObject> GetItems()
    {
        return items;
    }

    public Items GetRegularItemAt(int idx)
    {
        return items[idx].GetComponent<Items>();

    }

    public void UseItem(int idx)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] != null)
            {
                var clone = (GameObject)Instantiate(potionCanvas, player.transform.position, Quaternion.Euler(Vector3.zero));
                clone.GetComponent<DamageNumber>().damagePoints = healthManager.maxHealth;

                //healthManager.Heal();
                RemoveItem(items[idx]);
                //SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.DRINK);

                return;
            }
        }
    }


    public void AddItem(GameObject item)
    {

        items.Add(item);
        count++;
    }

    public void RemoveItem(GameObject item)
    {
        items.Remove(item);
        count--;
    }
}
