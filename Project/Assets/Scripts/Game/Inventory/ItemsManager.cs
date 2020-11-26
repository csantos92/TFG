using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemsManager : MonoBehaviour
{
    private int count, hp;
    private HealthManager _healthManager;
    public Text potionCount;
    public GameObject potionCanvas;
    public GameObject player;

    private void Start()
    {
        _healthManager = player.GetComponentInChildren<HealthManager>();
        count = 0;
        hp = 0;
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
    }

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

    public void UseItem()
    {
        if (items.Count >= 1 && _healthManager.currentHealth < 100)
        {
            if(_healthManager.currentHealth <= 70)
            {
                hp = 30;
                _healthManager.currentHealth += 30;
            }
            else
            {
                hp = 100 - _healthManager.currentHealth;
                _healthManager.currentHealth = 100;
            }
            
            RemoveItem(items[items.Count - 1]);

            var clone = (GameObject)Instantiate(potionCanvas, player.transform.position, Quaternion.Euler(Vector3.zero));
            clone.GetComponent<DamageNumber>().damagePoints = hp;
            //SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.DRINK);
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
